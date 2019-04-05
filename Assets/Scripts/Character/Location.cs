using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Location : IPlayerLocation, IEnemyLocation {
    public Location(ICharacter character) {
      character.gameObject.UpdateAsObservable()
        .Subscribe(_ => UpdateRaycast(character) );
    }

    public void InitializeFoot(Collider2D footCollider) {
      _footCollider = footCollider;
    }

    public void InitializeCenter(Collider2D centerCollider) {
      _centerCollider = centerCollider;
    }

    private void UpdateRaycast(ICharacter character) {
      Vector2 footRayOrigin = new Vector2(character.FootCollider.bounds.center.x, character.FootCollider.bounds.min.y);

      float rayLength = 0.1f;
      RaycastHit2D hitGround     = Physics2D.Raycast(footRayOrigin, Vector2.down, rayLength, _groundLayer);
      RaycastHit2D hitLeftSlope  = Physics2D.Raycast(footRayOrigin, Vector2.left, rayLength, _groundLayer);
      RaycastHit2D hitRightSlope = Physics2D.Raycast(footRayOrigin, Vector2.right, rayLength, _groundLayer);

      if (IsGround) {
        UpdateGroundAngle(hitGround);
        UpdateSlopeAngle(hitLeftSlope, hitRightSlope);
      }
      else {
        GroundAngle   = 0;
        SlopeAngle    = 0;
        IsLeftSlope   = false;
        IsRightSlope  = false;
        IsLeftGround  = false;
        IsRightGround = false;
      }

      Debug.DrawRay(footRayOrigin, Vector2.left * rayLength, Color.red);
      Debug.DrawRay(footRayOrigin, Vector2.right * rayLength, Color.red);
    }

    private void UpdateGroundAngle(RaycastHit2D hitGround) {
      GroundAngle = Vector2.Angle(hitGround.normal, Vector2.up);

      if (hitGround.normal.x > 0)
        IsLeftGround = true;
      else
        IsRightGround = true;
    }

    private void UpdateSlopeAngle(RaycastHit2D hitLeftSlope, RaycastHit2D hitRightSlope) {
      if (hitLeftSlope.collider == null)
        IsLeftSlope = false;
      else {
        IsLeftSlope = true;

        float degAngle = Vector2.Angle(hitLeftSlope.normal, Vector2.up);
        SlopeAngle = degAngle;
      }

      if (hitRightSlope.collider == null)
        IsRightSlope = false;
      else {
        IsRightSlope = true;

        float degAngle = Vector2.Angle(hitRightSlope.normal, Vector2.up);
        SlopeAngle = degAngle;
      }
    }

    public bool IsGround           => _footCollider.IsTouchingLayers(_groundLayer) || IsCanNotDownGround;
    public bool IsCanNotDownGround => _footCollider.IsTouchingLayers(_canNotDownGroundLayer);
    public bool IsAir              => !IsGround;
    public bool IsLadder           => _centerCollider.IsTouchingLayers(_ladderLayer);
    public bool IsLadderTopEdge    => _centerCollider.IsTouchingLayers(_ladderTopEdgeLayer);
    public bool IsLadderBottomEdge => _centerCollider.IsTouchingLayers(_ladderBottomEdgeLayer);
    public bool IsPortal           => _centerCollider.IsTouchingLayers(_portalLayer);
    public bool IsLeftSlope   { get; private set; }
    public bool IsRightSlope  { get; private set; }
    public bool IsLeftGround  { get; private set; } // Use on a top slope
    public bool IsRightGround { get; private set; } // Use on a top slope
    public float GroundAngle  { get; private set; } // Vertical raycast
    public float SlopeAngle   { get; private set; } // Horizontal raycast

    private static readonly LayerMask _groundLayer           = LayerMask.GetMask("Ground");
    private static readonly LayerMask _canNotDownGroundLayer = LayerMask.GetMask("CanNotDownGround");
    private static readonly LayerMask _ladderLayer           = LayerMask.GetMask("Ladder");
    private static readonly LayerMask _ladderTopEdgeLayer    = LayerMask.GetMask("LadderTopEdge");
    private static readonly LayerMask _ladderBottomEdgeLayer = LayerMask.GetMask("LadderBottomEdge");
    private static readonly LayerMask _portalLayer           = LayerMask.GetMask("Portal");

    private Collider2D _footCollider;
    private Collider2D _centerCollider;
  }
}

