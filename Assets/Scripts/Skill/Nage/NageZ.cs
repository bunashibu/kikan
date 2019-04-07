using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class NageZ : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      MonoUtility.Instance.StoppableDelaySec(_existTime, "NageZFalse" + GetInstanceID().ToString(), () => {
        gameObject.SetActive(false);

        // NOTE: See SMB-DestroySkillSelf
        MonoUtility.Instance.StoppableDelaySec(5.0f, "NageZDestroy" + GetInstanceID().ToString(), () => {
          Destroy(gameObject);
        });
      });
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
        _synchronizer.SyncDebuff(target.PhotonView.viewID, DebuffType.Heavy, _duration);
      }
    }

    void OnDestroy() {
      if (photonView.isMine && SkillReference.Instance != null)
        SkillReference.Instance.Remove(this);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [Header("Debuff Duration(sec)")]
    [SerializeField] private float _duration;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private float _existTime = 4.0f;
  }
}

