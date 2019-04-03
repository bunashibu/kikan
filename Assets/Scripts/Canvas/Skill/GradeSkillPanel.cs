using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class GradeSkillPanel : SkillPanel {
    public override void Register(Weapon weapon) {
      base.Register(weapon);

      weapon.Stream.OnGradeUpdated
        .Subscribe(index => Replace(index) );
    }

    private void Replace(int index) {
      _alphaRectTransform[index].parent.gameObject.SetActive(false);
      _gradeAlphaRectTransform.parent.gameObject.SetActive(true);

      _alphaRectTransform[index] = _gradeAlphaRectTransform;
    }

    [SerializeField] private RectTransform _gradeAlphaRectTransform;
  }
}

