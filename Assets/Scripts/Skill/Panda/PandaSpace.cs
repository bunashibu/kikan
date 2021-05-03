using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class PandaSpace : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      MonoUtility.Instance.StoppableDelaySec(_existTime, "PandaSpaceFalse" + GetInstanceID().ToString(), () => {
        if (gameObject == null)
          return;

        gameObject.SetActive(false);

        // NOTE: See SMB-DestroySkillSelf
        MonoUtility.Instance.StoppableDelaySec(5.0f, "PandaSpaceDestroy" + GetInstanceID().ToString(), () => {
          if (gameObject == null)
            return;

          Destroy(gameObject);
        });
      });

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Subscribe(_ => {
          _player = _skillUserObj.GetComponent<Player>();
        });

      this.UpdateAsObservable()
        .Where(_ => _player != null)
        .Subscribe(_ => {
          transform.position = _player.transform.position + _player.Weapon.AppearOffset[4];
        });

      this.UpdateAsObservable()
        .Where(_ => _player != null)
        .Where(_ => _player.Level.Cur.Value >= 11)
        .Take(1)
        .Subscribe(_ => _attackInfo = new AttackInfo(150, _attackInfo.MaxDeviation, _attackInfo.CriticalPercent));
    }

    void OnTriggerStay2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Panda);
      }
    }

    void OnDestroy() {
      if (photonView.isMine && SkillReference.Instance != null)
        SkillReference.Instance.Remove(viewID);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _existTime = 10.0f;
    private Player _player;
  }
}
