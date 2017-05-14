using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Photon.MonoBehaviour {
  [PunRPC]
  protected void SyncInit(bool flipX, int viewID) {
    gameObject.GetComponent<SpriteRenderer>().flipX = flipX;
    _viewID = viewID;
  }

  public void Init(bool flipX, int viewID) {
    photonView.RPC("SyncInit", PhotonTargets.All, flipX, viewID);
  }

  protected int _viewID;
}

