using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class KillDeath : Photon.MonoBehaviour {
    public void Init() {
      KillCount = 0;
      DeathCount = 0;
    }

    [PunRPC]
    protected void SyncKillCount(int killCount) {
      KillCount = killCount;
    }

    [PunRPC]
    protected void SyncDeathCount(int deathCount) {
      DeathCount = deathCount;
    }

    public void RecordKill() {
      KillCount += 1;
      photonView.RPC("SyncKillCount", PhotonTargets.Others, KillCount);
    }

    public void RecordDeath() {
      DeathCount += 1;
      photonView.RPC("SyncDeathCount", PhotonTargets.Others, DeathCount);
    }

    public int KillCount { get; private set; }
    public int DeathCount { get; private set; }
  }
}

