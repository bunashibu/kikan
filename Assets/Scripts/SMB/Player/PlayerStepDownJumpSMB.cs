using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerStepDownJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      InitFlag();
      _player.FootCollider.isTrigger = true;
      _player.Movement.StepDownJump();
      _player.AudioEnvironment.PlayOneShot("Jump");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        UpdateFlag();

        if ( _player.Hp.Cur.Value <= 0    ) { _player.StateTransfer.TransitTo( "Die" , animator ); return; }
        if ( _player.BuffState.Stun ) { _player.StateTransfer.TransitTo( "Stun", animator ); return; }
        if ( _fallFlag              ) { _player.StateTransfer.TransitTo( "Fall", animator ); return; }
        if (!_player.FootCollider.isTrigger) { _player.StateTransfer.TransitTo( "Idle", animator ); return; }
      }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      _player.FootCollider.isTrigger = false;
    }

    private void InitFlag() {
      _isAlreadyJumped = false;
      _isPassedGround = false;
      _fallFlag = false;
    }

    private void UpdateFlag() {
      if (LocationJudger.IsAir(_player.FootCollider))
        _isAlreadyJumped = true;

      if (LocationJudger.IsGround(_player.FootCollider) && _isAlreadyJumped)
        _isPassedGround = true;

      if (LocationJudger.IsAir(_player.FootCollider) && _isPassedGround)
        _fallFlag = true;
    }

    private Player _player;
    private bool _isAlreadyJumped;
    private bool _isPassedGround;
    private bool _fallFlag;
  }
}

