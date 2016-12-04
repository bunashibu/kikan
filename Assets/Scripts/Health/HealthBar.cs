using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : Bar {
  public override void Show(int life, int maxLife) {
    _text.text = "[" + life.ToString() + "/" + maxLife.ToString() + "]  ";
    Animate(life, maxLife);
  }

  private void Animate(int life, int maxLife) {
    if (life == 0) {
      GetComponent<Slider>().value = 0;
      return;
    }

    float ratio = 1.0f * life / maxLife;
    GetComponent<Slider>().value = ratio;
  }

  [SerializeField] private Text _text;
}

