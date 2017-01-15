using UnityEngine;
using System.Collections;

public class StepDownJumpSystem : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _rigid.AddForce(Vector2.up * _force);
      _actFlag = false;
    }
  }

  public void StepDown() {
    _actFlag = true;
    _collider.isTrigger = true;
    // ImplThroughGround
  }

  public void SetForce(float force) {
    _force = force;
  }

  public bool CanUse {
    get {
      return _state.LieDown;
    }
  }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private RigidState _state;
  private float _force;
  private bool _actFlag;
}

