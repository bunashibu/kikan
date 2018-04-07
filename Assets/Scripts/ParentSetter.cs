using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ParentSetter : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncParent(int viewID) {
      SetParent(viewID);
    }

    public void SetParent(int viewID) {
      SetParentImpl(viewID);
      photonView.RPC("SyncParent", PhotonTargets.Others, viewID);
    }

    private void SetParentImpl(int viewID) {
      var parentObject = PhotonView.Find(viewID).gameObject;
      transform.SetParent(parentObject.transform, false);
    }
  }
}

