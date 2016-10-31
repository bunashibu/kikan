using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
  public void Show(int life, int maxLife) {
    _text.text = "[" + life.ToString() + "/" + maxLife.ToString() + "]  ";

    Animate();

    _life = life;
    _maxLife = maxLife;
  }

  private void Animate() {
  }

  private int _life;
  private int _maxLife;
  [SerializeField] private Text _text;
}
