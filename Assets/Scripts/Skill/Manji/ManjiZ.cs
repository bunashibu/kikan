using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManjiZ : DamageSkill {
  protected override void Awake() {
    _damageBehaviour = new DamageBehaviour();
    _rewardGetter = new RewardGetter();

    //_stateChanger = new StateChanger();
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;
      var targetPlayer = target.GetComponent<BattlePlayer>();

      if (target == _skillUser)
        return;

      if (target.tag == "Player" && _limiter.Check(target, _team)) {
        DamageToPlayer(_power, _maxDeviation, targetPlayer);
        //_stateChanger.ChangeTo("Rigor", 2.0f, target);
      }
    }
  }

  [SerializeField] private TargetLimiter _limiter;
  [SerializeField] private int _power;
  [SerializeField] private int _maxDeviation;

  //private StateChanger _stateChanger;
}

