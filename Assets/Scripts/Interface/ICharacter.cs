using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter {
  Rigidbody2D   Rigid    { get; }
  BoxCollider2D Collider { get; }
}

