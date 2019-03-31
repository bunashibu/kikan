using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Location : IPlayerLocation, IEnemyLocation {
    public Location(ICharacter character) {
      character.gameObject.UpdateAsObservable()
        .Subscribe(_ => UpdateGroundRaycast(character) );
    }

    public void InitializeFoot(Collider2D footCollider) {
      _footCollider = footCollider;
    }

    public void InitializeCenter(Collider2D centerCollider) {
      _centerCollider = centerCollider;
    }

    // 左右にもray飛ばす
    private void UpdateGroundRaycast(ICharacter character) {
      Vector2 footRayOrigin = new Vector2(character.FootCollider.bounds.center.x, character.FootCollider.bounds.min.y);

      float rayLength = 0.1f + Mathf.Abs(character.Rigid.velocity.y) * Time.deltaTime;
      RaycastHit2D hitGround = Physics2D.Raycast(footRayOrigin, Vector2.down, rayLength, _groundLayer);

      if (IsGround) {
        float degAngle = Vector2.Angle(hitGround.normal, Vector2.up);
        if (degAngle == 90)
          degAngle = 0;
        GroundAngle = degAngle;

        if (degAngle > 0 && degAngle < 90) {
          float sign = Mathf.Sign(hitGround.normal.x);
          IsLeftSlope  = (sign == 1 ) ? true : false;
          IsRightSlope = (sign == -1) ? true : false;
        }
        else {
          IsLeftSlope  = false;
          IsRightSlope = false;
        }
      }
      else {
        GroundAngle = 0;
        IsLeftSlope  = false;
        IsRightSlope = false;
      }

      Debug.DrawRay(footRayOrigin, Vector2.down * rayLength, Color.red);
    }

    public bool IsGround           => _footCollider.IsTouchingLayers(_groundLayer) || IsCanNotDownGround;
    public bool IsCanNotDownGround => _footCollider.IsTouchingLayers(_canNotDownGroundLayer);
    public bool IsAir              => !IsGround;
    public bool IsLadder           => _centerCollider.IsTouchingLayers(_ladderLayer);
    public bool IsLadderTopEdge    => _centerCollider.IsTouchingLayers(_ladderTopEdgeLayer);
    public bool IsLadderBottomEdge => _centerCollider.IsTouchingLayers(_ladderBottomEdgeLayer);
    public bool IsPortal           => _centerCollider.IsTouchingLayers(_portalLayer);
    public bool IsLeftSlope  { get; private set; }
    public bool IsRightSlope { get; private set; }
    public float GroundAngle { get; private set; }

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

