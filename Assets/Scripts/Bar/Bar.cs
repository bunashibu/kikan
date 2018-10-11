using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bunashibu.Kikan {
  public abstract class Bar : MonoBehaviour, IObserver {
    void Start() {
      _slider = GetComponent<Slider>();
    }

    public abstract void OnNotify(Notification notification, object[] args);
    public abstract void UpdateView(int cur, int max); // To be protected

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

