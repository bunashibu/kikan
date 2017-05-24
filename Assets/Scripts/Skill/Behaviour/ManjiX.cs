using UnityEngine;
using System.Collections;

public class ManjiX : Skill {
  [PunRPC]
  private void InstantiateDamagePanel(int damage) {
    var skin = _user.GetComponent<PlayerDamageSkin>().Skin;
    var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

    damagePanel.Create(damage, skin);
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (PhotonNetwork.isMasterClient) {
      var target = collider.gameObject;

      if (target == _user)
        return;

      if (target.tag == "Player") {
        if (_limiter.Check(target, _team)) {
          var targetHp = target.GetComponent<PlayerHp>();

          int atk = _user.GetComponent<PlayerStatus>().Atk;
          int deviation = (int)((Random.value - 0.5) * 2 * MaxDeviation);
          int damage = atk + deviation;

          photonView.RPC("InstantiateDamagePanel", PhotonTargets.All, damage);

          targetHp.Minus(damage);
          targetHp.Show();

          if (targetHp.Dead) {
            _expGetter.SetExpReceiver(_user, _team);
            _expGetter.GetExpFrom(target);

            _user.GetComponent<PlayerKillDeathRecorder>().RecordKill();
            target.GetComponent<PlayerKillDeathRecorder>().RecordDeath();
          }
        }
      }
    }
  }

  [SerializeField] private BoxCollider2D _collider;
  [SerializeField] private int _power;
  [SerializeField] private TargetLimiter _limiter;
  [SerializeField] private ExpGetter _expGetter;
  [SerializeField] private DamagePanel _damagePanel;
  private static readonly int MaxDeviation = 10;
}

