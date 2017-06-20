using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerSMB: StateMachineBehaviour {
  override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
    if (PhotonView == null) {
      PhotonView   = animator.GetComponent<PhotonView>();
      Renderers    = animator.GetComponentsInChildren<SpriteRenderer>();
      Rigid        = animator.GetComponent<Rigidbody2D>();
      ColliderFoot = animator.GetComponents<BoxCollider2D>()[1];

      RigidState   = animator.GetComponent<RigidState>();
      SkillInfo    = animator.GetComponentInChildren<SkillInfo>();
      Movement     = animator.GetComponent<LobbyPlayer>().Movement;

      StateTransfer = new StateTransfer("Idle");
    }
  }

  public PhotonView          PhotonView    { get; private set; }
  public SpriteRenderer[]    Renderers     { get; private set; } // INFO: [PlayerSprite, WeaponSprite]
  public Rigidbody2D         Rigid         { get; private set; }
  public BoxCollider2D       ColliderFoot  { get; private set; }

  public RigidState          RigidState    { get; private set; }
  public SkillInfo           SkillInfo     { get; private set; }
  public LobbyPlayerMovement Movement      { get; private set; }

  public StateTransfer       StateTransfer { get; private set; }
}

