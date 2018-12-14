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
    }

    [PunRPC]
    private void SyncCoreLevelUpRPC(CoreType type) {
      _coreSubject.OnNext(type);
    }

    public void SyncCoreLevelUp(CoreType type) {
      Assert.IsTrue(photonView.isMine);

      photonView.RPC("SyncCoreLevelUpRPC", PhotonTargets.All, type);
    }

    public IObservable<CoreType> OnCoreLevelUpped => _coreSubject;

    private Subject<CoreType> _coreSubject;
  }
}

