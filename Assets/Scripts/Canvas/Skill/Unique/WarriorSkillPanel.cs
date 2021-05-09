using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class WarriorSkillPanel : SkillPanel {
    public void Register(Hammer hammer) {
      base.Register(hammer);

      hammer.IsAcceptingCtrlBreak
        .Where(isAccepting => isAccepting)
        .Subscribe(_ => ShowUniqueSlot(3, 0) )
        .AddTo(hammer.gameObject);
      hammer.IsAcceptingCtrlBreak
        .Where(isAccepting => !isAccepting)
        .Subscribe(_ => HideUniqueSlot(3, 0) )
        .AddTo(hammer.gameObject);

      hammer.IsAcceptingSpaceBreak
        .Where(isAccepting => isAccepting)
        .Subscribe(_ => ShowUniqueSlot(4, 1) )
        .AddTo(hammer.gameObject);
      hammer.IsAcceptingSpaceBreak
        .Where(isAccepting => !isAccepting)
        .Subscribe(_ => HideUniqueSlot(4, 1) )
        .AddTo(hammer.gameObject);

      EventStream.OnClientPlayerDied
        .Subscribe(_ => HideUniqueSlot(4, 1) )
        .AddTo(hammer.gameObject);
    }

    private void ShowUniqueSlot(int index, int slotIndex) {
      _alphaRectTransform[index].parent.gameObject.SetActive(false);
      _uniqueSlotObj[slotIndex].SetActive(true);
    }

    private void HideUniqueSlot(int index, int slotIndex) {
      _alphaRectTransform[index].parent.gameObject.SetActive(true);
      _uniqueSlotObj[slotIndex].SetActive(false);
    }

    protected override void HideAll() {
      base.HideAll();

      foreach (var uniqueSlotObj in _uniqueSlotObj)
        uniqueSlotObj.SetActive(false);
    }

    // NOTE: CtrlBreakSlot is must be 0
    //       SpaceBreakSlot is must be 1
    [SerializeField] private List<GameObject> _uniqueSlotObj;
  }
}
