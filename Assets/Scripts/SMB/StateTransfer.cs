using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTransfer {
  public StateTransfer(string initState) {
    _currentState = initState;
  }

  public void TransitTo(string transitState, Animator animator) {
    animator.SetBool(transitState, true);
    animator.SetBool(_currentState, false);

    _currentState = transitState;
  }

  private string _currentState;
}

