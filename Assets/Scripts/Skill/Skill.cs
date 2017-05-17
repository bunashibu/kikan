using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Skill : Photon.MonoBehaviour {
  [PunRPC]
  protected void SyncInit(bool flipX, int viewID) {
    gameObject.GetComponent<SpriteRenderer>().flipX = flipX;
    _viewID = viewID;
  }

  public void Init(bool flipX, int viewID) {
    Assert.IsTrue(photonView.isMine);

    photonView.RPC("SyncInit", PhotonTargets.All, flipX, viewID);
  }

  protected int _viewID;
}

