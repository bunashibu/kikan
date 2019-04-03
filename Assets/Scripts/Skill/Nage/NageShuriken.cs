using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class NageShuriken : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      Destroy(gameObject, 0.37f);
    }

    void Start() {
      if (photonView.isMine) {
        var renderer = _skillUserObj.GetComponent<SpriteRenderer>();
        Assert.IsNotNull(renderer);

        _direction = renderer.flipX ? Vector2.right : Vector2.left;
      }
    }

    void Update() {
      if (photonView.isMine)
        transform.Translate(_direction * _spd * Time.deltaTime);
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

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Nage);
      }
    }

    void OnDestroy() {
      if (photonView.isMine)
        SkillReference.Instance.Remove(this);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private Vector2 _direction;
    private float _spd = 12.0f;
  }
}

