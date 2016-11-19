using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
  public void Init(int life, int maxLife) { // Consider
    _life = life;
    _maxLife = maxLife;
  }

  public int Get() {
    return _life;
  }

  public int GetMax() {
    return _maxLife;
  }

  public bool IsDead() {
    return _isDead;
  }

  public void Plus(int quantity) {
    _life += quantity;

    if (_life < 0)
      _life = 0;
    if (_life > _maxLife)
      _life = _maxLife;

    _isDead = (_life == 0) ? true : false;
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  [SerializeField] private int _life;
  [SerializeField] private int _maxLife;
  private bool _isDead;
}

