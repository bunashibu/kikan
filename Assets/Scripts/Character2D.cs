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
      RaycastHit2D hitGround = Physics2D.Raycast(footRayOrigin, Vector2.down, Mathf.Abs(_character.Rigid.velocity.y), _groundMask);
      Debug.DrawRay(footRayOrigin, Vector2.down * Mathf.Abs(_character.Rigid.velocity.y), Color.red);

      bool isGround = (hitGround.collider != null) && (hitGround.distance == 0);

      if (isGround)
        ApplyGroundSettings(hitGround);
      else
        ApplyAirSettings();
    }

    private void ApplyGroundSettings(RaycastHit2D hitGround) {
      _character.Rigid.velocity = new Vector2(_character.Rigid.velocity.x, 0);
      _character.Rigid.gravityScale = 0;
      transform.position = new Vector3(transform.position.x, hitGround.collider.bounds.max.y + _character.Collider.bounds.extents.y, 0);
      State.ToGround();
    }

    private void ApplyAirSettings() {
      _character.Rigid.gravityScale = 1;
      State.ToAir();
    }

    private void CalculateFriction() {
      var movingDirection = new Vector2(Mathf.Sign(_character.Rigid.velocity.x), 0);
      Vector2 estimatedVelocity = _character.Rigid.velocity - (movingDirection * _friction);

      if (Mathf.Sign(estimatedVelocity.x) == movingDirection.x)
        _character.Rigid.velocity = estimatedVelocity;
      else
        _character.Rigid.velocity = new Vector2(0, 0);
    }

    public CharacterState State { get; private set; }

    [SerializeField] private LayerMask _groundMask;
    [Range(0, 1)]
    [SerializeField] private float _friction;
    private ICharacter _character;
  }
}

