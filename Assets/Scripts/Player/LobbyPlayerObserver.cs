using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class LobbyPlayerObserver : MonoBehaviour, IBuffObserver {
    void Awake() {
      _shouldSync = new Dictionary<string, bool>();

      _shouldSync.Add("Buff"     , false);
      _shouldSync.Add("BuffStun" , false);
    }

    public bool ShouldSync(string key) {
      Assert.IsTrue(_shouldSync.ContainsKey(key));
      return _shouldSync[key];
    }

    public void SyncBuff() {
      _player.PhotonView.RPC("SyncBuffRPC", PhotonTargets.Others, _player.BuffState.Stun, _player.BuffState.Slow, _player.BuffState.Heavy);
    }

    public void SyncStun() {
      _player.PhotonView.RPC("SyncStunRPC", PhotonTargets.Others, _player.BuffState.Stun);
    }

    [PunRPC]
    private void SyncBuffRPC(bool stun, bool slow, bool heavy) {
      ForceSync("Buff", () => _player.BuffState.ForceSync(stun, slow, heavy));
    }

    [PunRPC]
    private void SyncStunRPC(bool stun) {
      ForceSync("BuffStun", () => _player.BuffState.ForceSyncStun(stun));
    }

    private void ForceSync(string key, Action action) {
      _shouldSync[key] = true;
      action();
      _shouldSync[key] = false;
    }

    [SerializeField] private LobbyPlayer _player;
    private Dictionary<string, bool> _shouldSync;
  }
}

