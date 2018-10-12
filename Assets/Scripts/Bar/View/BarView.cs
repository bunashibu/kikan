using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public abstract class BarView : MonoBehaviour {
    void Start() {
      _slider = GetComponent<Slider>();
    }

    public abstract void UpdateView(int cur, int max);

    protected virtual void Animate(int cur, int max) {
      if (cur == 0) {
        _slider.value = 0;
        return;
      }

      float ratio = 1.0f * cur / max;
      _slider.value = ratio;
    }

    private Slider _slider;
  }
}

