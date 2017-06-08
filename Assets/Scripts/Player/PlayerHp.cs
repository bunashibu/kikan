using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerHp : Hp {
  public void Init(Bar hudBar) {
    Assert.IsTrue(photonView.isMine);

    photonView.RPC("SyncHpAll", PhotonTargets.All, _hpTable.Data[0], 0, _hpTable.Data[0]);

    _hudBar = hudBar;
    _worldBar.gameObject.SetActive(false);
  }

  public void UpdateMaxHp() {
    double ratio = (double)((_core.Hp + 100) / 100.0);
    int maxHp = (int)(_hpTable.Data[_level.Lv - 1] * ratio);

    photonView.RPC("SyncHpMax", PhotonTargets.All, maxHp);
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

  [SerializeField] private DataTable _hpTable;
  [SerializeField] private PlayerLevel _level;
  [SerializeField] private PlayerCore _core;
  [SerializeField] private Bar _worldBar;
  private Bar _hudBar;
}

