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

      _heavyFixSpd = new FixSpd(_heavySpd, FixSpdType.Debuff);
      _character.FixSpd.Add(_heavyFixSpd);
    }

    void OnDestroy() {
      _character.State.Heavy = false;
      _character.FixSpd.Remove(_heavyFixSpd);
    }

    private ICharacter _character;
    private FixSpd _heavyFixSpd;
    private float _heavySpd = 0.85f;
  }
}
