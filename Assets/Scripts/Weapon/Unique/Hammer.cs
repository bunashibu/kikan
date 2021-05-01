using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Hammer : Weapon {
    new void Awake() {
      base.Awake();

      _isAcceptingCtrlBreak = new ReactiveProperty<bool>(false);
      _isAcceptingSpaceBreak = new ReactiveProperty<bool>(false);

      this.UpdateAsObservable()
        .Where(_ => _player.PhotonView.isMine                          )
        .Where(_ => _player.Hp.Cur.Value > 0                           )
        .Where(_ => !_player.State.Rigor                               )
        .Where(_ => _instantiator.IsSkillUsableAnimationState(_player) )
        .Where(_ => !_player.Debuff.State[DebuffType.Stun]             )
        .Where(_ => CanInstantiate.Value                               )
        .Subscribe(_ => {
          GetInput(_ctrlInfo.Index);
          GetInput(_spaceInfo.Index);
        });

      this.UpdateAsObservable()
        .Where(_ => CanGetCtrlBreakInput() )
        .Subscribe(_ => GetUniqueInput(_ctrlInfo.Index) );
      this.UpdateAsObservable()
        .Where(_ => IsTimeoutCtrlBreak() )
        .Subscribe(_ => UseUnique(_ctrlInfo.Index) );
      this.UpdateAsObservable()
        .Where(_ => _isAcceptingCtrlBreak.Value  )
        .Where(_ => _ctrl.Shield.IsBroken )
        .Subscribe(_ => UseUnique(_ctrlInfo.Index) );

      this.UpdateAsObservable()
        .Where(_ => CanGetSpaceBreakInput() )
        .Subscribe(_ => GetUniqueInput(_spaceInfo.Index) );
      this.UpdateAsObservable()
        .Where(_ => IsTimeoutSpaceBreak() )
        .Subscribe(_ => UseUnique(_spaceInfo.Index) );
    }

    private bool CanGetCtrlBreakInput() {
      return ( _isAcceptingCtrlBreak.Value ) &&
             ( !_player.State.Rigor        ) &&
             ( Time.time - _ctrlInstantiatedTimestamp <= _ctrlInfo.UsableSec ) &&
             ( IsPlayerMineAndAlive()      ) &&
             ( CanInstantiate.Value        );
    }

    private bool CanGetSpaceBreakInput() {
      return ( _isAcceptingSpaceBreak.Value ) &&
             ( !_player.State.Rigor         ) &&
             ( Time.time - _spaceInstantiatedTimestamp <= _spaceInfo.UsableSec ) &&
             ( IsPlayerMineAndAlive()       ) &&
             ( CanInstantiate.Value         );
    }

    private bool IsTimeoutCtrlBreak() {
      return ( _isAcceptingCtrlBreak.Value ) &&
             ( Time.time - _ctrlInstantiatedTimestamp > _ctrlInfo.UsableSec ) &&
             ( _player.PhotonView.isMine   );
    }

    private bool IsTimeoutSpaceBreak() {
      return ( _isAcceptingSpaceBreak.Value ) &&
             ( Time.time - _spaceInstantiatedTimestamp > _spaceInfo.UsableSec ) &&
             ( _player.PhotonView.isMine    );
    }

    private bool IsPlayerMineAndAlive() {
      return ( _player.PhotonView.isMine ) &&
             ( _player.Hp.Cur.Value > 0  ) &&
             ( _instantiator.IsSkillUsableAnimationState(_player) );
    }

    // Before unique skill is instantiated
    private void GetInput(int i) {
      if (_player.Level.Cur.Value < RequireLv[i])
        return;

      for (int k=0; k < KeysList[i].keys.Count; ++k) {
        if (base.IsUsable(i) && Input.GetKey(KeysList[i].keys[k])) {
          if (i == _ctrlInfo.Index)
            _ctrl = (WarriorCtrl)InstantiateSkill(i);
          if (i == _spaceInfo.Index)
            _space = (WarriorSpace)InstantiateSkill(i);
        }
      }
    }

    private Skill InstantiateSkill(int i) {
      Assert.IsTrue(_player.PhotonView.isMine);

      if (i == _ctrlInfo.Index) {
        _isAcceptingCtrlBreak.Value = true;
        _ctrlInstantiatedTimestamp = Time.time;
      }
      if (i == _spaceInfo.Index) {
        _isAcceptingSpaceBreak.Value = true;
        _spaceInstantiatedTimestamp = Time.time;
      }

      return _instantiator.InstantiateSkill(i, this, _player);
    }

    // During unique skill is instantiated
    private void GetUniqueInput(int i) {
      Assert.IsTrue(_player.Level.Cur.Value >= RequireLv[i]);

      for (int k=0; k < KeysList[i].keys.Count; ++k) {
        if (Input.GetKeyDown(KeysList[i].keys[k]))
          UseUnique(i);
      }
    }

    private void UseUnique(int i) {
      Assert.IsTrue(_player.PhotonView.isMine);

      if (i == _ctrlInfo.Index) {
        Assert.IsNotNull(_ctrl);

        _isAcceptingCtrlBreak.Value = false;
        _ctrl.SyncRedBreak();
      }

      if (i == _spaceInfo.Index) {
        Assert.IsNotNull(_space);

        _space.SyncSpaceBreak();
        _isAcceptingSpaceBreak.Value = false;
      }
    }

    private void ResetUniqueSkill() {
      _isAcceptingCtrlBreak.Value = false;
      _isAcceptingSpaceBreak.Value = false;
    }

    public override bool IsUsable(int i) {
      if (i == _ctrlInfo.Index || i == _spaceInfo.Index)
        return false;

      return _ctManager.IsUsable(i);
    }

    public override void ResetAllCT() {
      base.ResetAllCT();
      ResetUniqueSkill();
    }

    public IReadOnlyReactiveProperty<bool> IsAcceptingCtrlBreak  => _isAcceptingCtrlBreak;
    public IReadOnlyReactiveProperty<bool> IsAcceptingSpaceBreak => _isAcceptingSpaceBreak;

    [SerializeField] private HammerUniqueInfo _ctrlInfo;
    [SerializeField] private HammerUniqueInfo _spaceInfo;

    private WarriorCtrl _ctrl;
    private bool _shouldUseCtrlBreak;
    private float _ctrlInstantiatedTimestamp;
    private ReactiveProperty<bool> _isAcceptingCtrlBreak;

    private WarriorSpace _space;
    private bool _shouldUseSpaceBreak;
    private float _spaceInstantiatedTimestamp;
    private ReactiveProperty<bool> _isAcceptingSpaceBreak;
  }

  [System.Serializable]
  public class HammerUniqueInfo {
    public int   Index     => _index;
    public float UsableSec => _usableSec;

    [SerializeField] private int _index;
    [SerializeField] private float _usableSec;
  }
}
