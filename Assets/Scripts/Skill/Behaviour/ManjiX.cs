using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;

      if (target == _user)
        return;

      if (target.tag == "Player" && _limiter.Check(target, _team))
        PlayerDamageProcess(target);

      if (target.tag == "Enemy")
        Debug.Log("Enemy damage impl");
    }
  }

  private void PlayerDamageProcess(GameObject target) {
    int damage = CalcDamage();

    var targetHp = target.GetComponent<PlayerHp>();
    targetHp.Minus(damage);
    targetHp.UpdateView();

    var targetView = target.GetComponent<PhotonView>();
    photonView.RPC("InstantiateDamagePanel", PhotonTargets.All, damage, _isCritical, targetView.owner, targetView.viewID);

    if (targetHp.Dead)
      PlayerDeathProcess(target);
  }

  private void PlayerDeathProcess(GameObject target) {
    _rewardGetter.SetRewardReceiver(_user, _team);
    _rewardGetter.GetRewardFrom(target);

    var killPlayer = _user.GetComponent<PlayerKillDeath>();
    killPlayer.RecordKill();
    killPlayer.UpdateKillView();

    var deathPlayer = target.GetComponent<PlayerKillDeath>();
    deathPlayer.RecordDeath();
    deathPlayer.UpdateDeathView();
  }

  [PunRPC]
  private void InstantiateDamagePanel(int damage, bool isCritical, PhotonPlayer targetPlayer, int viewID) {
    bool isTarget = (PhotonNetwork.player == targetPlayer);

    if (isTarget) {
      var target = PhotonView.Find(viewID).gameObject;
      var skin = target.GetComponent<PlayerDamageSkin>().Skin;
      var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

      damagePanel.CreateTake(damage, skin);
    }
    else {
      var skin = _user.GetComponent<PlayerDamageSkin>().Skin;
      var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

      if (isCritical)
        damagePanel.CreateCritical(damage, skin);
      else
        damagePanel.CreateHit(damage, skin);
    }
  }

  private int CalcDamage() {
    int atk = _user.GetComponent<PlayerStatus>().Atk;
    double ratio = (double)((_user.GetComponent<PlayerCore>().Attack + 100) / 100.0);
    int deviation = (int)((Random.value - 0.5) * 2 * MaxDeviation);

    int damage = (int)(atk * ratio) + deviation;

    _isCritical = CriticalCheck();
    if (_isCritical)
      damage *= 2;

    return damage;
  }

  private bool CriticalCheck() {
    int probability = _user.GetComponent<PlayerCore>().Critical;
    int threshold = (int)(Random.value * 99);

    if (probability > threshold)
      return true;

    return false;
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
  [SerializeField] private TargetLimiter _limiter;
  [SerializeField] private RewardGetter _rewardGetter;
  [SerializeField] private DamagePanel _damagePanel;
  private static readonly int MaxDeviation = 10;
  private bool _isCritical = false;
}

