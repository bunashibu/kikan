using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  // Dirty
  public class TargetChaseSystem : MonoBehaviour {
    void Awake() {
      _enemy = GetComponent<Enemy>();
    }

    void Update() {
      if ( _enemy.Debuff.State[DebuffType.Stun] )
        return;
      if ( _enemy.Debuff.State[DebuffType.Slip] )
        return;

      if (!IsEnable)
        return;

      if (_isRush) {
        if (_leftFlag)
          RushLeft();
        else
          RushRight();

        return;
      }

      float distance = Vector3.Distance(transform.position, _targetTrans.position);
      if (distance > 10.0f)
        return;

      if (Math.Abs(transform.position.x - _targetTrans.position.x) < 2.0f) {
        _isRush = true;
        _leftFlag = _targetTrans.position.x < transform.position.x;
        MonoUtility.Instance.DelaySec(3.0f, () => { _isRush = false; });
        return;
      }

      if (Time.time - _firstTime > 120.0f) {
        IsEnable = false;
        return;
      }

      Chase();
    }

    private void Chase() {
      float degAngle = _enemy.Location.GroundAngle;

      if (_enemy.Location.IsGround) {
        if (_targetTrans.position.x < transform.position.x)
          ChaseLeft(degAngle);
        else if (_targetTrans.position.x > transform.position.x)
          ChaseRight(degAngle);
      }
    }

    private void RushLeft() {
      float degAngle = _enemy.Location.GroundAngle;

      if (_enemy.Location.IsGround)
        ChaseLeft(degAngle);
    }

    private void RushRight() {
      float degAngle = _enemy.Location.GroundAngle;

      if (_enemy.Location.IsGround)
        ChaseRight(degAngle);
    }

    private void ChaseLeft(float degAngle) {
      degAngle *= _enemy.Location.IsLeftSlope ? 1 : -1;
      _enemy.Movement.GroundMoveLeft(degAngle);
      _enemy.Renderer.flipX = false;
    }

    private void ChaseRight(float degAngle) {
      degAngle *= _enemy.Location.IsRightSlope ? 1 : -1;
      _enemy.Movement.GroundMoveRight(degAngle);
      _enemy.Renderer.flipX = true;
    }

    public void SetChaseTarget(Transform targetTrans) {
      _targetTrans = targetTrans;
      IsEnable = true;
      _firstTime = Time.time;
    }

    public bool IsEnable { get; private set; }

    private Enemy _enemy;
    private Transform _targetTrans;
    private float _firstTime;

    private bool _isRush;
    private bool _leftFlag;
  }
}
