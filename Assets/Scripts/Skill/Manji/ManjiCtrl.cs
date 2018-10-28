using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiCtrl : Skill {
    void Awake() {
      _targetRistrictor  = new TargetRistrictor(_targetNum, _dupHitNum);
      _killDeathRecorder = new KillDeathRecorder();
    }

    void Start() {
      if (photonView.isMine)
        TranslateSkillUser();
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (!PhotonNetwork.isMasterClient)
        return;

      var targetObj = collider.gameObject;
      if (targetObj == _skillUserObj)
        return;

      if (targetObj.tag == "Player")
        ProceedAttackToPlayer(targetObj);

      if (targetObj.tag == "Enemy")
        ProceedAttackToEnemy(targetObj);
    }

    private void TranslateSkillUser() {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();
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
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();
      var halfCharaHeight = skillUser.BodyCollider.bounds.size.y / 2;

      Vector2 moveVector = direction * _moveDistance;
      Vector2 footOrigin = new Vector2(skillUser.Transform.position.x, skillUser.Transform.position.y - halfCharaHeight);

      Vector2 rayOrigin = footOrigin + moveVector;
      if (skillUser.State.Air)
        rayOrigin += new Vector2(0, halfCharaHeight * 2);

      RaycastHit2D hitGround = Physics2D.Raycast(rayOrigin, Vector2.down, halfCharaHeight * 2, _groundLayer);
      bool shouldMoveToGround = (hitGround.collider != null);

      Vector2 estimatedPosition = new Vector2(0, 0);
      if (shouldMoveToGround)
        estimatedPosition = hitGround.point + new Vector2(0, halfCharaHeight);
      else
        estimatedPosition = new Vector2(skillUser.Transform.position.x, skillUser.Transform.position.y) + moveVector;

      if (IsOutOfArea(estimatedPosition)) {
        estimatedPosition.x = Mathf.Clamp(estimatedPosition.x, StageReference.Instance.StageData.StageRect.xMin, StageReference.Instance.StageData.StageRect.xMax);
        estimatedPosition.y = Mathf.Clamp(estimatedPosition.y, StageReference.Instance.StageData.StageRect.yMin, StageReference.Instance.StageData.StageRect.yMax);
      }

      skillUser.Transform.position = estimatedPosition;

      skillUser.Rigid.velocity = new Vector2(0, 0);
    }

    [PunRPC]
    private void TranslateVertically(Vector2 direction) {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (skillUser.State.CanNotDownGround && (direction.y == -1))
        return;

      var halfCharaHeight = skillUser.BodyCollider.bounds.size.y / 2;

      transform.Rotate(0.0f, 0.0f, 90f);

      // 1.4f is ManjiCtrl AppearOffset. See Weapon Inspector
      float faceDirection = (skillUser.Renderers[0].flipX) ? 1.4f : -1.4f;
      transform.Translate(-faceDirection, direction.y * 1.4f, 0.0f, Space.World);

      Vector2 moveVector = direction * _moveDistance;
      Vector2 footOrigin = new Vector2(skillUser.Transform.position.x, skillUser.Transform.position.y - halfCharaHeight);

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
        estimatedPosition = new Vector2(skillUser.Transform.position.x, skillUser.Transform.position.y) + moveVector;

      if (IsOutOfArea(estimatedPosition)) {
        estimatedPosition.x = Mathf.Clamp(estimatedPosition.x, StageReference.Instance.StageData.StageRect.xMin, StageReference.Instance.StageData.StageRect.xMax);
        estimatedPosition.y = Mathf.Clamp(estimatedPosition.y, StageReference.Instance.StageData.StageRect.yMin, StageReference.Instance.StageData.StageRect.yMax);
      }

      skillUser.Transform.position = estimatedPosition;

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

    private void ProceedAttackToPlayer(GameObject targetObj) {
      var target = targetObj.GetComponent<BattlePlayer>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackPlayer(target, skillUser)) {
        DamageToPlayer(target, skillUser);
        //target.NumberPopupEnvironment.Popup(DamageCalculator.Damage, DamageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Player);

        if (target.Hp.Cur <= 0)
          ProceedPlayerDeath(target, skillUser);
      }
    }

    private void ProceedAttackToEnemy(GameObject targetObj) {
      var target = targetObj.GetComponent<Enemy>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackEnemy(target)) {
        DamageToEnemy(target, skillUser);
        //target.NumberPopupEnvironment.Popup(DamageCalculator.Damage, DamageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Enemy);

        if (target.Hp.Cur <= 0)
          ProceedEnemyDeath(target, skillUser);
      }
    }

    private bool IsCorrectAttackPlayer(BattlePlayer target, BattlePlayer skillUser) {
      if (target.PlayerInfo.Team == skillUser.PlayerInfo.Team)
        return false;
      if (_targetRistrictor.ShouldRistrict(target))
        return false;

      return true;
    }

    private bool IsCorrectAttackEnemy(Enemy target) {
      if (_targetRistrictor.ShouldRistrict(target))
        return false;

      return true;
    }

    private void DamageToPlayer(BattlePlayer target, BattlePlayer skillUser) {
      DamageCalculator.Calculate(_skillUserObj, _attackInfo);

      //target.Hp.Subtract(DamageCalculator.Damage);
      target.Hp.UpdateView();
    }

    private void DamageToEnemy(Enemy target, BattlePlayer skillUser) {
      DamageCalculator.Calculate(_skillUserObj, _attackInfo);

      //target.Hp.Subtract(DamageCalculator.Damage);
      target.Hp.UpdateView(skillUser.PhotonView.owner);
    }

    private void ProceedPlayerDeath(BattlePlayer target, BattlePlayer skillUser) {
      _killDeathRecorder.RecordKillDeath(target, skillUser);
    }

    private void ProceedEnemyDeath(Enemy target, BattlePlayer skillUser) {
    }

    [SerializeField] private AttackInfo _attackInfo;

    [Header("TargetSettings")]
    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;

    [Space(10)]
    [SerializeField] private LayerMask _groundLayer;

    private float _moveDistance = 3;

    private TargetRistrictor  _targetRistrictor;
    private KillDeathRecorder _killDeathRecorder;
  }
}

