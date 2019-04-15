using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class PandaSkillPanel : SkillPanel {
    public void Register(Fist fist) {
      base.Register(fist);

      fist.Stream.OnUniqueUsed
        .Where(_ => fist.IsSecondTime )
        .Subscribe(index => ShowUniqueSlot(index) )
        .AddTo(fist.gameObject);

      fist.Stream.OnUniqueUsed
        .Where(_ => !fist.IsSecondTime )
        .Subscribe(index => HideUniqueSlot(index) )
        .AddTo(fist.gameObject);
    }

    private void ShowUniqueSlot(int index) {
      _alphaRectTransform[index].parent.gameObject.SetActive(false);
      _uniqueSlotObj.SetActive(true);
    }

    private void HideUniqueSlot(int index) {
      _alphaRectTransform[index].parent.gameObject.SetActive(true);
      _uniqueSlotObj.SetActive(false);
    }

    [SerializeField] private GameObject _uniqueSlotObj;
  }
}

