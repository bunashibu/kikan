using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Kote : Weapon {
    new void Awake() {
      base.Awake();

      this.UpdateAsObservable()
        .Where(_ => _player.PhotonView.isMine )
        .First(_ => _player.Level.Cur.Value >= _uniqueInfo.RequireLv )
        .Subscribe(_ => ReplaceUniqueSkill() )
        .AddTo(_player);
    }

    private void ReplaceUniqueSkill() {
      _skillNames[_uniqueInfo.Index] = _uniqueInfo.After;
      Stream.OnNextUnique(_uniqueInfo.Index);
    }

    [SerializeField] private KoteUniqueInfo _uniqueInfo;
  }

  [System.Serializable]
  public class KoteUniqueInfo {
    public int       RequireLv => _requireLv;
    public int       Index     => _index;
    public SkillName After     => _after;

    [SerializeField] private int _requireLv;
    [SerializeField] private int _index;
    [SerializeField] private SkillName _after;
  }
}
