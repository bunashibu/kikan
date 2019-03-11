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
      // Probably this process should not be here...
      if (PhotonNetwork.player.IsMasterClient && StageReference.Instance.StageData.Name == "FinalBattle")
        _enemy.StateTransfer.TransitTo("Die" , animator);
    }

    private Enemy _enemy;
  }
}

