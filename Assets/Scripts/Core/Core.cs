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
      _effect = new Dictionary<CoreType, GameObject>();
      _state = new CoreState();
    }

    public Core(ICorePlayer player) : this() {
      player.gameObject.UpdateAsObservable()
        .Where(_ => player.PhotonView.isMine)
        .Subscribe(_ => Update(player) )
        .AddTo(player.gameObject);
    }

    public void Register(CoreType type, CoreInfo info, GameObject effect) {
      _info[type] = info;
      _state.Register(type);
      _effect[type] = effect;
    }

    private void Update(ICorePlayer player) {
      foreach (CoreType type in Enum.GetValues(typeof(CoreType))) {
        if (!Input.GetKeyDown(_info[type].Key))
          continue;

        if (_state.IsNeedReconfirm(type))
          return;

        bool isMaxLevel = (_state.CurLevel(type) == _state.MaxLevel(type));
        if (isMaxLevel)
          return;

        bool isNotEnoughGold = (player.Gold.Cur.Value < _info[type].Gold(_state.CurLevel(type)));
        if (isNotEnoughGold)
          return;

        player.Synchronizer.SyncCoreLevelUp(type);
      }
    }

    public void LevelUp(CoreType type) {
      _state.LevelUp(type);
    }

    public void Instantiate(CoreType type, Transform trans) {
      GameObject.Instantiate(_effect[type], trans);
    }

    public int GetValue(CoreType type) {
      if (_info.ContainsKey(type))
        return _info[type].Value(_state.CurLevel(type));

      return -1;
    }

    public int RequiredGold(CoreType type) {
      if (_info.ContainsKey(type))
        return _info[type].Gold(_state.CurLevel(type));

      return -1;
    }

    private Dictionary<CoreType, CoreInfo> _info;
    private Dictionary<CoreType, GameObject> _effect;
    private CoreState _state;
  }
}

