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

      Collider2D[] candidates = new Collider2D[32];
      int count = _collider.OverlapCollider(_filter, candidates);

      if (count == 0)
        return;

      // Get the most nearest character's collider index
      int index = candidates.Where(candidate => candidate != null)
        .Select((candidate, i) =>
          new {
            Val = _collider.Distance(candidate).distance,
            Index = i
          })
        .Aggregate((min, tmp) => (min.Val < tmp.Val) ? min : tmp).Index;

      var targetCollider = candidates[index];

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

