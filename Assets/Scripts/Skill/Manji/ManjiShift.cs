using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiShift : Skill {
    void Awake() {
      _rewardGetter      = new RewardGetter();
      _targetRistrictor  = new TargetRistrictor(_targetNum, _dupHitNum);
      _killDeathRecorder = new KillDeathRecorder();
      _powerCalculator   = new PowerCalculator();
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

    private void ProceedAttackToPlayer(GameObject targetObj) {
      var target = targetObj.GetComponent<BattlePlayer>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackPlayer(target, skillUser)) {
        DamageToPlayer(target, skillUser);
        target.NumberPopupEnvironment.Popup(DamageCalculator.Damage, DamageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Player);

        if (target.Hp.Cur <= 0)
          ProceedPlayerDeath(target, skillUser);
      }
    }

    private void ProceedAttackToEnemy(GameObject targetObj) {
      var target = targetObj.GetComponent<Enemy>();
      var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

      if (IsCorrectAttackEnemy(target)) {
        DamageToEnemy(target, skillUser);
        target.NumberPopupEnvironment.Popup(DamageCalculator.Damage, DamageCalculator.IsCritical, skillUser.DamageSkin.Id, PopupType.Enemy);

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

      target.Hp.Subtract(DamageCalculator.Damage);
      target.Hp.UpdateView();
    }

    private void DamageToEnemy(Enemy target, BattlePlayer skillUser) {
      DamageCalculator.Calculate(_skillUserObj, _attackInfo);

      target.Hp.Subtract(DamageCalculator.Damage);
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

    [SerializeField] private AttackInfo _attackInfo;

    [Header("TargetSettings")]
    [SerializeField] private int _targetNum;
    [SerializeField] private int _dupHitNum;

    private RewardGetter      _rewardGetter;
    private TargetRistrictor  _targetRistrictor;
    private KillDeathRecorder _killDeathRecorder;
    private PowerCalculator   _powerCalculator;
  }
}

