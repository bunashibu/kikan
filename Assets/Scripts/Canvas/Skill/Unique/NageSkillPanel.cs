using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class NageSkillPanel : SkillPanel {
    public override void Register(Weapon weapon) {
      base.Register(weapon);

      weapon.Stream.OnUniqueUsed
        .Subscribe(index => Replace(index) )
        .AddTo(weapon.gameObject);
    }

    private void Replace(int index) {
      _alphaRectTransform[index].parent.gameObject.SetActive(false);
      _uniqueAlphaRectTransform.parent.gameObject.SetActive(true);

      _alphaRectTransform[index] = _uniqueAlphaRectTransform;
    }

    [SerializeField] private RectTransform _uniqueAlphaRectTransform;
  }
}

