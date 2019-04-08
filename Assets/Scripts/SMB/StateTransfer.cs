using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StateTransfer {
    public StateTransfer(string initState, Animator animator) {
      _currentState = initState;
      animator.SetBool(initState, true);
    }

    public void TransitTo(string transitState, Animator animator) {
      // INFO: Must be this order
      animator.SetBool(_currentState, false);
      animator.SetBool(transitState, true);

      _currentState = transitState;
    }

    private string _currentState;
  }
}

