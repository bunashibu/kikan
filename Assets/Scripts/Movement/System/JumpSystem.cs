using UnityEngine;
using System.Collections;

public class JumpSystem : MonoBehaviour {
  void FixedUpdate() {
    _rigid.AddForce(_inputVec * _force);
  }

  public void Jump() {
    _inputVec.y = 1;
  }

  public void Stay() {
    _inputVec.y = 0;
  }

  public void SetForce(float force) {
    _force = force;
  }

  [SerializeField] private Rigidbody2D _rigid;
  private float _force;
  private Vector2 _inputVec;
}

