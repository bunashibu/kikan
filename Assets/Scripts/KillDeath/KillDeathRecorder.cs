using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDeathRecorder : Photon.MonoBehaviour {
  public void Init() {
    KillCnt = 0;
    DeathCnt = 0;
  }

  [PunRPC]
  protected void SyncKillCnt(int killCnt) {
    KillCnt = killCnt;
  }

  [PunRPC]
  protected void SyncDeathCnt(int deathCnt) {
    DeathCnt = deathCnt;
  }

  public virtual void RecordKill() {
    KillCnt += 1;
    photonView.RPC("SyncKillCnt", PhotonTargets.Others, KillCnt);
  }

  public virtual void RecordDeath() {
    DeathCnt += 1;
    photonView.RPC("SyncDeathCnt", PhotonTargets.Others, DeathCnt);
  }

  public int KillCnt { get; private set; }
  public int DeathCnt { get; private set; }
}

