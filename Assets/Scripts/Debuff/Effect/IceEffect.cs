using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class IceEffect : MonoBehaviour {
    void Start() {
      _character = transform.parent.gameObject.GetComponent<ICharacter>();
      _character.State.Slow = true;

      _iceFixSpd = new FixSpd(_slowSpd, FixSpdType.Debuff);
      _character.FixSpd.Add(_iceFixSpd);
    }

    void OnDestroy() {
      _character.State.Slow = false;
      _character.FixSpd.Remove(_iceFixSpd);
    }

    private ICharacter _character;
    private FixSpd _iceFixSpd;
    private float _slowSpd = 0.7f;
  }
}
