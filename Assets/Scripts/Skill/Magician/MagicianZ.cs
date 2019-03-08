using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class MagicianZ : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);
    }

    void OnTriggerStay2D(Collider2D collider) {
      var target = collider.gameObject.GetComponent<IPhoton>();

      if (target == null)
        return;

      if (target.PhotonView.isMine) {
        if (TeamChecker.IsNotSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        _synchronizer.SyncHeal(target.PhotonView.viewID, _quantity);
      }
    }

    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private int _quantity;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
  }
}

