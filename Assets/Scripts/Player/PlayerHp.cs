using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHp : Hp {
  public void Init(int life, Bar hudBar) {
    Assert.IsTrue(photonView.isMine);

    photonView.RPC("SyncHpInit", PhotonTargets.All, life, 0, life);

    _hudBar = hudBar;
    _worldBar.gameObject.SetActive(false);
  }

  [PunRPC]
  private void SyncHpUpdate() {
    if (photonView.isMine)
      _hudBar.UpdateView(Cur, Max);
    else
      _worldBar.UpdateView(Cur, Max);
  }

  public void UpdateView() {
    photonView.RPC("SyncHpUpdate", PhotonTargets.All);
  }

  public void FullRecovery() {
    Plus(Max);
  }

  [SerializeField] private Bar _worldBar;
  private Bar _hudBar;
}

