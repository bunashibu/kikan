using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class MagicianSpace : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      _collider = GetComponent<BoxCollider2D>();
      _collider.enabled = false;
      _timestamp = Time.time;

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Where(_ => {
          var skillUser = _skillUserObj.GetComponent<Player>();
          return Client.Opponents.Contains(skillUser);
        })
        .Subscribe(_ => _renderer.color = new Color(255.0f / 255.0f, 0, 190.0f / 255.0f, 1));

      this.UpdateAsObservable()
        .Where(_ => Time.time - _timestamp >= _collisionOccurenceTime)
        .Take(1)
        .Subscribe(_ => _collider.enabled = true );

      this.UpdateAsObservable()
        .Where(_ => _skillUserObj != null)
        .Take(1)
        .Subscribe(_ => _player = _skillUserObj.GetComponent<Player>() );

      this.UpdateAsObservable()
        .Where(_ => _player != null)
        .Where(_ => _player.Level.Cur.Value >= 11)
        .Take(1)
        .Subscribe(_ => _attackInfo = new AttackInfo(850, _attackInfo.MaxDeviation, _attackInfo.CriticalPercent));
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

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Magician);
      }
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private SpriteRenderer _renderer;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;

    private Player _player;
    private BoxCollider2D _collider;
    private float _timestamp;
    private float _collisionOccurenceTime = 1.0f;
  }
}
