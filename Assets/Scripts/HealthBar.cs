using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {
  public void Decrease(int damage) {
    GetComponentInChildren<RectTransform>().sizeDelta = new Vector2(200-damage, 30);
  }

  private int _life;
  [SerializeField] public int _maxLife;
}
