using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _respawner = animator.GetComponent<PlayerRespawner>();
    }

    _respawner.Respawn();

    Debug.Log("Die");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_respawner.Ready) {
      _respawner.Ready = false;
      ActTransition("Idle", animator);
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Die", false);
  }

  private PhotonView _photonView;
  private PlayerRespawner _respawner;
}

