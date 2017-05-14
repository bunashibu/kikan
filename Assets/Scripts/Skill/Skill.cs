using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Photon.MonoBehaviour {
  [PunRPC]
  protected void SyncInit(bool flipX, PhotonPlayer skillUser) {
    gameObject.GetComponent<SpriteRenderer>().flipX = flipX;
    _skillUser = skillUser;
  }

  public void Init(bool flipX, PhotonPlayer skillUser) {
    photonView.RPC("SyncInit", PhotonTargets.All, flipX, skillUser);
  }

  protected PhotonPlayer _skillUser;
}

