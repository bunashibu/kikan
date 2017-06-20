using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DieSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _respawner  = animator.GetComponent<PlayerRespawner>();
    }

    Action action = () => { ActTransition("Idle", animator); };
    _respawner.Respawn(action);

    Debug.Log("Die");
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Die", false);
  }

  private PhotonView _photonView;
  private PlayerRespawner _respawner;
}

