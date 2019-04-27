using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Fist : Weapon {
    void Awake() {
      base.Awake();

      _isAcceptingUnique = new ReactiveProperty<bool>(false);

      _beforeName = _skillNames[_uniqueInfo.Index];
      _beforeCT = _skillCT[_uniqueInfo.Index];

      Stream.OnInstantiated
        .Subscribe(i => {
          if (i == _uniqueInfo.Index && !_isAcceptingUnique.Value)
            ReadyUniqueSkill();
        })
        .AddTo(this);

      this.UpdateAsObservable()
        .Where(_ => CanGetUniqueInput() )
        .Subscribe(_ => GetUniqueInput() );

      this.UpdateAsObservable()
        .Where(_ => IsTimeoutToUseUniqueSkill() )
        .Subscribe(_ => ResetUniqueSkill() );

      this.UpdateAsObservable()
        .Where(_ => ShouldInstantiateUniqueSkill() )
        .Subscribe(_ => {
          _instantiator.InstantiateSkill(_uniqueInfo.Index, this, _player);
          ResetUniqueSkill();
        });
    }

    private bool CanGetUniqueInput() {
      return ( _isAcceptingUnique.Value ) &&
             ( Time.time - _instantiatedTimestamp <= _uniqueInfo.UsableSec ) &&
             ( IsPlayerMineAndAlive()   ) &&
             ( CanInstantiate           );
    }

    private bool IsTimeoutToUseUniqueSkill() {
      return ( _isAcceptingUnique.Value  ) &&
             ( Time.time - _instantiatedTimestamp > _uniqueInfo.UsableSec ) &&
             ( _player.PhotonView.isMine );
    }

    private bool ShouldInstantiateUniqueSkill() {
      return ( _isAcceptingUnique.Value ) &&
             ( _shouldUseUnique         ) &&
             ( !_player.State.Rigor     ) &&
             ( IsPlayerMineAndAlive()   ) &&
             ( CanInstantiate           );
    }

    private bool IsPlayerMineAndAlive() {
      return ( _player.PhotonView.isMine ) &&
             ( _player.Hp.Cur.Value > 0  ) &&
             ( _instantiator.IsSkillUsableAnimationState(_player) );
    }

    private void ReadyUniqueSkill() {
      _skillNames[_uniqueInfo.Index] = _uniqueInfo.After;
      _skillCT[_uniqueInfo.Index] = _uniqueInfo.SkillCT;

      _isAcceptingUnique.Value = true;
      _instantiatedTimestamp = Time.time;
    }

    private void ResetUniqueSkill() {
      _skillNames[_uniqueInfo.Index] = _beforeName;
      _skillCT[_uniqueInfo.Index] = _beforeCT;
      _shouldUseUnique = false;
      _isAcceptingUnique.Value = false;
    }

    // NOTE: While ready to use unique skill, the input of ctrl is managed in this. not in SkillInstantiator.
    private void GetUniqueInput() {
      if (Time.time == _instantiatedTimestamp)
        return;

      int i = _uniqueInfo.Index;

      for (int k=0; k < KeysList[i].keys.Count; ++k) {
        if (Input.GetKeyDown(KeysList[i].keys[k])) {
          _shouldUseUnique = true;
          break;
        }
      }
    }

    public override void ResetAllCT() {
      base.ResetAllCT();
      ResetUniqueSkill();
    }

    public IReadOnlyReactiveProperty<bool> IsAcceptingUnique => _isAcceptingUnique;

    [SerializeField] private FistUniqueInfo _uniqueInfo;

    private bool _shouldUseUnique;
    private float _instantiatedTimestamp;
    private ReactiveProperty<bool> _isAcceptingUnique;

    private SkillName _beforeName;
    private float _beforeCT;
  }

  [System.Serializable]
  public class FistUniqueInfo {
    public int       Index     => _index;
    public float     SkillCT   => _skillCT;
    public float     UsableSec => _usableSec;
    public SkillName After     => _after;

    [SerializeField] private int _index;
    [SerializeField] private float _skillCT;
    [SerializeField] private float _usableSec;
    [SerializeField] private SkillName _after;
  }
}

