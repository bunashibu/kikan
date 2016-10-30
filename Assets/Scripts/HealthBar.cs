using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {
  void Start() {
    _text.text = "[" + life.ToString() + "/" + maxLife.ToString() + "]  ";
  }

  [SerializeField] public int life;
  [SerializeField] public int maxLife;
  [SerializeField] Text _text;
}
