using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class Skill : Photon.MonoBehaviour {
    [PunRPC]
    protected void SyncInitRPC(int viewID) {
      _skillUserObj = PhotonView.Find(viewID).gameObject;
      _skillUserViewID = viewID;
    }

    public void SyncInit(int viewID) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncInitRPC", PhotonTargets.All, viewID);
    }

    public int viewID => photonView.viewID;

    protected GameObject _skillUserObj;
    protected int _skillUserViewID;
  }
}

