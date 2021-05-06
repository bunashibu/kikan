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
        .Subscribe(_ => {
          int index = GetUniqueInput();

          if (index == none) // Not using unique skill
            return;

          if (index == z) {
            if (!_player.Location.IsGroundAbove) return;
            if (!ZUsableAnimationState()) return;
            _instantiator.InstantiateSkill(index, this, _player);
          }

          if (index == ctrl) {
            var offset = AppearOffset[index];
            var quat = _player.Renderers[0].flipX ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            _instantiator.InstantiateSkill(index, this, _player, offset, quat);

            offset = AppearOffset[index] + new Vector3(0, 0.27f, 0);
            quat = _player.Renderers[0].flipX ? Quaternion.Euler(0, 180, -20) : Quaternion.Euler(0, 0, -20);
            _instantiator.InstantiateSkill(index, this, _player, offset, quat);

            offset = AppearOffset[index] + new Vector3(0, -0.27f, 0);
            quat = _player.Renderers[0].flipX ? Quaternion.Euler(0, 180, 20) : Quaternion.Euler(0, 0, 20);
            _instantiator.InstantiateSkill(index, this, _player, offset, quat);

            offset = AppearOffset[index] + new Vector3(0, 0.49f, 0);
            quat = _player.Renderers[0].flipX ? Quaternion.Euler(0, 180, -40) : Quaternion.Euler(0, 0, -40);
            _instantiator.InstantiateSkill(index, this, _player, offset, quat);

            offset = AppearOffset[index] + new Vector3(0, -0.49f, 0);
            quat = _player.Renderers[0].flipX ? Quaternion.Euler(0, 180, 40) : Quaternion.Euler(0, 0, 40);
            _instantiator.InstantiateSkill(index, this, _player, offset, quat);
          }
        });
    }

    // NOTE: Z and Ctrl are managed by this; Weapon KeysList Element2(Z) and Element3(Ctrl) are set as None
    private int GetUniqueInput() {
      if (_player.Level.Cur.Value >= RequireLv[z]) {
        if (IsUsable(z) && Input.GetKey(KeyCode.Z))
          return z;
      }
      if (_player.Level.Cur.Value >= RequireLv[ctrl]) {
        if (IsUsable(ctrl) && (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)))
          return ctrl;
      }

      return none;
    }

    private bool ZUsableAnimationState() {
      return _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") ||
        _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") ||
        _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("GroundJump") ||
        _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("LieDown");
    }

    private int none = -1;
    private int z = 2;
    private int ctrl = 3;
  }
}
