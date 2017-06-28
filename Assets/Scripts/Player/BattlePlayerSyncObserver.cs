using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerSyncObserver : Photon.MonoBehaviour {
  public void SyncAllHp() {
    _player.PhotonView.RPC("SyncCurHpRPC", PhotonTargets.Others, _player.Hp.Cur, _player.Hp.Min, _player.Hp.Max);
  }

  public void SyncCurHp() {
    _player.PhotonView.RPC("SyncCurHpRPC", PhotonTargets.Others, _player.Hp.Cur);
  }

  public void SyncMaxHp() {
    _player.PhotonView.RPC("SyncMaxHpRPC", PhotonTargets.Others, _player.Hp.Max);
  }

  [PunRPC]
  private void SyncAllHpRPC(int cur, int min, int max) {
    _player.Hp.Plus(cur - _player.Hp.Cur);
    _player.Hp.Plus(min - _player.Hp.Min);
    _player.Hp.Plus(max - _player.Hp.Max);
  }

  [PunRPC]
  private void SyncCurHpRPC(int cur) {
    _player.Hp.Plus(cur - _player.Hp.Cur);
  }

  [PunRPC]
  private void SyncMaxHpRPC(int max) {
    _player.Hp.Plus(max - _player.Hp.Max);
  }






  /*
  [PunRPC]
  protected void SyncHpDead(bool dead) {
    Dead = dead;
  }

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

