using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class MagicianX : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();

      _collider = GetComponent<Collider2D>();
      _filter = new ContactFilter2D();
      _filter.useLayerMask = true;
      _filter.useTriggers = true;
      _filter.layerMask = _layerMask;

      Destroy(gameObject, 1.0f);
    }

    void Start() {
      if (PhotonNetwork.isMasterClient) {
        var target = GetMostNearestTarget();

        if (target == null)
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Magician);
      }
    }

    private IPhoton GetMostNearestTarget() {
      Collider2D[] colliders = new Collider2D[32];
      int overlapCount = _collider.OverlapCollider(_filter, colliders);

      if (overlapCount == 0)
        return null;

      var candidates = colliders.Where(candidate => candidate != null)
        .Where(candidate => TeamChecker.IsNotSameTeam(candidate.gameObject, _skillUserObj));

      if (candidates.Count() == 0)
        return null;

      var targetCollider = candidates.Select(candidate =>
          new {
            Candidate = candidate,
            Distance = Vector3.Distance(candidate.transform.position, _skillUserObj.transform.position)
          })
        .Aggregate((min, tmp) => (min.Distance < tmp.Distance) ? min : tmp).Candidate;

      var target = targetCollider.gameObject.GetComponent<IPhoton>();

      return target;
    }

    void OnDestroy() {
      SkillReference.Instance.Remove(this);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private LayerMask _layerMask;

    private SkillSynchronizer _synchronizer;

    private Collider2D _collider;
    private ContactFilter2D _filter;
  }
}

