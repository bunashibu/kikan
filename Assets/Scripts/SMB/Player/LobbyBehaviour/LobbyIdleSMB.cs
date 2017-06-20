using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyIdleSMB : StateMachineBehaviour {
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player.PhotonView.isMine) {
      if ( ShouldTransitToSkill()      ) { _player.StateTransfer.TransitTo( "Skill"      , animator ); return; }
      if ( ShouldTransitToClimb()      ) { _player.StateTransfer.TransitTo( "Climb"      , animator ); return; }
      if ( ShouldTransitToWalk()       ) { _player.StateTransfer.TransitTo( "Walk"       , animator ); return; }
      if ( ShouldTransitToLieDown()    ) { _player.StateTransfer.TransitTo( "LieDown"    , animator ); return; }
      if ( ShouldTransitToGroundJump() ) { _player.StateTransfer.TransitTo( "GroundJump" , animator ); return; }
      if ( _player.RigidState.Air      ) { _player.StateTransfer.TransitTo( "Fall"       , animator ); return; }
    }
  }

  private bool ShouldTransitToSkill() {
    bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                     ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                     ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using );

    return SkillFlag;
  }

  private bool ShouldTransitToClimb() {
    bool OnlyUpKeyDown   = Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow);
    bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
    bool ClimbFlag       = ( OnlyUpKeyDown   && !_player.RigidState.LadderTopEdge    ) ||
                           ( OnlyDownKeyDown && !_player.RigidState.LadderBottomEdge );

    return _player.RigidState.Ladder && ClimbFlag;
  }

  private bool ShouldTransitToWalk() {
    bool OnlyLeftKeyDown  = Input.GetKey(KeyCode.LeftArrow)  && !Input.GetKey(KeyCode.RightArrow);
    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    bool WalkFlag         = OnlyLeftKeyDown || OnlyRightKeyDown;

    return _player.RigidState.Ground && WalkFlag;
  }

  private bool ShouldTransitToLieDown() {
    bool OnlyDownKeyDown = Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow);
    bool LieDownFlag     = OnlyDownKeyDown && !_player.RigidState.LadderTopEdge;

    return _player.RigidState.Ground && LieDownFlag;
  }

  private bool ShouldTransitToGroundJump() {
    return _player.RigidState.Ground && Input.GetButton("Jump");
  }

  [SerializeField] private LobbyPlayerSMB _player;
}

