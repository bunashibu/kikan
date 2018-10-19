using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiX : Skill {
    void Awake() {
      _targetChecker = new TargetChecker(_skillUserObj, _targetNum);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient && _targetChecker.IsAttackTarget(collider)) {
        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        var target = collider.gameObject.GetComponent<INotifier>();
        target.Notifier.Notify(Notification.TakeDamage, DamageCalculator.IsCritical, DamageCalculator.Damage, _skillUserObj);
      }
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

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private int _targetNum;

    private TargetChecker    _targetChecker;
  }
}

