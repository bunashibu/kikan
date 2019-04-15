using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public abstract class BarView : MonoBehaviour {
    // Awake() "MUST BE CALLED" if inherited class override it.
    // note: Overriding Awake() is automatically done by just write "void Awake()".
    //       so, be aware that it doesn't require "override modifier".
    protected void Awake() {
      _slider = GetComponent<Slider>();
    }

    public abstract void UpdateView(int cur, int max);

    protected virtual void Animate(int cur, int max) {
      Assert.IsTrue(max > 0);

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

