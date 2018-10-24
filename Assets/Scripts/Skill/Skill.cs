using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class Skill : Photon.MonoBehaviour {
    [PunRPC]
    protected void SyncInit(bool flipX, int viewID) {
      gameObject.GetComponent<SpriteRenderer>().flipX = flipX;
      _skillUserObj = PhotonView.Find(viewID).gameObject;
      _skillUserViewID = viewID;
    }

    public void Init(bool flipX, int viewID) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncInit", PhotonTargets.All, flipX, viewID);
    }

    protected GameObject _skillUserObj;
    protected int _skillUserViewID;
  }
}

