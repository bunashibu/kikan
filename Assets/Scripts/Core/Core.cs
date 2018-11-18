using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Core {
    private Core() {
      _info = new Dictionary<CoreType, CoreInfo>();
      _state = new CoreState();
    }

    public Core(ICorePlayer player) : this() {
      player.gameObject.UpdateAsObservable()
        .Where(_ => player.PhotonView.isMine)
        .Subscribe(_ => Update() );

      _player = player;
    }

    public void Register(CoreType type, CoreInfo info, GameObject effect) {
      _info[type] = info;
      _state.Register(type);

      _state.Level[type].Cur
        .SkipLatestValueOnSubscribe()
        .Subscribe(_ => GameObject.Instantiate(effect, _player.transform) );

      _state.Level[type].Cur
        .SkipLatestValueOnSubscribe()
        .Subscribe(level => _player.Gold.Subtract(_info[type].Gold(level - 1)) );
    }

    private void Update() {
      foreach (CoreType type in Enum.GetValues(typeof(CoreType))) {
        if (!Input.GetKeyDown(_info[type].Key))
          continue;

        if (_state.IsNeedReconfirm(type))
          return;

        bool isMaxLevel = (_state.CurLevel(type) == _state.MaxLevel(type));
        if (isMaxLevel)
          return;

        bool isNotEnoughGold = (_player.CurGold < _info[type].Gold(_state.CurLevel(type)));
        if (isNotEnoughGold)
          return;

        _state.LevelUp(type);
      }
    }

    public int GetValue(CoreType type) {
      if (_info.ContainsKey(type))
        return _info[type].Value(_state.CurLevel(type));

      return 0;
    }

    private Dictionary<CoreType, CoreInfo> _info;
    private CoreState _state;
    private ICorePlayer _player;
  }
}

