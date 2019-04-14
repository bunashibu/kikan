using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SlowEffect : MonoBehaviour {
    void Start() {
      _character = transform.parent.gameObject.GetComponent<ICharacter>();
      _character.State.Slow = true;

      _slowFixSpd = new FixSpd(_slowSpd, FixSpdType.Debuff);
      _character.FixSpd.Add(_slowFixSpd);
    }

    void OnDestroy() {
      _character.State.Slow = false;
      _character.FixSpd.Remove(_slowFixSpd);
    }

    private ICharacter _character;
    private FixSpd _slowFixSpd;
    private float _slowSpd = 0.85f;
  }
}

