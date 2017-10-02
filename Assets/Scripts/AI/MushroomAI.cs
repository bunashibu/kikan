using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Enemy))]
  public class MushroomAI : MonoBehaviour {
    /*
    void Awake() {
      Movement = new LinearJumpEnemyMovement();
      Movement.SetLinearMoveForce(_linearMoveForce);
      Movement.SetJumpForce(_jumpForce);
    }

    void Update() {
      //Movement.GroundMoveLeft();
    }

    void FixedUpdate() {
      Movement.FixedUpdate(_enemy.Rigid);
    }
    */

    public LinearJumpEnemyMovement Movement { get; private set; }

    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _linearMoveForce;
    [SerializeField] private float _jumpForce;
  }
}

