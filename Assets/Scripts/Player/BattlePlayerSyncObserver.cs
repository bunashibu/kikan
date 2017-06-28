using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerSyncObserver : Photon.MonoBehaviour {
  public void SyncPlayerHp() {
    _player.PhotonView.RPC("SyncPlayerHpRPC", PhotonTargets.Others, _player.Hp.Cur, _player.Hp.Min, _player.Hp.Max);
  }

  public void SyncPlayerCurHp() {
    _player.PhotonView.RPC("SyncPlayerCurHpRPC", PhotonTargets.Others, _player.Hp.Cur);
  }

  public void SyncPlayerMaxHp() {
    _player.PhotonView.RPC("SyncPlayerMaxHpRPC", PhotonTargets.Others, _player.Hp.Max);
  }

  public void SyncPlayerIsDead() {
    _player.PhotonView.RPC("SyncPlayerIsDeadRPC", PhotonTargets.Others, _player.Hp.IsDead);
  }

  [PunRPC]
  private void SyncPlayerHpRPC(int cur, int min, int max) {
    _player.Hp.ForceSyncHp(cur, min, max);
  }

  [PunRPC]
  private void SyncPlayerCurHpRPC(int cur) {
    _player.Hp.ForceSyncCur(cur);
  }

  [PunRPC]
  private void SyncPlayerMaxHpRPC(int max) {
    _player.Hp.ForceSyncCur(max);
  }

  [PunRPC]
  private void SyncPlayerIsDeadRPC(bool isDead) {
    _player.Hp.ForceSyncIsDead(isDead);
  }

  /*
  [PunRPC]
  private void SyncHpUpdate() {
    if (photonView.isMine)
      _hudBar.UpdateView(Cur, Max);
    else
      _worldBar.UpdateView(Cur, Max);
  }
  */

  [SerializeField] private BattlePlayer _player;
}

