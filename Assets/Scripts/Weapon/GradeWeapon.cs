using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class GradeWeapon : Weapon {
    void Awake() {
      base.Awake();

      this.UpdateAsObservable()
        .First(_ => _player.Level.Cur.Value >= _gradeInfo.RequireLv )
        .Subscribe(_ => ReplaceGradeSkill() );
    }

    private void ReplaceGradeSkill() {
      _skillNames[_gradeInfo.Index] = _gradeInfo.After;
    }

    [SerializeField] private GradeWeaponInfo _gradeInfo;
  }

  [System.Serializable]
  public class GradeWeaponInfo {
    public int       RequireLv => _requireLv;
    public int       Index     => _index;
    public SkillName After     => _after;

    [SerializeField] private int _requireLv;
    [SerializeField] private int _index;
    [SerializeField] private SkillName _after;
  }
}

