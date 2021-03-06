﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;
using System;

namespace Bunashibu.Kikan {
  public class PlayerLadderJumpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      if (_player.PhotonView.isMine)
        _player.AudioEnvironment.PlayOneShot("Jump", 0.4f);

      _player.FootCollider.isTrigger = true;

      _alreadyJumped = false;
      _disposable = _player.Stream.OnLadderJumped
        .Take(1)
        .Subscribe(_ => {
          _alreadyJumped = true;
        });

      _player.UpdateAsObservable()
        .Where(_ => _alreadyJumped)
        .Where(_ => _player.Rigid.velocity.y <= 0)
        .Take(1)
        .Subscribe(_ => {
          _player.FootCollider.isTrigger = false;
        });

      LadderJump();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        if (!_player.State.Rigor)
          AirMove();

        if ( _player.Debuff.State[DebuffType.Stun] ) { SyncAnimation( "Stun"  ); return; }
        if ( _player.State.Rigor                   ) { SyncAnimation( "Skill" ); return; }
        if ( _player.Location.IsGroundAbove && !_player.FootCollider.isTrigger ) { SyncAnimation( "Idle" ); return; }
      }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      _alreadyJumped = true;
      _disposable.Dispose();
    }

    private void SyncAnimation(string state) {
      _player.Synchronizer.SyncAnimation(state);
    }

    private void LadderJump() {
      if (Input.GetKey(KeyCode.LeftArrow)) {
        _player.Movement.LadderJump(Vector2.left);

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;

        return;
      }

      if (Input.GetKey(KeyCode.RightArrow)) {
        _player.Movement.LadderJump(Vector2.right);

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;

        return;
      }

      _player.Movement.LadderJump(Vector2.left);

      foreach (var sprite in _player.Renderers)
        sprite.flipX = false;

      Debug.LogError("If come here, it's bug");
    }

    private void AirMove() {
      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown) {
        _player.Movement.AirMoveLeft();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown) {
        _player.Movement.AirMoveRight();

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }
    }

    private Player _player;
    private bool _alreadyJumped;
    private IDisposable _disposable;
  }
}
