using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerLadderJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      if (_player.PhotonView.isMine)
        _player.AudioEnvironment.PlayOneShot("Jump", 0.4f);

      LadderJump();
      _player.Movement.LadderJump();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Debuff.State[DebuffType.Stun] ) { SyncAnimation( "Stun"  ); return; }
        if ( _player.State.Rigor                   ) { SyncAnimation( "Skill" ); return; }
        if ( _player.Location.IsAir    && !_player.Location.IsLadder ) { SyncAnimation( "Fall" ); return; }
        if ( _player.Location.IsGround && !_player.Location.IsLadder ) { SyncAnimation( "Idle" ); return; }
      }
    }

    private void SyncAnimation(string state) {
      _player.Synchronizer.SyncAnimation(state);
    }

    private void LadderJump() {
      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown) {
        _player.Movement.GroundMoveLeft();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown) {
        _player.Movement.GroundMoveRight();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }
    }

    private Player _player;
  }
}
