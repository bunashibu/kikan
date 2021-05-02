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

      this.UpdateAsObservable()
        .Take(1)
        .Where(_ => {
          return Client.Opponents.Contains(_player);
        })
        .Subscribe(_ => _renderer.color = new Color(0, 1, 1, 1));
    }

    private void ReplaceUniqueSkill() {
      _skillNames[_uniqueInfo.Index] = _uniqueInfo.After;
      Stream.OnNextUnique(_uniqueInfo.Index);
    }

    [SerializeField] private KoteUniqueInfo _uniqueInfo;
    [SerializeField] private SpriteRenderer _renderer;
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
