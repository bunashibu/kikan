using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class HelenaSpace : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Subscribe(_ => {
          _player = _skillUserObj.GetComponent<Player>();
        });

      this.UpdateAsObservable()
        .Where(_ => _player != null)
        .Where(_ => photonView.isMine )
        .Take(1)
        .Subscribe(_ => {
          CameraManager.Instance.SetTrackTarget(gameObject);

          Observable.Timer(TimeSpan.FromSeconds(_existTime))
            .Where(none => this != null)
            .Where(none => photonView.isMine )
            .Subscribe(none => {
              PhotonNetwork.Destroy(gameObject);
            });
        });

      this.UpdateAsObservable()
        .Where(_ => _player != null)
        .Where(_ => _player.Level.Cur.Value >= 11)
        .Take(1)
        .Subscribe(_ => _attackInfo = new AttackInfo(130, _attackInfo.MaxDeviation, _attackInfo.CriticalPercent));
    }

    void OnDestroy() {
      if (_player == null)
        return;

      if (photonView.isMine)
        CameraManager.Instance.SetTrackTarget(_player.gameObject);
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

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Helena);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _existTime = 5.0f;
    private Player _player;
  }
}
