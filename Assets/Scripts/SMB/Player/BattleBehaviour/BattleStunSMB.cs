using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleStunSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<BattlePlayer>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Hp.Cur <= 0 ) { _player.StateTransfer.TransitTo( "Die" , animator ); return; }

        if (!_player.State.Stun) {
          if ( _player.State.Ground  ) { _player.StateTransfer.TransitTo( "Idle" , animator ); return; }
          if ( _player.State.Air     ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
        }
      }
    }

    private BattlePlayer _player;
  }
}

