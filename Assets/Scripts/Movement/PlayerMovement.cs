using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class PlayerMovement {
    private PlayerMovement() {
      _airMove      = new AirMove();
      _groundMove   = new GroundMove();
      _ladderMove   = new LadderMove();

      _groundJump   = new GroundJump();
      _ladderJump   = new LadderJump();
      _stepDownJump = new StepDownJump();

      _lieDown      = new LieDown();
    }

    private PlayerMovement(GameObject playerObj) : this() {
      playerObj.FixedUpdateAsObservable()
        .Subscribe(_ => FixedUpdate() )
        .AddTo(playerObj);
    }

    private void FixedUpdate() {
      if (_core == null) {
        _airMove.FixedUpdate(_rigid);
        _groundMove.FixedUpdate(_rigid);

        _groundJump.FixedUpdate(_rigid);
        _ladderJump.FixedUpdate(_rigid);
      }
      else {
        _airMove.FixedUpdate(_rigid, _core);
        _groundMove.FixedUpdate(_rigid, _core);

        _groundJump.FixedUpdate(_rigid, _core);
        _ladderJump.FixedUpdate(_rigid, _core);
      }

      _ladderMove.FixedUpdate(_trans);
      _stepDownJump.FixedUpdate(_rigid);
    }

    public PlayerMovement(ILobbyMovementPlayer player) : this(player.gameObject) {
      _trans = player.transform;
      _rigid = player.Rigid;
    }

    public PlayerMovement(IBattleMovementPlayer player) : this(player.gameObject) {
      _trans = player.transform;
      _rigid = player.Rigid;
      _core  = player.Core;
    }

    public void AirMoveLeft() {
      _airMove.MoveLeft();
    }

    public void AirMoveRight() {
      _airMove.MoveRight();
    }

    public void GroundMoveLeft(float degAngle = 0) {
      _groundMove.MoveLeft(degAngle);
    }

    public void GroundMoveRight(float degAngle = 0) {
      _groundMove.MoveRight(degAngle);
    }

    public void LadderMoveUp() {
      _ladderMove.MoveUp();
    }

    public void LadderMoveDown() {
      _ladderMove.MoveDown();
    }

    public void GroundJump() {
      _groundJump.Jump();
    }

    public void LadderJump() {
      _ladderJump.JumpOff();
    }

    public void StepDownJump() {
      _stepDownJump.StepDown();
    }

    public void LieDown(BoxCollider2D collider) {
      _lieDown.Lie(collider);
    }

    public void Stand(BoxCollider2D collider) {
      _lieDown.Stand(collider);
    }

    public void SetMoveForce(float force) {
      _groundMove.SetForce(force);
    }

    public void SetJumpForce(float force) {
      _groundJump.SetForce(force);
    }

    private Transform   _trans;
    private Rigidbody2D _rigid;
    private Core        _core;

    private AirMove    _airMove;
    private GroundMove _groundMove;
    private LadderMove _ladderMove;

    private GroundJump   _groundJump;
    private LadderJump   _ladderJump;
    private StepDownJump _stepDownJump;

    private LieDown _lieDown;
  }
}

