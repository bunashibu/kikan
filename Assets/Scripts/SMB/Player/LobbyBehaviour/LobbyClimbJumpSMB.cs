using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyClimbJumpSMB : StateMachineBehaviour {
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView == null) {
      _photonView = animator.GetComponent<PhotonView>();
      _renderers  = animator.GetComponentsInChildren<SpriteRenderer>();
      _movement   = animator.GetComponent<LobbyPlayer>().Movement;

      //_stateTransfer = new StateTransfer();
    }

    ClimbJump();
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (_photonView.isMine) {
      if ( ShouldTransitToFall() ) { _stateTransfer.TransitTo( "Fall" , animator ); return; }
    }
  }

  private void ClimbJump() {
    bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
    if (OnlyLeftKeyDown) {
      _movement.GroundMoveLeft();

      foreach (var sprite in _renderers)
        sprite.flipX = false;
    }

    bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
    if (OnlyRightKeyDown) {
      _movement.GroundMoveRight();

      foreach (var sprite in _renderers)
        sprite.flipX = true;
    }

    _movement.ClimbJump();
  }

  private bool ShouldTransitToFall() {
    return false;
  }

  private PhotonView _photonView;
  private SpriteRenderer[] _renderers; // INFO: [PlayerSprite, WeaponSprite]
  private LobbyPlayerMovement _movement;

  private StateTransfer _stateTransfer;
}

