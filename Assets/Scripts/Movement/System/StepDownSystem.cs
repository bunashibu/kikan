using UnityEngine;
using System.Collections;

public class StepDownSystem : MonoBehaviour {
  public void StepDown() {
    _system.SetForce(_force);
    _system.Jump();
    // ImplThroughGround
  }

  public void SetForce(float force) {
    _force = force;
  }

  [SerializeField] private GroundJumpSystem _system;
  public bool CanUse { get; private set; }
  private float _force;
}

