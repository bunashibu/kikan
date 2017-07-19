using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    Debug.Log("Die");
    if (_enemy == null)
      _enemy = animator.GetComponent<Enemy>();

    if (PhotonNetwork.player.IsMasterClient)
      _enemy.PopulationObserver.IntervalReplenishPopulation(_enemy);
  }

  private Enemy _enemy;
}

