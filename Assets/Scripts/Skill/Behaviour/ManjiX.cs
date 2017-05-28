using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;

      bool isSkillUser = (target == _user);
      bool isOtherPlayer = (target.tag == "Player");
      bool isEnemy = (target.tag == "Enemy");

      if (isSkillUser)
        return;

      if (isOtherPlayer) {
        bool isNotLimited = _limiter.Check(target, _team);

        if (isNotLimited)
          PlayerDamageProcess(target);
      }

      if (isEnemy) {
        Debug.Log("Enemy damage impl");
      }
    }
  }

  [PunRPC]
  private void InstantiateDamagePanel(int damage, PhotonPlayer targetPlayer, int viewID) {
    bool isTarget = (PhotonNetwork.player == targetPlayer);

    if (isTarget) {
      var target = PhotonView.Find(viewID).gameObject;
      var skin = target.GetComponent<PlayerDamageSkin>().Skin;
      var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

      damagePanel.CreateTake(damage, skin);
    } else {
      var skin = _user.GetComponent<PlayerDamageSkin>().Skin;
      var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

      damagePanel.CreateHit(damage, skin);
    }
  }

  private int CalcDamage() {
    int atk = _user.GetComponent<PlayerStatus>().Atk;
    int deviation = (int)((Random.value - 0.5) * 2 * MaxDeviation);

    return atk + deviation;
  }

  private void PlayerDamageProcess(GameObject target) {
    var targetView = target.GetComponent<PhotonView>();
    var targetPlayer = targetView.owner;
    var targetViewID = targetView.viewID;

    int damage = CalcDamage();

    photonView.RPC("InstantiateDamagePanel", PhotonTargets.All, damage, targetPlayer, targetViewID);

    DamageToPlayer(target, damage);
  }

  private void DamageToPlayer(GameObject target, int damage) {
    var targetHp = target.GetComponent<PlayerHp>();
    targetHp.Minus(damage);
    targetHp.UpdateView();

    if (targetHp.Dead)
      PlayerDeathProcess(target);
  }

  private void PlayerDeathProcess(GameObject target) {
    _rewardGetter.SetRewardReceiver(_user, _team);
    _rewardGetter.GetRewardFrom(target);

    var userKillDeath = _user.GetComponent<PlayerKillDeath>();
    var targetKillDeath = target.GetComponent<PlayerKillDeath>();

    userKillDeath.RecordKill();
    targetKillDeath.RecordDeath();

    userKillDeath.UpdateKillView();
    targetKillDeath.UpdateDeathView();
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
  [SerializeField] private TargetLimiter _limiter;
  [SerializeField] private RewardGetter _rewardGetter;
  [SerializeField] private DamagePanel _damagePanel;
  private static readonly int MaxDeviation = 10;
}

