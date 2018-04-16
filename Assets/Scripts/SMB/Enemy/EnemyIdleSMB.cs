using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyIdleSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_enemy == null)
        _enemy = animator.GetComponent<Enemy>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (PhotonNetwork.player.IsMasterClient) {
        if ( _enemy.Hp.Cur <= 0 ) { _enemy.StateTransfer.TransitTo( "Die" , animator ); return; }

        // Probably this process should not be here...
        if (StageManager.Instance.StageData.Name != "Battle")
          _enemy.StateTransfer.TransitTo("Die" , animator);
      }
    }

    private Enemy _enemy;
  }
}

