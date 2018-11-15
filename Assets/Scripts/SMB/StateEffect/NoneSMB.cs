using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class NoneSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_character == null) {
        _character = animator.transform.parent.GetComponent<ICharacter>();
        //_buffEffect = animator.GetComponent<BuffEffect>();
        _transform = animator.GetComponent<Transform>();
      }

      _transform.position = _transform.parent.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      // if (_character.BuffState.Stun) { _buffEffect.StateTransfer.TransitTo("Stun", animator); }
    }

    private ICharacter _character;
    private BuffEffect _buffEffect;
    private Transform _transform;
  }
}

