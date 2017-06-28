using UnityEngine;
using System.Collections;

public class GroundJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<PlayerState>();
      _skillInfo  = animator.GetComponentInChildren<SkillInfo>();

      _movement   = animator.GetComponent<LobbyPlayer>().Movement;
      _hp         = animator.GetComponent<PlayerHp>();
    }

    Debug.Log("jump");
    _movement.GroundJump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      SkillState stateX     = _skillInfo.GetState(SkillName.X);
      SkillState stateShift = _skillInfo.GetState(SkillName.Shift);
      SkillState stateZ     = _skillInfo.GetState(SkillName.Z);

      bool SkillFlag = (stateX == SkillState.Using) ||
                       (stateShift == SkillState.Using) ||
                       (stateZ == SkillState.Using);

      if (_hp.IsDead) { ActTransition("Die", animator); return; }

      if (SkillFlag) { ActTransition("Skill", animator); return; }

      if (_rigidState.Air) {
        ActTransition("Fall", animator); return;
      }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("GroundJump", false);
  }

  private PhotonView _photonView;
  private PlayerState _rigidState;
  private SkillInfo _skillInfo;

  private LobbyPlayerMovement _movement;
  private PlayerHp _hp;
}

