using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiX : Skill {
    void Awake() {
      _powerCalculator = new PowerCalculator();
      _damageCalculator = new DamageCalculator();
      _rewardGetter = new RewardGetter();
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (!PhotonNetwork.isMasterClient)
        return;

      var targetObj = collider.gameObject;
      if (targetObj == _skillUserObj)
        return;

      if (targetObj.tag == "Player") {
        var target = targetObj.GetComponent<BattlePlayer>();
        var skillUser = _skillUserObj.GetComponent<BattlePlayer>();

        if (target.PlayerInfo.Team == skillUser.PlayerInfo.Team)
          return;
        if (_targetRistrictor.ShouldRistrict(target))
          return;

        DamageToPlayer(target, skillUser);

        if (target.Hp.Cur <= 0) {
          _rewardGetter.SetRewardReceiver(skillUser);
          _rewardGetter.GetRewardFrom(target);

          _killDeathRecorder.RecordKillDeath(target, skillUser);
        }
      }

      if (targetObj.tag == "Enemy") {
      }
    }

    private void DamageToPlayer(BattlePlayer target, BattlePlayer skillUser) {
      int playerPower = _powerCalculator.CalculatePlayerPower(skillUser);
      int power = playerPower * (_skillPower / 100);

      int damage = _damageCalculator.CalculateDamage(power, _maxDeviation, skillUser.Core.Critical);

      target.Hp.Subtract(damage);
      target.Hp.UpdateView();
    }

    [SerializeField] private TargetRistrictor _targetRistrictor;
    [SerializeField] private int _skillPower;
    [SerializeField] private int _maxDeviation;
    private PowerCalculator _powerCalculator;
    private DamageCalculator _damageCalculator;
    private RewardGetter _rewardGetter;
    private KillDeathRecorder _killDeathRecorder;
  }
}

