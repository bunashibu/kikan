﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerLadderSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      _player.Rigid.isKinematic = true;
      _player.Rigid.velocity = new Vector2(0.0f, 0.0f);
      _player.FootCollider.isTrigger = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        LadderMove();

        if ( _player.Debuff.State[DebuffType.Stun] ) { SyncAnimation( "Stun"       ); return; }
        if ( ShouldTransitToLadderJump() )           { SyncAnimation( "LadderJump" ); return; }
        if ( ShouldTransitToFall()       )           { SyncAnimation( "Fall"       ); return; }
      }
    }

    private void SyncAnimation(string state) {
      _player.Synchronizer.SyncAnimation(state);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      _player.Rigid.isKinematic = false;
      _player.FootCollider.isTrigger = false;
    }

    private void LadderMove() {
      bool OnlyUpKeyDown = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
      if (OnlyUpKeyDown)
        _player.Movement.LadderMoveUp();

      bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
      if (OnlyDownKeyDown)
        _player.Movement.LadderMoveDown();
    }

    private bool ShouldTransitToLadderJump() {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);

      return Input.GetButton("Jump") && (OnlyLeftKeyDown || OnlyRightKeyDown) && !_player.State.Heavy;
    }

    private bool ShouldTransitToFall() {
      bool IsAscending = _player.Location.IsLadderTopEdge && Input.GetKey(KeyCode.UpArrow);
      bool IsDescending = _player.Location.IsLadderBottomEdge && Input.GetKey(KeyCode.DownArrow);

      return IsAscending || IsDescending;
    }

    private Player _player;
  }
}
