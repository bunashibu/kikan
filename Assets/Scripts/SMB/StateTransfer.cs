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
      animator.SetBool(transitState, true);
      animator.SetBool(_currentState, false);
  
      _currentState = transitState;
    }
  
    private string _currentState;
  }
}

