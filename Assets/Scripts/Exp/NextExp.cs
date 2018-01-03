using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
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

    public void Add(int quantity) {
      if (_level.Lv >= 15) // 15 is MaxLv
        return;

      Cur += quantity;

      if (Cur >= Max) {
        photonView.RPC("SyncExpLvUp", PhotonTargets.All);
        return;
      }
      if (Cur < Min)
        Cur = Min;

      photonView.RPC("SyncExpCur", PhotonTargets.Others, Cur);
    }

    public void Subtract(int quantity) {
      Add(-quantity);
    }

    [PunRPC]
    protected void SyncExpLvUp() {
      _level.LvUp();
      _index += 1;
      Max = _table.Data[_index];
      Cur = Min;
    }

    [SerializeField] private DataTable _table;
    [SerializeField] private Level _level;
    public int Cur { get; private set; }
    public int Min { get; private set; }
    public int Max { get; private set; }
    private int _index;
  }
}

