using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      //_health = animator.GetComponent<PlayerHealth>();
      _respawner = animator.GetComponent<PlayerRespawner>();
    }


    Debug.Log("Die");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    /*
    if (_respawner.Ready)
      ActTransition("Idle", animator);
      */
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Die", false);
  }

  private PhotonView _photonView;
  private PlayerHealth _health;
  private PlayerRespawner _respawner;
}

