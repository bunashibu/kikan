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

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private float _force;
  private Vector2 _inputVec;
}

