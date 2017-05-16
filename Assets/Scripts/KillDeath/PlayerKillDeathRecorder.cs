using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerKillDeathRecorder : KillDeathRecorder {
  [PunRPC]
  private void SyncKDRecInit(int kdViewID) {
    var kdPanel = PhotonView.Find(kdViewID).gameObject.GetComponent<KillDeathPanel>();
    _kdPanel = kdPanel;
  }

  public void Init(KillDeathPanel kdPanel) {
    Assert.IsTrue(photonView.isMine);

    Init();

    var kdViewID = kdPanel.GetComponent<PhotonView>().viewID;
    photonView.RPC("SyncKDRecInit", PhotonTargets.All, kdViewID);
  }

  public override void RecordKill() {
    base.RecordKill();
    _kdPanel.UpdateKill(KillCnt);
  }

  public override void RecordDeath() {
    base.RecordDeath();
    _kdPanel.UpdateDeath(DeathCnt);
  }

  private KillDeathPanel _kdPanel;
}

