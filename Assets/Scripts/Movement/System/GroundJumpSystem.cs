using UnityEngine;
using System.Collections;

public class GroundJumpSystem : MonoBehaviour {
  void FixedUpdate() {
    if (_actFlag) {
      _rigid.AddForce(Vector2.up * _force);
      _actFlag = false;
    }
  }

  public void Jump() {
    _actFlag = true;
  }

  public void SetForce(float force) {
    _force = force;
  }

  public bool CanUse {
    get { return _state.Ground && _state.Stand; }
  }

  [SerializeField] private Rigidbody2D _rigid;
  [SerializeField] private RigidState _state;
  private float _force;
  private bool _actFlag;
}

