using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Rigidbody2D))]
  [RequireComponent(typeof(BoxCollider2D))]
  public class Character2D : MonoBehaviour {
    void Awake() {
      _character = gameObject.GetComponent<ICharacter>();
      State = new CharacterState();
    }

    void Update() {
      UpdateRaycast();

      if (State.IsGround)
        CalculateFriction();
    }

    private void UpdateRaycast() {
      Vector2 footRayOrigin = new Vector2(_character.Collider.bounds.center.x, _character.Collider.bounds.min.y);

      RaycastHit2D hitGround;
      bool isGround;
      UpdateGroundRaycast(footRayOrigin, out hitGround, out isGround);

      RaycastHit2D hitLeftSlope;
      bool isLeftSlope;
      UpdateSlopeRaycast(Vector2.left, footRayOrigin, out hitLeftSlope, out isLeftSlope);

      RaycastHit2D hitRightSlope;
      bool isRightSlope;
      UpdateSlopeRaycast(Vector2.right, footRayOrigin, out hitRightSlope, out isRightSlope);

      if (isGround) {
        if (isLeftSlope)
          ApplySlopeSettings(hitLeftSlope);
        else if (isRightSlope)
          ApplySlopeSettings(hitRightSlope);
        else
          ApplyGroundSettings(hitGround);

      } else {
        ApplyAirSettings();
      }
    }

    private void UpdateGroundRaycast(Vector2 footRayOrigin, out RaycastHit2D hitGround, out bool isGround) {
      hitGround = Physics2D.Raycast(footRayOrigin, Vector2.down, Mathf.Abs(_character.Rigid.velocity.y), _groundMask);
      isGround  = (hitGround.collider != null) && (hitGround.distance == 0);
      Debug.DrawRay(footRayOrigin, Vector2.down * Mathf.Abs(_character.Rigid.velocity.y), Color.red);
    }

    private void UpdateSlopeRaycast(Vector2 direction, Vector2 footRayOrigin, out RaycastHit2D hitSlope, out bool isSlope) {
      float rayLength = Mathf.Abs(_character.Rigid.velocity.x); // divide by XX FPS

      hitSlope = Physics2D.Raycast(footRayOrigin, direction, rayLength, _slopeMask);
      isSlope  = (hitSlope.collider != null) && (hitSlope.distance == 0);
      Debug.DrawRay(footRayOrigin, direction * rayLength, Color.red);
    }

    private void ApplyGroundSettings(RaycastHit2D hitGround) {
      _character.Rigid.velocity = new Vector2(_character.Rigid.velocity.x, 0);
      transform.position        = new Vector3(transform.position.x, hitGround.collider.bounds.max.y + _character.Collider.bounds.extents.y, 0);
      _character.Rigid.gravityScale = 0;
      State.ToGround();
    }

    private void ApplySlopeSettings(RaycastHit2D hitSlope) {
      float angle = Vector2.Angle(hitSlope.normal, Vector2.up);
      Debug.Log(angle);
    }

    private void ApplyAirSettings() {
      _character.Rigid.gravityScale = 1;
      State.ToAir();
    }

    private void CalculateFriction() {
      var     movingDirection   = new Vector2(Mathf.Sign(_character.Rigid.velocity.x), 0);
      Vector2 estimatedVelocity = _character.Rigid.velocity - (movingDirection * _friction);

      if (Mathf.Sign(estimatedVelocity.x) == movingDirection.x)
        _character.Rigid.velocity = estimatedVelocity;
      else
        _character.Rigid.velocity = new Vector2(0, 0);
    }

    public CharacterState State { get; private set; }

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _slopeMask;
    [Range(0, 1)]
    [SerializeField] private float _friction;
    [Range(0, 90)]
    [SerializeField] private float _maxClimbableAngle;
    private ICharacter _character;
  }
}

