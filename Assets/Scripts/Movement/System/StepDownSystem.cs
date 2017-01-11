using UnityEngine;
using System.Collections;

public class StepDownSystem : MonoBehaviour {
  public void StepDown() {
    _system.Jump();
  }

  [SerializeField] private JumpSystem _system;
}

