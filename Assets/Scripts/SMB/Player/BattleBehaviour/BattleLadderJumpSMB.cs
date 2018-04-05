using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleLadderJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<BattlePlayer>();

      LadderJump();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Hp.Cur <= 0                           ) { _player.StateTransfer.TransitTo( "Die"  , animator ); return; }
        if ( _player.BuffState.Stun                        ) { _player.StateTransfer.TransitTo( "Stun" , animator ); return; }
        if ( _player.State.Air && !_player.State.Ladder    ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
        if ( _player.State.Ground && !_player.State.Ladder ) { _player.StateTransfer.TransitTo( "Idle" , animator ); return; }
      }
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

      _player.Movement.LadderJump();
    }

    private BattlePlayer _player;
  }
}

