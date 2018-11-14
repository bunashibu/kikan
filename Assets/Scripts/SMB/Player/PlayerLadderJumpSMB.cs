using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerLadderJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      _player.AudioEnvironment.PlayOneShot("Jump");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if ( _player.Hp.Cur.Value <= 0                           ) { _player.StateTransfer.TransitTo( "Die"  , animator ); return; }
        if ( _player.BuffState.Stun                        ) { _player.StateTransfer.TransitTo( "Stun" , animator ); return; }
        if ( ShouldTransitToSkill()                        ) { _player.StateTransfer.TransitTo( "Skill", animator ); return; }
        if ( LocationJudger.IsAir(_player.FootCollider) && !_player.State.Ladder    ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
        if ( LocationJudger.IsGround(_player.FootCollider) && !_player.State.Ladder ) { _player.StateTransfer.TransitTo( "Idle" , animator ); return; }
      }
    }

    private bool ShouldTransitToSkill() {
      bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Ctrl  ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Space ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Alt   ) == SkillState.Using );

      return SkillFlag;
    }

    private Player _player;
  }
}

