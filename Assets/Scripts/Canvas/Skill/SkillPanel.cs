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
          if (weapon.IsRequiredLv(flow.Index)) {
            UpdateAlphaMask(flow.Index, flow.CurCT, weapon.SkillCT[flow.Index]);
          }
        })
        .AddTo(weapon.gameObject);

      weapon.CanInstantiate
        .Where(canInstantiate => !canInstantiate)
        .Subscribe(_ => {
          HideAll();
        })
        .AddTo(weapon.gameObject);

      weapon.CanInstantiate
        .Where(canInstantiate => canInstantiate)
        .Subscribe(_ => {
          for (int i=0; i < _alphaRectTransform.Count; ++i) {
            if (weapon.IsRequiredLv(i)) {
              Show(i);
              if (i > 0)
                _availableImages[i-1].enabled = true;
            }
          }
        })
        .AddTo(weapon.gameObject);

      weapon.Stream.OnInstantiated
        .Where(i => i > 0)
        .Subscribe(i => {
          _availableImages[i-1].enabled = false;
        })
        .AddTo(weapon.gameObject);

      weapon.Stream.OnSkillIsReady
        .Where(i => i > 0)
        .Subscribe(i => {
          _availableImages[i-1].enabled = true;
        })
        .AddTo(weapon.gameObject);

      weapon.Stream.OnSkillAvailable
        .Subscribe(i => {
          Show(i);
          _availableImages[i-1].enabled = true;
        })
        .AddTo(weapon.gameObject);

      foreach (var image in _availableImages)
        image.enabled = false;
      // Alt can available from the beginning
      _availableImages[4].enabled = true;
    }

    protected virtual void HideAll() {
      foreach (var alphaRect in _alphaRectTransform)
        alphaRect.sizeDelta = new Vector2(55.0f, 55.0f);
    }

    private void Show(int i) {
      _alphaRectTransform[i].sizeDelta = new Vector2(55.0f, 0);
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
    [SerializeField] protected List<Image> _availableImages;
  }
}
