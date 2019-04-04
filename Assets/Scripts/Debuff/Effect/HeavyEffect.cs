using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class HeavyEffect : MonoBehaviour {
    void Start() {
      _character = transform.parent.gameObject.GetComponent<ICharacter>();
      _character.State.Heavy = true;
    }

    void OnDestroy() {
      _character.State.Heavy = false;
    }

    private ICharacter _character;
  }
}

