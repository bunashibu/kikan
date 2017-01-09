using UnityEngine;
using System.Collections;

public abstract class MovementSystem : MonoBehaviour {
  [SerializeField] protected Rigidbody2D _rigid;
  protected Vector2 _inputVec;
}

