using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  // Obsolete
  public class PlayerObserver : MonoBehaviour, IBuffObserver {
    void Awake() {
      _shouldSync = new Dictionary<string, bool>();

      _shouldSync.Add("Team"         , false);
      _shouldSync.Add("Buff"         , false);
      _shouldSync.Add("BuffStun"     , false);
    }

    public bool ShouldSync(string key) {
      Assert.IsTrue(_shouldSync.ContainsKey(key));
      return _shouldSync[key];
    }

    /* Team */
    public void SyncTeam() {
      _player.PhotonView.RPC("SyncTeamRPC", PhotonTargets.Others, _player.PlayerInfo.Team);
    }

    /* Buff */
    public void SyncBuff() {
      _player.PhotonView.RPC("SyncBuffRPC", PhotonTargets.Others, _player.BuffState.Stun, _player.BuffState.Slow, _player.BuffState.Heavy);
    }

    public void SyncStun() {
      _player.PhotonView.RPC("SyncStunRPC", PhotonTargets.Others, _player.BuffState.Stun);
    }

    /* Other */
    public void SyncNameBackground() {
      _player.PhotonView.RPC("SyncNameBackgroundRPC", PhotonTargets.Others, _player.NameBackground.ColorName);
    }

    public void SyncRigidSimulated() {
      _player.PhotonView.RPC("SyncRigidSimulatedRPC", PhotonTargets.Others, _player.Rigid.simulated);
    }

    public void SyncBodyCollider() {
      _player.PhotonView.RPC("SyncBodyColliderRPC", PhotonTargets.Others, _player.BodyCollider.enabled);
    }

    public void SyncKillSE() {
      _player.PhotonView.RPC("SyncKillSERPC", PhotonTargets.All);
    }

    /* Team RPC */
    [PunRPC]
    private void SyncTeamRPC(int team) {
      ForceSync("Team", () => _player.PlayerInfo.ForceSync(team));
    }

    /* Buff RPC */
    [PunRPC]
    private void SyncBuffRPC(bool stun, bool slow, bool heavy) {
      ForceSync("Buff", () => _player.BuffState.ForceSync(stun, slow, heavy));
    }

    [PunRPC]
    private void SyncStunRPC(bool stun) {
      ForceSync("BuffStun", () => _player.BuffState.ForceSyncStun(stun));
    }

    /* Other */
    [PunRPC]
    private void SyncNameBackgroundRPC(string colorName) {
      _player.NameBackground.SetColor(colorName);
    }

    [PunRPC]
    private void SyncRigidSimulatedRPC(bool flag) {
      _player.Rigid.simulated = flag;
    }

    [PunRPC]
    private void SyncBodyColliderRPC(bool flag) {
      _player.BodyCollider.enabled = flag;
    }

    [PunRPC]
    private void SyncKillSERPC() {
      if (_player.PhotonView.isMine)
        _player.AudioEnvironment.PlayOneShot("Kill1");
    }

    private void ForceSync(string key, Action action) {
      _shouldSync[key] = true;
      action();
      _shouldSync[key] = false;
    }

    [SerializeField] private Player _player;
    private Dictionary<string, bool> _shouldSync;
  }
}

