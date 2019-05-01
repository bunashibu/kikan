using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class PandaSkillPanel : SkillPanel {
    public void Register(Fist fist) {
      base.Register(fist);

      fist.IsAcceptingUnique
        .Where(isAccepting => isAccepting)
        .Subscribe(_ => ShowUniqueSlot(3) )
        .AddTo(fist.gameObject);
      fist.IsAcceptingUnique
        .Where(isAccepting => !isAccepting)
        .Subscribe(_ => HideUniqueSlot(3) )
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

    protected override void HideAll() {
      base.HideAll();
      _uniqueSlotObj.SetActive(false);
    }

    [SerializeField] private GameObject _uniqueSlotObj;
  }
}

