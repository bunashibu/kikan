using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Enemy))]
  public class EnemyAI : MonoBehaviour {
    void Update() {
      if (_enemy.PhotonView.isMine && !_enemy.TargetChaseSystem.IsEnable)
        UpdateBehaviour();
    }

    void FixedUpdate() {
      _enemy.Movement.FixedUpdate(_enemy.Rigid);
    }

    private void UpdateBehaviour() {
      // temporary
      /*
      if (_enemy.BuffState.Stun)
        return;
      */

      if (!_strategyFlag) {
        ConsiderStrategy();

        MonoUtility.Instance.DelaySec(Random.value * 3, () => {
          _strategyFlag = false;
        });

        _strategyFlag = true;
      }

      float degAngle = _enemy.State.GroundAngle;

      if (_enemy.Location.IsGround) {
        if (_strategy == "MoveLeft") {
          degAngle *= _enemy.State.GroundLeft ? 1 : -1;
          _enemy.Movement.GroundMoveLeft(degAngle);
        }
        else if (_strategy == "MoveRight") {
          degAngle *= _enemy.State.GroundRight ? 1 : -1;
          _enemy.Movement.GroundMoveRight(degAngle);
        }
      }

      debugAngle = degAngle;
      debugGroundLeft = _enemy.State.GroundLeft;
    }

    private void ConsiderStrategy() {
      float rand = Random.value;

      if (rand < 0.4) {
        _strategy = "MoveRight";
        _enemy.Renderer.flipX = true;
      }
      else if (rand < 0.8) {
        _strategy = "MoveLeft";
        _enemy.Renderer.flipX = false;
      }
      else
        _strategy = "Stay";
    }

    [SerializeField] private Enemy _enemy;

    public float debugAngle;
    public bool debugGroundLeft;

    private bool _strategyFlag = false;
    public string _strategy = "Stay";
  }
}

