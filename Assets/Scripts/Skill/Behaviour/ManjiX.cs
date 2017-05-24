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
  private void InstantiateDamagePanel(int damage) {
    var skin = _user.GetComponent<PlayerDamageSkin>().Skin;
    var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

    damagePanel.Create(damage, skin);
  }

  private int CalcDamage() {
    int atk = _user.GetComponent<PlayerStatus>().Atk;
    int deviation = (int)((Random.value - 0.5) * 2 * MaxDeviation);

    return atk + deviation;
  }

  private void PlayerDamageProcess(GameObject target) {
    int damage = CalcDamage();

    photonView.RPC("InstantiateDamagePanel", PhotonTargets.All, damage);
    DamageToPlayer(target, damage);
  }

  private void DamageToPlayer(GameObject target, int damage) {
    var targetHp = target.GetComponent<PlayerHp>();
    targetHp.Minus(damage);
    targetHp.Show();

    if (targetHp.Dead)
      PlayerDeathProcess(target);
  }

  private void PlayerDeathProcess(GameObject target) {
    _expGetter.SetExpReceiver(_user, _team);
    _expGetter.GetExpFrom(target);

    _user.GetComponent<PlayerKillDeathRecorder>().RecordKill();
    target.GetComponent<PlayerKillDeathRecorder>().RecordDeath();
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
  [SerializeField] private TargetLimiter _limiter;
  [SerializeField] private ExpGetter _expGetter;
  [SerializeField] private DamagePanel _damagePanel;
  private static readonly int MaxDeviation = 10;
}

