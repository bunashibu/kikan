using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  // Obsolete
  public class PlayerObserver : MonoBehaviour {
    void Awake() {
      _shouldSync = new Dictionary<string, bool>();

      _shouldSync.Add("Buff"         , false);
      _shouldSync.Add("BuffStun"     , false);
    }

    public bool ShouldSync(string key) {
      Assert.IsTrue(_shouldSync.ContainsKey(key));
      return _shouldSync[key];
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

