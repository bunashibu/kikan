using UnityEngine;
using System.Collections;

public class WalkSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _rigidState = animator.GetComponent<RigidState>();
      _skillInfo  = animator.GetComponentInChildren<SkillInfo>();
      _renderers  = animator.GetComponentsInChildren<SpriteRenderer>();

      _movement   = animator.GetComponent<LobbyPlayer>().Movement;
      _hp         = animator.GetComponent<PlayerHp>();
    }

    Debug.Log("walk");
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      bool OnlyDownKeyDown  = Input.GetKey(KeyCode.DownArrow)  && !Input.GetKey(KeyCode.UpArrow);
      bool OnlyUpKeyDown    = Input.GetKey(KeyCode.UpArrow)    && !Input.GetKey(KeyCode.DownArrow);
      bool JumpButtonDown   = Input.GetButton("Jump");
      bool BothKeyDown      = Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow);
      bool OneKeyUp         = Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow);
      bool ClimbFlag = (OnlyUpKeyDown && !_rigidState.LadderTopEdge) ||
                       (OnlyDownKeyDown && !_rigidState.LadderBottomEdge);

      SkillState stateX     = _skillInfo.GetState(SkillName.X);
      SkillState stateShift = _skillInfo.GetState(SkillName.Shift);
      SkillState stateZ     = _skillInfo.GetState(SkillName.Z);

      bool SkillFlag = (stateX == SkillState.Using) ||
                       (stateShift == SkillState.Using) ||
                       (stateZ == SkillState.Using);

      if (_hp.Dead) {
        ActTransition("Die", animator); return;
      }

      if (SkillFlag) {
        ActTransition("Skill", animator); return;
      }

      if (OnlyLeftKeyDown)  {
        _movement.GroundMoveLeft();

        foreach (var sprite in _renderers)
          sprite.flipX = false;
      }

      if (OnlyRightKeyDown) {
        _movement.GroundMoveRight();

        foreach (var sprite in _renderers)
          sprite.flipX = true;
      }

      if (_rigidState.Ladder && ClimbFlag) {
        ActTransition("Climb", animator); return;
      }

      if (_rigidState.Ground) {
        if (OnlyDownKeyDown && JumpButtonDown) { ActTransition("StepDownJump", animator); return; }
        if (JumpButtonDown)          { ActTransition("GroundJump", animator); return; }
        if (BothKeyDown || OneKeyUp) { ActTransition("Idle", animator);       return; }
      }

      if (_rigidState.Air) {
        ActTransition("Fall", animator); return;
      }
    }
  }

  private void ActTransition(string stateName, Animator animator) {
    animator.SetBool(stateName, true);
    animator.SetBool("Walk", false);
  }

  private PhotonView _photonView;
  private RigidState _rigidState;
  private SkillInfo _skillInfo;
  private SpriteRenderer[] _renderers;

  private LobbyPlayerMovement _movement;
  private PlayerHp _hp;
}

