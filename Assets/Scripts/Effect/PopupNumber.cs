using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PopupNumber : MonoBehaviour {
    void Start() {
      Destroy(gameObject, 1.5f);
      _subtractColor = new Color(0, 0, 0, 0.01f);
      MonoUtility.Instance.DelaySec(0.5f, () => { _ShouldFadeOut = true; });
    }

    void Update() {
      MoveUp();

      if (_ShouldFadeOut)
        FadeOut();
    }

    private void MoveUp() {
      transform.Translate(Vector2.up * 0.002f);
    }

    private void FadeOut() {
      _renderer.color = _renderer.color - _subtractColor;
    }

    [SerializeField] private SpriteRenderer _renderer;
    private Color _subtractColor;
    private bool _ShouldFadeOut = false;
  }
}

