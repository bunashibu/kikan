using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Hammer : Weapon {
    void Awake() {
      base.Awake();

      this.UpdateAsObservable()
        .Where(_ => _player.PhotonView.isMine                          )
        .Where(_ => _player.Hp.Cur.Value > 0                           )
        .Where(_ => !_player.State.Rigor                               )
        .Where(_ => _instantiator.IsSkillUsableAnimationState(_player) )
        .Where(_ => !_player.Debuff.State[DebuffType.Stun]             )
        .Where(_ => CanInstantiate                                     )
        .Subscribe(_ => {
          GetUniqueInput(_ctrlInfo.Index);
          GetUniqueInput(_spaceInfo.Index);
        });
    }

    private void GetUniqueInput(int i) {
      if (_player.Level.Cur.Value < RequireLv[i])
        return;

      for (int k=0; k < KeysList[i].keys.Count; ++k) {
        if (base.IsUsable(i) && Input.GetKey(KeysList[i].keys[k]))
          InstantiateUniqueSkill(i);
      }
    }

    private void InstantiateUniqueSkill(int i) {
      _instantiator.InstantiateSkill(i, this, _player);
    }

    public override bool IsUsable(int i) {
      if (i == _ctrlInfo.Index || i == _spaceInfo.Index)
        return false;

      return _ctManager.IsUsable(i);
    }

    [SerializeField] private HammerUniqueInfo _ctrlInfo;
    [SerializeField] private HammerUniqueInfo _spaceInfo;
  }

  [System.Serializable]
  public class HammerUniqueInfo {
    public int   Index     => _index;
    public float UsableSec => _usableSec;

    [SerializeField] private int _index;
    [SerializeField] private float _usableSec;
  }
}

