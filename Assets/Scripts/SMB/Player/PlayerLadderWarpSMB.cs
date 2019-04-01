using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerLadderWarpSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      if (_player.PhotonView.isMine)
        _player.AudioEnvironment.PlayOneShot("Jump");

      PrepareEasing();

      _isMoved = false;
      MonoUtility.Instance.DelaySec(_duration, () => { _isMoved = true; } );
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player.PhotonView.isMine) {
        Move();

        if (_isMoved) {
          if ( _player.Debuff.State[DebuffType.Stun] )                   { _player.StateTransfer.TransitTo( "Stun" , animator ); return; }
          if ( ShouldTransitToSkill()                )                   { _player.StateTransfer.TransitTo( "Skill", animator ); return; }
          if ( _player.Location.IsAir    && !_player.Location.IsLadder ) { _player.StateTransfer.TransitTo( "Fall" , animator ); return; }
          if ( _player.Location.IsGround && !_player.Location.IsLadder ) { _player.StateTransfer.TransitTo( "Idle" , animator ); return; }
        }
      }
    }

    private Vector3 GetDestination() {
      Vector3 destination = new Vector3(_player.transform.position.x, 0, 0);
      Collider2D[] results = new Collider2D[1];

      var filter = new ContactFilter2D();
      filter.SetLayerMask(LayerMask.GetMask("Ground", "CanNotDownGround"));

      _player.BodyCollider.OverlapCollider(filter, results);

      if (results.Length > 0)
        destination.y = results[0].bounds.center.y + 0.5f + 0.2f; // 0.5f == player sprite half; 0.2f == grand half + alpha
      else
        Debug.Log("Error in PlayerLaderJumpSMB GetDestination()");

      return destination;
    }

    private void PrepareEasing() {
      var destination = GetDestination();

      bool OnlyLeftKeyDown = Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
      if (OnlyLeftKeyDown) {
        destination.x += -1;

        foreach (var sprite in _player.Renderers)
          sprite.flipX = false;
      }

      bool OnlyRightKeyDown = Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow);
      if (OnlyRightKeyDown) {
        destination.x += 1;

        foreach (var sprite in _player.Renderers)
          sprite.flipX = true;
      }

      _easing = new QuadraticEaseInOut(_player.transform.position, destination, _duration);
    }

    private void Move() {
      _player.transform.position = _easing.GetNextPosition();
    }

    private bool ShouldTransitToSkill() {
      bool SkillFlag = ( _player.SkillInfo.GetState ( SkillName.X     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Shift ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Z     ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Ctrl  ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Space ) == SkillState.Using ) ||
                       ( _player.SkillInfo.GetState ( SkillName.Alt   ) == SkillState.Using );

      return SkillFlag;
    }

    private Player _player;
    private QuadraticEaseInOut _easing;
    private bool _isMoved;
    private float _duration = 0.3f;
  }
}

