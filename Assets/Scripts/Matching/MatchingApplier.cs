using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class MatchingApplier : Photon.MonoBehaviour {
    void Start() {
      _applyButtonList[0].onClick.AddListener( () => Apply(ApplyType.Practice) );
      _applyButtonList[1].onClick.AddListener( () => Apply(ApplyType.VS1)      );
      _applyButtonList[2].onClick.AddListener( () => Apply(ApplyType.VS2)      );
      _applyButtonList[3].onClick.AddListener( () => Apply(ApplyType.VS3)      );
    }

    public ApplyType CurApplyingType => _curApplyingType;

    private void Apply(ApplyType applyType) {
      _curApplyingType = applyType;
      photonView.RPC("Approve", PhotonTargets.MasterClient, PhotonNetwork.player, applyType);
    }

    [SerializeField] private List<Button> _applyButtonList;
    private ApplyType _curApplyingType;
  }

  public enum ApplyType {
    Practice,
    VS1,
    VS2,
    VS3
  }
}

