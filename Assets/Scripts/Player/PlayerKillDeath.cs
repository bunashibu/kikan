using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerKillDeath : KillDeath {
  [PunRPC]
  private void SyncKillDeathInit(int kdViewID) {
    var kdPanel = PhotonView.Find(kdViewID).gameObject.GetComponent<KillDeathPanel>();
    _kdPanel = kdPanel;
  }

  public void Init(KillDeathPanel kdPanel) {
    Assert.IsTrue(photonView.isMine);

    Init();

    var kdViewID = kdPanel.GetComponent<PhotonView>().viewID;
    photonView.RPC("SyncKillDeathInit", PhotonTargets.All, kdViewID);
  }

  public void UpdateKillView() {
    _kdPanel.UpdateKillView(KillCount, photonView.owner);
  }

  public void UpdateDeathView() {
    _kdPanel.UpdateDeathView(DeathCount, photonView.owner);
  }

  private KillDeathPanel _kdPanel;
}

