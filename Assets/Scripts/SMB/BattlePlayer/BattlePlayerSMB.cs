using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _renderers  = animator.GetComponentsInChildren<SpriteRenderer>();
      _movement   = animator.GetComponent<LobbyPlayer>().Movement;

      _rigidState = animator.GetComponent<RigidState>();
      _hp         = animator.GetComponent<PlayerHp>();
      _skillInfo  = animator.GetComponentInChildren<SkillInfo>();
    }
  }

  protected static bool ShouldTransitionTo(string stateName) {
    switch (stateName) {
      case "Die":
        return _hp.Dead;
      case "Skill":
        return TransitionCheckSkill();
      case "Climb":
        return TransitionCheckClimb();
      case "Walk":
        return TransitionCheckWalk();
      case "LieDown":
        return TransitionCheckLieDown();
      case "GroundJump":
        return _rigidState.Ground && Input.GetButton("Jump");
      case "Fall":
        return _rigidState.Air;
    }

    // Never come to here
    Debug.Log("BattlePlayerSMB-ShouldTransitionTo-stateName is wrong");
    return false;
  }

  private static bool TransitionCheckSkill() {
    bool SkillFlag = ( _skillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                     ( _skillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                     ( _skillInfo.GetState ( SkillName.Z     ) == SkillState.Using );

    return SkillFlag;
  }

  private static bool TransitionCheckClimb() {
    bool OnlyUpKeyDown = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
    bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
    bool ClimbFlag     = ( OnlyUpKeyDown   && !_rigidState.LadderTopEdge    ) ||
                         ( OnlyDownKeyDown && !_rigidState.LadderBottomEdge );

    return _rigidState.Ladder && ClimbFlag;
  }

  private static bool TransitionCheckWalk() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

    return _rigidState.Ground && WalkFlag;
  }

  private static bool TransitionCheckLieDown() {
    bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
    bool LieDownFlag     = OnlyDownKeyDown && !_rigidState.LadderTopEdge;

    return _rigidState.Ground && LieDownFlag;
  }

  protected static void ActTransition(string transitionStateName, Animator animator) {
    animator.SetBool(transitionStateName, true);
    animator.SetBool(_currentStateName, false);
  }

  protected static PhotonView _photonView;
  protected static SpriteRenderer[] _renderers;
  protected static LobbyPlayerMovement _movement;
  protected static string _currentStateName;

  private static RigidState _rigidState;
  private static PlayerHp _hp;
  private static SkillInfo _skillInfo;
}

