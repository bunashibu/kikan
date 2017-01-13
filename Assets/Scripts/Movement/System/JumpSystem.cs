using UnityEngine;
using System.Collections;

public class JumpSystem : MonoBehaviour {
  void FixedUpdate() {
    if (_flag) {
      _rigid.AddForce(Vector2.up * _force);
      _flag = false;
    }
  }

  public void Jump() {
    _flag = true;
  }

  public void SetForce(float force) {
    _force = force;
  }

  [SerializeField] private Rigidbody2D _rigid;
  public bool CanUse { get; private set; }
  private float _force;
  private bool _flag;
}

