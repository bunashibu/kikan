﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerGroundJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      _isAlreadyJumped = false;
      _player.Movement.GroundJump();
      ApplyInertia();

      if (_player.PhotonView.isMine)
        _player.AudioEnvironment.PlayOneShot("Jump", 0.4f);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if (_player.Location.IsAir)
          _isAlreadyJumped = true;

        AirMove();

        if ( _player.Debuff.State[DebuffType.Stun] ) { SyncAnimation( "Stun"   ); return; }
        if ( _player.State.Rigor                   ) { SyncAnimation( "Skill"  ); return; }
        if ( ShouldTransitToLadder() )               { SyncAnimation( "Ladder" ); return; }
        if ( ShouldTransitToFall()   )               { SyncAnimation( "Fall"   ); return; }
      }
    }

    private void SyncAnimation(string state) {
      _player.Synchronizer.SyncAnimation(state);
    }

    private void ApplyInertia() {
      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown && _player.Renderers[0].flipX == false)
        _player.Movement.GroundMoveLeft(0);

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown && _player.Renderers[0].flipX == true)
        _player.Movement.GroundMoveRight(0);
    }

    private void AirMove() {
      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown) {
        _player.Movement.AirMoveLeft();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown) {
        _player.Movement.AirMoveRight();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }
    }

    private bool ShouldTransitToLadder() {
      bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      bool LadderFlag      = ( OnlyUpKeyDown   && !_player.Location.IsLadderTopEdge) ||
                             ( OnlyDownKeyDown && !_player.Location.IsLadderBottomEdge);

      return _player.Location.IsLadder && LadderFlag;
    }

    private bool ShouldTransitToFall() {
      if (_isAlreadyJumped)
        return _player.Rigid.velocity.y <= 0;

      return false;
    }

    private Player _player;
    private bool _isAlreadyJumped;
  }
}
