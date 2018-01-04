using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiCtrl : Skill {
    void Awake() {
      _rewardGetter      = new RewardGetter();
      _targetRistrictor  = new TargetRistrictor(_targetNum, _dupHitNum);
      _killDeathRecorder = new KillDeathRecorder();
      _powerCalculator   = new PowerCalculator();
      _damageCalculator  = new DamageCalculator();
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

      if (Input.GetKey(KeyCode.UpArrow))
        photonView.RPC("TranslateVertical", PhotonTargets.All, Vector2.up);
      else if (Input.GetKey(KeyCode.DownArrow))
        photonView.RPC("TranslateVertical", PhotonTargets.All, Vector2.down);
      else if (skillUser.Renderers[0].flipX)
        photonView.RPC("TranslateHorizontal", PhotonTargets.All, Vector2.right);
      else
        photonView.RPC("TranslateHorizontal", PhotonTargets.All, Vector2.left);
    }

    [PunRPC]
    private void TranslateHorizontal(Vector2 direction) {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();
      var halfCharaHeight = skillUser.BodyCollider.bounds.size.y / 2;

      Vector2 moveVector = direction * _moveDistance;
      Vector2 footOrigin = new Vector2(skillUser.Transform.position.x, skillUser.Transform.position.y - halfCharaHeight);

      if (skillUser.State.Ground) {
        RaycastHit2D hitGround = Physics2D.Raycast(footOrigin + moveVector, Vector2.down, halfCharaHeight * 2, _groundLayer);
        bool shouldMoveToGround = (hitGround.collider != null);

        if (shouldMoveToGround)
          skillUser.Transform.position = hitGround.point + new Vector2(0, halfCharaHeight);
        else
          skillUser.Transform.Translate(moveVector);

      } else if (skillUser.State.Air) {
        RaycastHit2D hitGround = Physics2D.Raycast(footOrigin + moveVector + new Vector2(0, halfCharaHeight * 2), Vector2.down, halfCharaHeight * 2, _groundLayer);
        bool shouldMoveToGround = (hitGround.collider != null);

        if (shouldMoveToGround)
          skillUser.Transform.position = hitGround.point + new Vector2(0, halfCharaHeight);
        else
          skillUser.Transform.Translate(moveVector);
      }

      skillUser.Rigid.velocity = new Vector2(0, 0);
    }

    [PunRPC]
    private void TranslateVertical(Vector2 direction) {
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();
      var halfCharaHeight = skillUser.BodyCollider.bounds.size.y / 2;

      if (skillUser.State.CanNotDownGround && (direction.y == -1))
        return;

      transform.Rotate(0.0f, 0.0f, 90f);

      // 1.4f is ManjiCtrl AppearOffset. See Weapon Inspector
      float faceDirection = (skillUser.Renderers[0].flipX) ? 1.4f : -1.4f;
      transform.Translate(-faceDirection, direction.y * 1.4f, 0.0f, Space.World);

      Vector2 moveVector = direction * _moveDistance;
      Vector2 footOrigin = new Vector2(skillUser.Transform.position.x, skillUser.Transform.position.y - halfCharaHeight);

      if (direction == Vector2.up) {
        RaycastHit2D hitGround = Physics2D.Raycast(footOrigin + moveVector, -direction, _moveDistance, _groundLayer);
        bool shouldMoveToGround = (hitGround.collider != null) && (!skillUser.FootCollider.IsTouching(hitGround.collider));

        if (shouldMoveToGround)
          skillUser.Transform.position = hitGround.point + new Vector2(0, halfCharaHeight);
        else
          skillUser.Transform.Translate(moveVector);

      } else if (direction == Vector2.down) {
        RaycastHit2D[] hitGroundAry = Physics2D.RaycastAll(footOrigin, direction, _moveDistance, _groundLayer);
        bool shouldMoveToGround = (hitGroundAry.Length > 0) && (!skillUser.FootCollider.IsTouching(hitGroundAry.Last().collider));

        if (shouldMoveToGround)
          skillUser.Transform.position = hitGroundAry.Last().point + new Vector2(0, halfCharaHeight);
        else
          skillUser.Transform.Translate(moveVector);
      }

      skillUser.Rigid.velocity = new Vector2(0, 0);
    }

    private void ProceedAttackToPlayer(GameObject targetObj) {
      var target = targetObj.GetComponent<BattlePlayer>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackPlayer(target, skillUser)) {
        DamageToPlayer(target, skillUser);
        target.NumberPopupEnvironment.Popup(_damageCalculator.Damage, _damageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Player);

        if (target.Hp.Cur <= 0)
          ProceedPlayerDeath(target, skillUser);
      }
    }

    private void ProceedAttackToEnemy(GameObject targetObj) {
      var target = targetObj.GetComponent<Enemy>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackEnemy(target)) {
        DamageToEnemy(target, skillUser);
        target.NumberPopupEnvironment.Popup(_damageCalculator.Damage, _damageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Enemy);

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
      int playerPower = _powerCalculator.CalculatePlayerPower(skillUser);
      int attackPower = (int)(playerPower * (_skillPower / 100.0));

      int damage = _damageCalculator.CalculateDamage(attackPower, _maxDeviation, skillUser.Core.Critical);

      target.Hp.Subtract(damage);
      target.Hp.UpdateView();
    }

    private void DamageToEnemy(Enemy target, BattlePlayer skillUser) {
      int playerPower = _powerCalculator.CalculatePlayerPower(skillUser);
      int attackPower = (int)(playerPower * (_skillPower / 100.0));

      int damage = _damageCalculator.CalculateDamage(attackPower, _maxDeviation, skillUser.Core.Critical);

      target.Hp.Subtract(damage);
      target.Hp.UpdateView(skillUser.PhotonView.owner);
    }

    private void ProceedPlayerDeath(BattlePlayer target, BattlePlayer skillUser) {
      _rewardGetter.SetRewardReceiver(skillUser);
      _rewardGetter.GetRewardFrom(target);

      _killDeathRecorder.RecordKillDeath(target, skillUser);
    }

    private void ProceedEnemyDeath(Enemy target, BattlePlayer skillUser) {
      _rewardGetter.SetRewardReceiver(skillUser);
      _rewardGetter.GetRewardFrom(target);
    }

    [Header("PowerSettings")]
    [SerializeField] private int _skillPower;
    [SerializeField] private int _maxDeviation;

    [Header("TargetSettings")]
    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;

    [Space(10)]
    [SerializeField] private LayerMask _groundLayer;

    private float _moveDistance = 3;

    private RewardGetter      _rewardGetter;
    private TargetRistrictor  _targetRistrictor;
    private KillDeathRecorder _killDeathRecorder;
    private PowerCalculator   _powerCalculator;
    private DamageCalculator  _damageCalculator;
  }
}

