using UnityEngine;
using System.Collections;

public class BattleGroundJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player == null)
      _player = animator.GetComponent<BattlePlayer>();

    _player.Movement.GroundJump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_player.PhotonView.isMine) {
      if ( _player.Hp.IsDead      ) { _player.StateTransfer.TransitTo( "Die"   , animator ); return; }
      if ( ShouldTransitToSkill() ) { _player.StateTransfer.TransitTo( "Skill" , animator ); return; }
      if ( _player.State.Air      ) { _player.StateTransfer.TransitTo( "Fall"  , animator ); return; }
    }
  }

  private bool ShouldTransitToSkill() {
    bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                     ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                     ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using );

    return SkillFlag;
  }

  private BattlePlayer _player;
}

