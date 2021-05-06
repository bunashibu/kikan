using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;
using System;

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
            .Where(_ => this != null)
            .Subscribe(_ => {
              CameraManager.Instance.SetTrackTarget(_player.gameObject);
            });
        });
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
