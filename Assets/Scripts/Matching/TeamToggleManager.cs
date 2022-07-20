using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class TeamToggleManager : Photon.MonoBehaviour {
    void Start() {
      _redToggle.onValueChanged.AddListener((state) => {
        Restrict(state, _blueToggle);
        SetHope(0);
      });

      _blueToggle.onValueChanged.AddListener((state) => {
        Restrict(state, _redToggle);
        SetHope(1);
      });
    }

    private void Restrict(bool state, Toggle target) {
      if (state && target.isOn)
        target.isOn = false;
    }

    private void SetHope(int hopeTeam) {
      var props = new Hashtable() {{ "HopeTeam", hopeTeam }};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    [SerializeField] private Toggle _redToggle;
    [SerializeField] private Toggle _blueToggle;
  }
}
