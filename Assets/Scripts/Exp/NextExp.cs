using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Level))]
public class NextExp : Photon.MonoBehaviour, IGauge<int> {
  [PunRPC]
  protected void SyncExpInit() {
    _index = 0;
    Cur = 0;
    Min = 0;
    Max = _table.Data[_index];
  }

  [PunRPC]
  protected void SyncExpCur(int cur) {
    Cur = cur;
  }

  public void Plus(int quantity) {
    Cur += quantity;

    if (Cur >= Max) {
      photonView.RPC("SyncExpLvUp", PhotonTargets.All);
      return;
    }
    if (Cur < Min)
      Cur = Min;

    photonView.RPC("SyncExpCur", PhotonTargets.Others, Cur);
  }

  public void Minus(int quantity) {
    Plus(-quantity);
  }

  [PunRPC]
  protected void SyncExpLvUp() {
    _level.LvUp();
    _index += 1;
    Max = _table.Data[_index];
    Cur = Min;
  }

  [SerializeField] private ExpTable _table;
  [SerializeField] private Level _level;
  public int Cur { get; private set; }
  public int Min { get; private set; }
  public int Max { get; private set; }
  private int _index;
}

