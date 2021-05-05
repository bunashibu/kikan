using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SlipEffect : MonoBehaviour {
    void Start() {
      _character = transform.parent.gameObject.GetComponent<ICharacter>();
      _character.FootCollider.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Friction/Friction0");
    }

    void OnDestroy() {
      _character.FootCollider.sharedMaterial = (PhysicsMaterial2D)Resources.Load("Friction/Friction2_3");
    }

    private ICharacter _character;
  }
}
