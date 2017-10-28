using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ManjiX : Skill {
    void Awake() {
      _damageBehaviour = new DamageBehaviour();
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
        if (_targetRistrictor.ShouldRistrict((IBattle)target))
          return;

        int playerPower = _powerCalculator.CalculatePlayerPower(skillUser);
        int power = playerPower * (_skillPower / 100);

        int damage = _damageBehaviour.CalculateDamage(power, _maxDeviation, skillUser.Core.Critical);

        _damageBehaviour.DamageToTarget(damage, (IBattle)target);
      }

      if (targetObj.tag == "Enemy") {
      }
    }

    [SerializeField] private TargetRistrictor _targetRistrictor;
    [SerializeField] private int _skillPower;
    [SerializeField] private int _maxDeviation;
    private PowerCalculator _powerCalculator;
    private DamageBehaviour _damageBehaviour;
    private RewardGetter _rewardGetter;
  }
}

