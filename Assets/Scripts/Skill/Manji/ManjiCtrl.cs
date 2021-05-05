using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class ManjiCtrl : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);
    }

    void Start() {
      if (photonView.isMine)
        TranslateSkillUser();
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Manji);
      }
    }

    private void TranslateSkillUser() {
      var skillUser = _skillUserObj.GetComponent<Player>();
      skillUser.FootCollider.isTrigger = false;

      if (Input.GetKey(KeyCode.UpArrow))
        photonView.RPC("TranslateVertically", PhotonTargets.All, Vector2.up);
      else if (Input.GetKey(KeyCode.DownArrow))
        photonView.RPC("TranslateVertically", PhotonTargets.All, Vector2.down);
      else if (skillUser.Renderers[0].flipX)
        photonView.RPC("TranslateHorizontally", PhotonTargets.All, Vector2.right);
      else
        photonView.RPC("TranslateHorizontally", PhotonTargets.All, Vector2.left);
    }

    [PunRPC]
    private void TranslateHorizontally(Vector2 direction) {
      var skillUser = _skillUserObj.GetComponent<Player>();
      var halfCharaHeight = skillUser.BodyCollider.bounds.size.y / 2;

      Vector2 moveVector = direction * _moveDistance;
      Vector2 footOrigin = new Vector2(skillUser.transform.position.x, skillUser.transform.position.y - halfCharaHeight);

      Vector2 rayOrigin = footOrigin + moveVector;
      if (skillUser.Location.IsAir)
        rayOrigin += new Vector2(0, halfCharaHeight * 2);

      RaycastHit2D hitGround = Physics2D.Raycast(rayOrigin, Vector2.down, halfCharaHeight * 2, _groundLayer);
      bool shouldMoveToGround = (hitGround.collider != null);

      Vector2 estimatedPosition = new Vector2(0, 0);
      if (shouldMoveToGround)
        estimatedPosition = hitGround.point + new Vector2(0, halfCharaHeight);
      else
        estimatedPosition = new Vector2(skillUser.transform.position.x, skillUser.transform.position.y) + moveVector;

      if (IsOutOfArea(estimatedPosition)) {
        estimatedPosition.x = Mathf.Clamp(estimatedPosition.x, StageReference.Instance.StageData.StageRect.xMin, StageReference.Instance.StageData.StageRect.xMax);
        estimatedPosition.y = Mathf.Clamp(estimatedPosition.y, StageReference.Instance.StageData.StageRect.yMin, StageReference.Instance.StageData.StageRect.yMax);
      }

      skillUser.transform.position = estimatedPosition;

      skillUser.Rigid.velocity = new Vector2(0, 0);
    }

    [PunRPC]
    private void TranslateVertically(Vector2 direction) {
      var skillUser = _skillUserObj.GetComponent<Player>();

      var halfCharaHeight = skillUser.BodyCollider.bounds.size.y / 2;

      if (direction == Vector2.up)
        transform.Rotate(0.0f, 0.0f, 270f);
      else if (direction == Vector2.down)
        transform.Rotate(0.0f, 0.0f, 90f);

      _boxCollider.size = new Vector2(4f, 0.2f);
      _boxCollider.offset = new Vector2(-0.5f, 0);

      // 1.4f is ManjiCtrl AppearOffset. See Weapon Inspector
      float faceDirection = (skillUser.Renderers[0].flipX) ? 1.4f : -1.4f;
      transform.Translate(-faceDirection, direction.y * 1.4f, 0.0f, Space.World);

      if (skillUser.Location.IsCanNotDownGround && (direction.y == -1))
        return;

      Vector2 moveVector = direction * _moveDistance;
      Vector2 footOrigin = new Vector2(skillUser.transform.position.x, skillUser.transform.position.y - halfCharaHeight);

      RaycastHit2D hitGround = new RaycastHit2D();
      if (direction == Vector2.up) {
        hitGround = Physics2D.Raycast(footOrigin + moveVector, Vector2.down, _moveDistance, _groundLayer);
      }
      else if (direction == Vector2.down) {
        RaycastHit2D[] hitGroundAry = Physics2D.RaycastAll(footOrigin, Vector2.down, _moveDistance, _groundLayer);
        if (hitGroundAry.Length > 0)
          hitGround = hitGroundAry.Last();
      }

      bool shouldMoveToGround = (hitGround.collider != null) && (!skillUser.FootCollider.IsTouching(hitGround.collider));

      Vector2 estimatedPosition = new Vector2(0, 0);
      if (shouldMoveToGround)
        estimatedPosition = hitGround.point + new Vector2(0, halfCharaHeight);
      else
        estimatedPosition = new Vector2(skillUser.transform.position.x, skillUser.transform.position.y) + moveVector;

      if (IsOutOfArea(estimatedPosition)) {
        estimatedPosition.x = Mathf.Clamp(estimatedPosition.x, StageReference.Instance.StageData.StageRect.xMin, StageReference.Instance.StageData.StageRect.xMax);
        estimatedPosition.y = Mathf.Clamp(estimatedPosition.y, StageReference.Instance.StageData.StageRect.yMin, StageReference.Instance.StageData.StageRect.yMax);
      }

      skillUser.transform.position = estimatedPosition;

      skillUser.Rigid.velocity = new Vector2(0, 0);
    }

    private bool IsOutOfArea(Vector2 vector) {
      var x = vector.x;
      var y = vector.y;

      if (x < StageReference.Instance.StageData.StageRect.xMin || StageReference.Instance.StageData.StageRect.xMax < x ||
          y < StageReference.Instance.StageData.StageRect.yMin || StageReference.Instance.StageData.StageRect.yMax < y)
        return true;

      return false;
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private BoxCollider2D _boxCollider;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;

    private float _moveDistance = 3;
  }
}
