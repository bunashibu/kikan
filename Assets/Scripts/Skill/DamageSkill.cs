using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageSkill : Skill {
  // INFO: Must Instantiate in InheritedClass like below.
  protected abstract void Awake(); /* {
    _damageBehaviour = new DamageBehaviour();
    _rewardGetter = new RewardGetter();
  }
  */

  protected void DamageToPlayer(int power, int maxDeviation, GameObject target) {
    _damageBehaviour.SetSkillUser(_skillUser);
    _damageBehaviour.DamageToPlayer(power, maxDeviation, target);

    var targetView = target.GetComponent<PhotonView>();
    photonView.RPC("SyncInstantiateDamagePanel", PhotonTargets.All, _damageBehaviour.Damage,
                   _damageBehaviour.Critical, targetView.owner, targetView.viewID);

    var targetHp = target.GetComponent<PlayerHp>();
    if (targetHp.Dead) {
      _rewardGetter.SetRewardReceiver(_skillUser, _team);
      _rewardGetter.GetRewardFrom(target);

      RecordKillDeath(target);
    }
  }

  [PunRPC]
  protected void SyncInstantiateDamagePanel(int damage, bool isCritical, PhotonPlayer targetPlayer, int viewID) {
    bool isTarget = (PhotonNetwork.player == targetPlayer);

    if (isTarget) {
      InstantiateDamageTakePanel(damage, viewID);
      return;
    }

    if (isCritical) {
      InstantiateDamageCriticalPanel(damage);
      return;
    }

    InstantiateDamageHitPanel(damage);
  }

  private void InstantiateDamageHitPanel(int damage) {
    var skin = _skillUser.GetComponent<PlayerDamageSkin>().Skin;
    var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

    damagePanel.CreateHit(damage, skin);
  }

  private void InstantiateDamageCriticalPanel(int damage) {
    var skin = _skillUser.GetComponent<PlayerDamageSkin>().Skin;
    var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

    damagePanel.CreateCritical(damage, skin);
  }

  private void InstantiateDamageTakePanel(int damage, int viewID) {
    var target = PhotonView.Find(viewID).gameObject;
    var skin = target.GetComponent<PlayerDamageSkin>().Skin;
    var damagePanel = Instantiate(_damagePanel, gameObject.transform, false);

    damagePanel.CreateTake(damage, skin);
  }

  private void RecordKillDeath(GameObject target) {
    var killPlayer = _skillUser.GetComponent<PlayerKillDeath>();
    killPlayer.RecordKill();
    killPlayer.UpdateKillView();

    var deathPlayer = target.GetComponent<PlayerKillDeath>();
    deathPlayer.RecordDeath();
    deathPlayer.UpdateDeathView();
  }

  [SerializeField] protected DamagePanel _damagePanel;
  protected DamageBehaviour _damageBehaviour;
  protected RewardGetter _rewardGetter;
}

