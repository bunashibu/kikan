using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Bunashibu.Kikan {
  public class HeavyEffect : MonoBehaviour {
    void Start() {
      _character = transform.parent.gameObject.GetComponent<ICharacter>();
      _character.State.Heavy = true;
      _character.FixSpd.Add(_heavySpd);
    }

    void OnDestroy() {
      _character.State.Heavy = false;
      _character.FixSpd.Remove(_heavySpd);
    }

    private ICharacter _character;
    private float _heavySpd = 0.85f;
  }
}

