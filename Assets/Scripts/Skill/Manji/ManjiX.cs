using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiX : Skill {
    void Awake() {
      _targetChecker    = new TargetChecker(_skillUserObj);
      _damageCalculater = new DamageCalculator(_increasePercent, _maxDeviation);
      _notifier         = new Notifier();
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient && _targetChecker.IsAttackTarget(collider))
        _notifier.Notify(Notification.HitSkill, collider);
    }

/*
comment {
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
}
*/

    [Header("PowerSettings (%)")]
    [SerializeField] private int _increasePercent;
    [SerializeField] private int _maxDeviation;

    [Header("TargetSettings")]
    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;

    private TargetChecker    _targetChecker;
    private DamageCalculator _damageCalculator;
    private Notifier         _notifier;
  }
}

