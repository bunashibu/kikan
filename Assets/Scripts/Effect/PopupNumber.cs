using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PopupNumber : MonoBehaviour {
    void Start() {
      Destroy(gameObject, 1.0f);
      _subtractColor = new Color(0, 0, 0, 0.008f);
    }

    void Update() {
      MoveUp();
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
  }
}

