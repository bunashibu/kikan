using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyDieSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_enemy == null)
        _enemy = animator.GetComponent<Enemy>();

      _enemy.BodyCollider.enabled = false;
      _enemy.AI.enabled = false;

      if (PhotonNetwork.player.IsMasterClient && StageReference.Instance.StageData.Name == "Battle")
        _enemy.PopulationObserver.IntervalReplenishPopulation(_enemy);
    }

    private Enemy _enemy;
  }
}

