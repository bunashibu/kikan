using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Bunashibu.Kikan {
  public class PlayerSynchronizer : Photon.MonoBehaviour {
    void Awake() {
      _coreSubject = new Subject<CoreType>();
      _autoHealSubject = new Subject<int>();
    }

    [PunRPC]
    private void SyncCoreLevelUpRPC(CoreType type) {
      _coreSubject.OnNext(type);
    }

    public void SyncCoreLevelUp(CoreType type) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncCoreLevelUpRPC", PhotonTargets.AllViaServer, type);
    }

    [PunRPC]
    private void SyncAutoHealRPC(int quantity) {
      _autoHealSubject.OnNext(quantity);
    }

    public void SyncAutoHeal(int quantity) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncAutoHealRPC", PhotonTargets.AllViaServer, quantity);
    }

    public IObservable<CoreType> OnCoreLevelUpped => _coreSubject;
    public IObservable<int> OnAutoHealed => _autoHealSubject;

    private Subject<CoreType> _coreSubject;
    private Subject<int> _autoHealSubject;
  }
}

