using UnityEngine;
using System.Collections;

public class PlayerHealth : Health {
  public void Init(int life, Bar hudBar) {
    photonView.RPC("SyncInit", PhotonTargets.All, life, 0, life);

    _hudBar = hudBar;

    if (photonView.isMine)
      _worldBar.gameObject.SetActive(false);
  }

  [PunRPC]
  private void SyncShow() {
    if (photonView.isMine)
      _hudBar.Show(Cur, Max);
    else
      _worldBar.Show(Cur, Max);
  }

  public void Show() {
    photonView.RPC("SyncShow", PhotonTargets.All);
  }

  public void FullRecovery() {
    Plus(Max);
  }

  [SerializeField] private Bar _worldBar;
  private Bar _hudBar;
}

