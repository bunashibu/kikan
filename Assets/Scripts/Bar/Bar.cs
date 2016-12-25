using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Bar : MonoBehaviour {
  public abstract void Show(int cur, int max);

  protected virtual void Animate(int cur, int max) {
    if (cur == 0) {
      GetComponent<Slider>().value = 0;
      return;
    }

    float ratio = 1.0f * cur / max;
    GetComponent<Slider>().value = ratio;
  }
}

