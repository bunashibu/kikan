using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyIdleSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_enemy == null)
        _enemy = animator.GetComponent<Enemy>();

      if (PhotonNetwork.player.IsMasterClient) {
        MonoUtility.Instance.DelaySec(3.0f, () => { // TEMP
          _enemy.StateTransfer.TransitTo("Die", animator);
        });
      }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    }

    private Enemy _enemy;
  }
}

