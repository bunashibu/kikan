using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UniRx;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class HelenaX : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      Observable.Timer(TimeSpan.FromSeconds(_existTime))
        .Where(_ => this != null)
        .Subscribe(_ => {
          gameObject.SetActive(false);

          Observable.Timer(TimeSpan.FromSeconds(5.0f))
            .Where(none => this != null)
            .Subscribe(none => {
              Destroy(gameObject);
            });
        });
    }

    void Start() {
      if (photonView.isMine)
        _moveDirection = transform.eulerAngles.y == 180 ? Vector2.right : Vector2.left;
    }

    void Update() {
      if (photonView.isMine)
        transform.Translate(_moveDirection * _spd * Time.deltaTime, Space.World);
    }

    void OnTriggerEnter2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        if (_isSecond)
          _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, (int)(DamageCalculator.Damage * _secondRatio), DamageCalculator.IsCritical, HitEffectType.Helena);
        else {
          _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Helena);
          _isSecond = true;
        }
      }
    }

    void OnDestroy() {
      if (photonView.isMine && SkillReference.Instance != null)
        SkillReference.Instance.Remove(viewID);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private Vector2 _moveDirection;
    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _spd = 8.0f;
    private float _existTime = 0.37f;
    private bool _isSecond;
    private float _secondRatio = 0.5f;
  }
}
