using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(SkillSynchronizer))]
  public class WarriorCtrl : Skill {
    void Awake() {
      _synchronizer = GetComponent<SkillSynchronizer>();
      _hitRistrictor = new HitRistrictor(_hitInfo);

      _animator = GetComponent<Animator>();
      _animator.SetBool("RedBreak", false);
      _photonView = GetComponent<PhotonView>();

      Shield = new Shield(_durability);
    }

    void Start() {
      transform.parent = _skillUserObj.transform;

      _skillUser = _skillUserObj.GetComponent<IOnAttacked>();
      _skillUser.DamageReactor.SetSlot(Shield);
    }

    void OnTriggerStay2D(Collider2D collider) {
      if (PhotonNetwork.isMasterClient && _animator.GetCurrentAnimatorStateInfo(0).IsName("CtrlBreak")) {
        var target = collider.gameObject.GetComponent<IPhoton>();

        if (target == null)
          return;
        if (TeamChecker.IsSameTeam(collider.gameObject, _skillUserObj))
          return;
        if (_hitRistrictor.ShouldRistrict(collider.gameObject))
          return;

        DamageCalculator.Calculate(_skillUserObj, _attackInfo);

        _synchronizer.SyncAttack(_skillUserViewID, target.PhotonView.viewID, DamageCalculator.Damage, DamageCalculator.IsCritical, HitEffectType.Warrior);
        _synchronizer.SyncDebuff(target.PhotonView.viewID, DebuffType.Stun, _stunSec);
      }
    }

    // NOTE: In case final battle forcely destroy WarriorCtrl.(e.g. final battle migration)
    void OnDestroy() {
      if (_skillUser != null)
        _skillUser.DamageReactor.SetSlot(new Passing());
    }

    [PunRPC]
    private void SyncRedBreakRPC() {
      _animator.SetBool("RedBreak", true);
      _skillUser.DamageReactor.SetSlot(new Passing());
    }

    public void SyncRedBreak() {
      _photonView.RPC("SyncRedBreakRPC", PhotonTargets.AllViaServer);
    }

    public Shield Shield { get; private set; }

    [SerializeField] private AttackInfo _attackInfo;
    [SerializeField] private HitInfo _hitInfo;
    [SerializeField] private float _stunSec;
    [SerializeField] private int _durability;

    private SkillSynchronizer _synchronizer;
    private HitRistrictor _hitRistrictor;
    private PhotonView _photonView;
    private Animator _animator;
    private IOnAttacked _skillUser;
  }
}

