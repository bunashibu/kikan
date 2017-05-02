using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Photon.MonoBehaviour {
  [PunRPC]
  public void Sync(bool flipX, PhotonPlayer skillUser) {
    gameObject.GetComponent<SpriteRenderer>().flipX = flipX;
    _skillUser = skillUser;
  }

  protected PhotonPlayer _skillUser;
}

