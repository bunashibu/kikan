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

        _damageBehaviour.DamageToTarget(_power, _maxDeviation, (IBattle)target);
      }

      if (targetObj.tag == "Enemy") {
        /*
        var target = targetObj.GetComponent<Enemy>();
        _damageBehaviour.DamageToTarget(_power, _maxDeviation, targetObj);
        */
      }
    }

    [SerializeField] private TargetRistrictor _targetRistrictor;
    [SerializeField] private int _power;
    [SerializeField] private int _maxDeviation;
    private BattlePlayer _skillUser;
    private DamageBehaviour _damageBehaviour;
    private RewardGetter _rewardGetter;
  }
}

