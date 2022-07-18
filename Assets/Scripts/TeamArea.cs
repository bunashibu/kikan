using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class TeamArea : Photon.MonoBehaviour, IAttacker, IPhotonBehaviour {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
    }

    // TEMP: deal with simultaneous heal/attack bug temporarily
    void OnTriggerEnter2D(Collider2D collider) {
      var player = GetMinePlayer(collider);
      if (player == null)
        return;

      _eventSynchronizer.SyncPlayerHp(player.PhotonView.viewID, player.Hp.Cur.Value);
    }

    void OnTriggerStay2D(Collider2D collider) {
      var target = GetMinePlayer(collider);
      if (target == null)
        return;

      if (target.PlayerInfo.Team == _team) {
        if (target.Hp.Cur.Value == target.Hp.Max.Value)
          return;

        if (Time.time - _healTimestamp > _healInterval) {
          _synchronizer.SyncHeal(target.PhotonView.viewID, (int)(target.Hp.Max.Value / 3.0f));
          _healTimestamp = Time.time;
        }
      }
      else {
        if (target.State.Invincible)
          return;

        target.State.Invincible = true;
        MonoUtility.Instance.DelaySec(_damageInterval, () => { target.State.Invincible = false; } );

        _synchronizer.SyncAttack(photonView.viewID, target.PhotonView.viewID, (int)(target.Hp.Max.Value / 3.0f), false, HitEffectType.None);
      }
    }

    private Player GetMinePlayer(Collider2D collider) {
      var targetObj = collider.gameObject;
      if (targetObj.tag != "Player")
        return null;

      var target = targetObj.GetComponent<Player>();

      if (!target.PhotonView.isMine)
        return null;

      return target;
    }

    public PhotonView PhotonView => photonView;
    public int Power => 0;
    public int Critical => 0;
    public int CurLevel => 1;

    [SerializeField] private int _team;
    [SerializeField] private EventSynchronizer _eventSynchronizer;
    private SkillSynchronizer _synchronizer;
    private float _healInterval = 1.0f;
    private float _damageInterval = 1.0f;
    private float _healTimestamp;
  }
}
