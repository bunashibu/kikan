using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EnemyObserver : MonoBehaviour, IBuffObserver {
    void Awake() {
      _shouldSync = new Dictionary<string, bool>();

      _shouldSync.Add("CurHp"        , false);
      _shouldSync.Add("UpdateHpView" , false);
      _shouldSync.Add("Buff"         , false);
      _shouldSync.Add("BuffStun"     , false);
    }

    public bool ShouldSync(string key) {
      Assert.IsTrue(_shouldSync.ContainsKey(key));
      return _shouldSync[key];
    }

    public void SyncCurHp() {
      _enemy.PhotonView.RPC("SyncCurHpRPC", PhotonTargets.Others, _enemy.Hp.Cur);
    }

    public void SyncUpdateHpView(PhotonPlayer skillOwner) {
      _enemy.PhotonView.RPC("SyncUpdateHpViewRPC", PhotonTargets.All, skillOwner);
    }

    public void SyncBuff() {
      _enemy.PhotonView.RPC("SyncBuffRPC", PhotonTargets.Others, _enemy.BuffState.Stun, _enemy.BuffState.Slow, _enemy.BuffState.Heavy);
    }

    public void SyncStun() {
      _enemy.PhotonView.RPC("SyncStunRPC", PhotonTargets.Others, _enemy.BuffState.Stun);
    }

    [PunRPC]
    private void SyncCurHpRPC(int cur) {
      ForceSync("CurHp", () => _enemy.Hp.ForceSyncCur(cur));
    }

    [PunRPC]
    private void SyncUpdateHpViewRPC(PhotonPlayer skillOwner) {
      ForceSync("UpdateHpView", () => _enemy.Hp.ForceSyncUpdateView(skillOwner));
    }

    [PunRPC]
    private void SyncBuffRPC(bool stun, bool slow, bool heavy) {
      ForceSync("Buff", () => _enemy.BuffState.ForceSync(stun, slow, heavy));
    }

    [PunRPC]
    private void SyncStunRPC(bool stun) {
      ForceSync("BuffStun", () => _enemy.BuffState.ForceSyncStun(stun));
    }

    private void ForceSync(string key, Action action) {
      _shouldSync[key] = true;
      action();
      _shouldSync[key] = false;
    }

    [SerializeField] private Enemy _enemy;
    private Dictionary<string, bool> _shouldSync;
  }
}

