using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerSMB: StateMachineBehaviour {
  override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
    if (PhotonView == null) {
      PhotonView = animator.GetComponent<PhotonView>();
      RigidState = animator.GetComponent<RigidState>();
      SkillInfo  = animator.GetComponentInChildren<SkillInfo>();
      StateTransfer = new StateTransfer("Idle");
    }
  }

  public PhotonView    PhotonView    { get; private set; }
  public RigidState    RigidState    { get; private set; }
  public SkillInfo     SkillInfo     { get; private set; }
  public StateTransfer StateTransfer { get; private set; }
}

