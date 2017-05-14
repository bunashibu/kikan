using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDeathRecoder : Photon.MonoBehaviour {
  void Awake() {
    KillCnt = 0;
    DeathCnt = 0;
  }

  [PunRPC]
  private void SyncKillCnt(int killCnt) {
    KillCnt = killCnt;
  }

  [PunRPC]
  private void SyncDeathCnt(int deathCnt) {
    DeathCnt = deathCnt;
  }

  public void RecordKill() {
    KillCnt += 1;
    photonView.RPC("SyncKillCnt", PhotonTargets.Others, KillCnt);
  }

  public void RecordDeath() {
    DeathCnt += 1;
    photonView.RPC("SyncDeathCnt", PhotonTargets.Others, DeathCnt);
  }

  public int KillCnt { get; private set; }
  public int DeathCnt { get; private set; }
}

