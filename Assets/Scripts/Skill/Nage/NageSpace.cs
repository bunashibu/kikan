using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class NageSpace : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      MonoUtility.Instance.StoppableDelaySec(_existTime, "NageSpaceFalse" + GetInstanceID().ToString(), () => {
        gameObject.SetActive(false);

        // NOTE: See SMB-DestroySkillSelf
        MonoUtility.Instance.StoppableDelaySec(5.0f, "NageSpaceDestroy" + GetInstanceID().ToString(), () => {
          Destroy(gameObject);
        });
      });

      _collider = GetComponent<BoxCollider2D>();
      _collider.enabled = false;
      _animator = GetComponent<Animator>();
      _animator.enabled = false;
      _audioSource = GetComponent<AudioSource>();
      _audioSource.enabled = false;

      GetComponent<SpriteRenderer>().sprite = _prepareSprite;

      _timestamp = Time.time;
    }

    void Update() {
      if (Time.time - _timestamp >= _collisionOccurenceTime) {
        _collider.enabled = true;
        _animator.enabled = true;
        _audioSource.enabled = true;
      }
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

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Nage);
      }
    }

    void OnDestroy() {
      if (photonView.isMine && SkillReference.Instance != null)
        SkillReference.Instance.Remove(this);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private Sprite _prepareSprite;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;

    private BoxCollider2D _collider;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _timestamp;
    private float _collisionOccurenceTime = 1.0f;
    private float _existTime = 10.0f;
  }
}

