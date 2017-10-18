using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Enemy))]
  public class MushroomAI : MonoBehaviour {
    void Awake() {
      _movement = new MushroomMovement();
    }

    void Update() {
      UpdateBehaviour();
    }

    void FixedUpdate() {
      _movement.FixedUpdate(_enemy.Rigid);
    }

    private void UpdateBehaviour() {

    }

    [SerializeField] private Enemy _enemy;
    [SerializeField] private MushroomMovement _movement;
  }
}

