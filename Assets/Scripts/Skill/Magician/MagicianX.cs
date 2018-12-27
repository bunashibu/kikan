using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class MagicianX : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _targetChecker = new TargetChecker(_targetNum);

      _collider = GetComponent<Collider2D>();
      _filter = new ContactFilter2D();
      _filter.useLayerMask = true;
      _filter.useTriggers = true;
      _filter.layerMask = _layerMask;

      Destroy(gameObject, 1.0f);
    }

    void Start() {
      if (!PhotonNetwork.isMasterClient)
        return;

      Collider2D[] colliders = new Collider2D[32];
      int count = _collider.OverlapCollider(_filter, colliders);

      if (count == 0)
        return;

      var candidates = colliders.Where(candidate => candidate != null)
        .Where(candidate => !_targetChecker.IsSameTeam(candidate.gameObject, _skillUserObj));

      if (candidates.Count() == 0)
        return;

      // Get the most nearest character's collider
      var targetCollider = candidates.Select(candidate =>
          new {
            Candidate = candidate,
            Distance = Vector3.Distance(candidate.transform.position, _skillUserObj.transform.position)
          })
        .Aggregate((min, tmp) => (min.Distance < tmp.Distance) ? min : tmp).Candidate;

      if (_targetChecker.IsAttackTarget(targetCollider, _skillUserObj)) {
        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        var target = targetCollider.gameObject.GetComponent<IPhoton>();
        Assert.IsNotNull(target);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Magician);
      }
    }

    void OnDestroy() {
      SkillReference.Instance.Remove(this);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private int _targetNum;
    [SerializeField] private LayerMask _layerMask;

    private SkillSynchronizer _synchronizer;
    private TargetChecker _targetChecker;

    private Collider2D _collider;
    private ContactFilter2D _filter;
  }
}

