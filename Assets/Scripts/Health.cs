using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {
  public void Set(int life, int maxLife) {
    _life = life;
    _maxLife = maxLife;
  }

  public int Get() {
    return _life;
  }

  public int GetMax() {
    return _maxLife;
  }

  public void Plus(int quantity) {
    _life += quantity;

    if (_life < 0)
      _life = 0;
    if (_life > _maxLife)
      _life = _maxLife;
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  [SerializeField] private int _life;
  [SerializeField] private int _maxLife;
}

