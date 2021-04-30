using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Bunashibu.Kikan {
  public class SkillPanel : MonoBehaviour {
    public virtual void Register(Weapon weapon) {
      weapon.Stream.OnCurCT
        .Where(_ => weapon.CanInstantiate.Value)
        .Subscribe(flow => {
          if (weapon.IsReachedToRequireLv(flow.Index))
            UpdateAlphaMask(flow.Index, flow.CurCT, weapon.SkillCT[flow.Index]);
        })
        .AddTo(weapon.gameObject);

      weapon.CanInstantiate
        .Subscribe(canInstantiate => {
          if (!canInstantiate)
            HideAll();
        })
        .AddTo(weapon.gameObject);
    }

    protected virtual void HideAll() {
      foreach (var alphaRect in _alphaRectTransform)
        alphaRect.sizeDelta = new Vector2(55.0f, 55.0f);
    }

    private void UpdateAlphaMask(int i, float cur, float max) {
      if (max == 0)
        return;

      if (i == 0)
        // NOTE: X is always visible
        _alphaRectTransform[0].sizeDelta = new Vector2(55.0f, 0);
      else
        // NOTE: AlphaMask width and height == 55.0f
        _alphaRectTransform[i].sizeDelta = new Vector2(55.0f, 55.0f * (cur / max));
    }

    [SerializeField] protected List<RectTransform> _alphaRectTransform;
  }
}
