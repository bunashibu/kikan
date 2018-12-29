using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class MagicianZ : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _targetChecker = new TargetChecker(_targetLimit, _dupHealLimit);
    }

    void OnTriggerStay2D(Collider2D collider) {
      if (_targetChecker.IsSameTeam(collider.gameObject, _skillUserObj)) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (_targetChecker.IsMaxSustainHit(collider.gameObject))
          return;
        if (_targetChecker.IsNeedInterval(collider.gameObject))
          return;

        _synchronizer.SyncHeal(target.PhotonView.viewID, _quantity);
      }
    }

    [SerializeField] private int _targetLimit;
    [SerializeField] private int _dupHealLimit;
    [SerializeField] private int _quantity;

    private SkillSynchronizer _synchronizer;
    private TargetChecker _targetChecker;
  }
}

