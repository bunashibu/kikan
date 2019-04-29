using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class WarriorSpace : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);
      _healRistrictor = new HitRistrictor(_healInfo);

      _animator = GetComponent<Animator>();
      _animator.SetBool("SpaceBreak", false);
      _photonView = GetComponent<PhotonView>();
    }

    void Start() {
      transform.parent = _skillUserObj.transform;

      _player = _skillUserObj.GetComponent<Player>();

      // NOTE: For skill user heal himself.
      //       Because of WarriorSpace is a child of skill user,
      //       WarriorSpace doesn't heal skill user in OnTriggerStay2D.
      this.UpdateAsObservable()
        .Where(_ => _photonView.isMine )
        .Where(_ => !_healRistrictor.ShouldRistrict(_player.gameObject) )
        .Subscribe(_ => _synchronizer.SyncHeal(_player.PhotonView.viewID, GetHealQuantity()) );
    }

    void OnTriggerStay2D(Collider2D collider) {
      var target = collider.gameObject.GetComponent<IPhoton>();

      if (target == null)
        return;

      if (target.PhotonView.isMine && _animator.GetCurrentAnimatorStateInfo(0).IsName("Space")) {
        if (TeamChecker.IsNotSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_healRistrictor.ShouldRistrict(collider.gameObject))
          return;

        _synchronizer.SyncHeal(target.PhotonView.viewID, GetHealQuantity());
      }

      if (PhotonNetwork.isMasterClient && _animator.GetCurrentAnimatorStateInfo(0).IsName("SpaceBreak")) {
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Warrior);
      }
    }

    private int GetHealQuantity() {
      int healPercent = 250;
      if (_player.Level.Cur.Value >= 11)
        healPercent = 400;
      var healPower = new AttackInfo(healPercent, 0, 0);

      DamageCalculator.Calculate(_skillUserObj, healPower);

      return DamageCalculator.Damage;
    }

    [PunRPC]
    private void SyncSpaceBreakRPC() {
      _animator.SetBool("SpaceBreak", true);
    }

    public void SyncSpaceBreak() {
      _photonView.RPC("SyncSpaceBreakRPC", PhotonTargets.AllViaServer);
    }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private HitInfo _healInfo;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private HitRistrictor _healRistrictor;
    private PhotonView _photonView;
    private Animator _animator;
    private Player _player;
  }
}

