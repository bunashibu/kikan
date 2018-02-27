using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class NoneSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_character == null) {
        _character = animator.transform.parent.GetComponent<ICharacter>();
        _stateEffect = animator.GetComponent<StateEffect>();
        _transform = animator.GetComponent<Transform>();
      }

      _transform.position = _transform.parent.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_character.State.Stun) { _stateEffect.StateTransfer.TransitTo("Stun", animator); }
    }

    private ICharacter _character;
    private StateEffect _stateEffect;
    private Transform _transform;
  }
}

