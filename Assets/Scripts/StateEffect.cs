using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StateEffect : MonoBehaviour {
    void Awake() {
      StateTransfer = new StateTransfer("None", _animator);
    }

    public StateTransfer StateTransfer { get; private set; }

    [SerializeField] private Animator _animator;
  }
}

