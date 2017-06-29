using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerSyncObserver : Photon.MonoBehaviour {
  public void SyncHp() {
    _player.PhotonView.RPC("SyncHpRPC", PhotonTargets.Others, _player.Hp.Cur, _player.Hp.Min, _player.Hp.Max);
  }

  public void SyncCurHp() {
    _player.PhotonView.RPC("SyncCurHpRPC", PhotonTargets.Others, _player.Hp.Cur);
  }

  public void SyncMaxHp() {
    _player.PhotonView.RPC("SyncMaxHpRPC", PhotonTargets.Others, _player.Hp.Max);
  }

  public void SyncIsDead() {
    _player.PhotonView.RPC("SyncIsDeadRPC", PhotonTargets.Others, _player.Hp.IsDead);
  }

  public void SyncUpdateHpView() {
    _player.PhotonView.RPC("SyncUpdateHpViewRPC", PhotonTargets.All);
  }

  [PunRPC]
  private void SyncHpRPC(int cur, int min, int max) {
    ForceSync( () => { _player.Hp.ForceSyncHp(cur, min, max); } );
  }

  [PunRPC]
  private void SyncCurHpRPC(int cur) {
    ForceSync( () => { _player.Hp.ForceSyncCur(cur); } );
  }

  [PunRPC]
  private void SyncMaxHpRPC(int max) {
    ForceSync( () => { _player.Hp.ForceSyncCur(max); } );
  }

  [PunRPC]
  private void SyncIsDeadRPC(bool isDead) {
    ForceSync( () => { _player.Hp.ForceSyncIsDead(isDead); } );
  }

  [PunRPC]
  private void SyncUpdateHpViewRPC() {
    ForceSync( () => { _player.Hp.ForceSyncUpdateView(); } );
  }

  private void ForceSync(Action action) {
    _syncCount += 1;
    action();
    _syncCount -= 1;
  }

  public bool ShouldSync {
    get {
      return (_syncCount > 0);
    }
  }

  [SerializeField] private BattlePlayer _player;
  private int _syncCount = 0;
}

