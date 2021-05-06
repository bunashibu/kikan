using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Bow : Weapon {
    new void Awake() {
      base.Awake();

      this.UpdateAsObservable()
        .Where(_ => _player.PhotonView.isMine              )
        .Where(_ => _player.Hp.Cur.Value > 0               )
        .Where(_ => !_player.State.Rigor                   )
        .Where(_ => _instantiator.IsSkillUsableAnimationState(_player) )
        .Where(_ => !_player.Debuff.State[DebuffType.Stun] )
        .Where(_ => CanInstantiate.Value                   )
        .Where(_ => _player.Location.IsGroundAbove         )
        .Where(_ => UsableAnimationState()                 )
        .Subscribe(_ => {
          int index = GetUniqueInput();

          if (index == -1) // Not using Z
            return;

          _instantiator.InstantiateSkill(index, this, _player);
        });
    }

    // NOTE: Only Z is managed by this; Weapon KeysList Element2(Z) is set as None
    private int GetUniqueInput() {
      int n = 2;

      if (_player.Level.Cur.Value >= RequireLv[n]) {
        if (IsUsable(n) && Input.GetKey(KeyCode.Z))
          return n;
      }

      return -1;
    }

    private bool UsableAnimationState() {
      return _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
        _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
        _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("GroundJump") ||
        _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("LieDown");
    }
  }
}
