using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class GetPlayerName : MonoBehaviour {
    void Start() {
      _text.text = _photonView.owner.NickName;
    }

    [SerializeField] private PhotonView _photonView;
    [SerializeField] private Text _text;
  }
}

