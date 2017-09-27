using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class BattlePlayerObserver : MonoBehaviour {
    void Awake() {
      _shouldSync = new Dictionary<string, bool>();
  
      _shouldSync.Add("Hp",  false);
      _shouldSync.Add("CurHp", false);
      _shouldSync.Add("MaxHp", false);
    }
  
    public void SyncHp() {
      _player.PhotonView.RPC("SyncHpRPC", PhotonTargets.Others, _player.Hp.Cur, _player.Hp.Min, _player.Hp.Max);
    }
  
    public void SyncCurHp() {
      _player.PhotonView.RPC("SyncCurHpRPC", PhotonTargets.Others, _player.Hp.Cur);
    }
  
    public void SyncMaxHp() {
      _player.PhotonView.RPC("SyncMaxHpRPC", PhotonTargets.Others, _player.Hp.Max);
    }
  
    public void SyncUpdateHpView() {
      _player.PhotonView.RPC("SyncUpdateHpViewRPC", PhotonTargets.Others);
    }
  
    [PunRPC]
    private void SyncHpRPC(int cur, int min, int max) {
      ForceSync("Hp", () => _player.Hp.ForceSync(cur, min, max));
    }
  
    [PunRPC]
    private void SyncCurHpRPC(int cur) {
      ForceSync("CurHp", () => _player.Hp.ForceSyncCur(cur));
    }
  
    [PunRPC]
    private void SyncMaxHpRPC(int max) {
      ForceSync("MaxHp", () => _player.Hp.ForceSyncMax(max));
    }
  
    [PunRPC]
    private void SyncUpdateHpViewRPC() {
      _player.Hp.UpdateView();
    }
  
    private void ForceSync(string key, Action action) {
      _shouldSync[key] = true;
      action();
      _shouldSync[key] = false;
    }
  
    public bool ShouldSync(string key) {
      Assert.IsTrue(_shouldSync.ContainsKey(key));
      return _shouldSync[key];
    }
  
    [SerializeField] private BattlePlayer _player;
    private Dictionary<string, bool> _shouldSync;
  }
}

