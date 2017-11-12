using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class EnemyObserver : MonoBehaviour {
    void Awake() {
      _shouldSync = new Dictionary<string, bool>();

      _shouldSync.Add("CurHp", false);
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

    [PunRPC]
    private void SyncCurHpRPC(int cur) {
      ForceSync("CurHp", () => _enemy.Hp.ForceSyncCur(cur));
    }

    [PunRPC]
    private void SyncUpdateHpViewRPC(PhotonPlayer skillOwner) {
      _enemy.Hp.SyncUpdateView(skillOwner);
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

