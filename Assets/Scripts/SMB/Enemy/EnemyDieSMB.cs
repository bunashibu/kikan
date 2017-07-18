using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_enemy == null)
      _enemy = animator.GetComponent<Enemy>();

    if (PhotonNetwork.player.IsMasterClient)
      _enemy.PopulationObserver.IntervalReplenishPopulation(_enemy);
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (PhotonNetwork.player.IsMasterClient)
      PhotonNetwork.Destroy(animator.gameObject);
  }

  private Enemy _enemy;
}

