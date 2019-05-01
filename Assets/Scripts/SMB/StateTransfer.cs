using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StateTransfer {
    public StateTransfer(string initState, Animator animator) {
      _currentState = initState;
      _animator = animator;

      _animator.SetBool(initState, true);
    }

    public void TransitTo(string transitState) {
      // INFO: Must be this order
      _animator.SetBool(_currentState, false);
      _animator.SetBool(transitState, true);

      _currentState = transitState;
    }

    private Animator _animator;
    private string _currentState;
  }
}

