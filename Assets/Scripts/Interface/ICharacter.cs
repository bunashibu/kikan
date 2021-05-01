using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public interface ICharacter : IPhotonBehaviour {
    Rigidbody2D    Rigid        { get; }
    BoxCollider2D  BodyCollider { get; }
    Collider2D     FootCollider { get; }
    CharacterState State        { get; }
    ReactiveCollection<FixSpd> FixSpd { get; }
  }
}
