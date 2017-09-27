using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class Skill : Photon.MonoBehaviour {
    [PunRPC]
    protected void SyncInit(bool flipX, int viewID, int team) {
      gameObject.GetComponent<SpriteRenderer>().flipX = flipX;
      _skillUser = PhotonView.Find(viewID).gameObject;
      _team = team;
    }
  
    public void Init(bool flipX, int viewID) {
      Assert.IsTrue(photonView.isMine);
  
      var team = (int)PhotonNetwork.player.CustomProperties["Team"];
      photonView.RPC("SyncInit", PhotonTargets.All, flipX, viewID, team);
    }
  
    protected GameObject _skillUser;
    protected int _team;
  }
}

