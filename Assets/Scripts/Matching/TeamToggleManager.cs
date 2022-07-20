using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class TeamToggleManager : Photon.MonoBehaviour {
    void Start() {
      SetHope(-1);

      _redToggle.onValueChanged.AddListener((state) => {
        Restrict(state, _blueToggle, 0);
        Fallback();
      });

      _blueToggle.onValueChanged.AddListener((state) => {
        Restrict(state, _redToggle, 1);
        Fallback();
      });
    }

    private void Restrict(bool state, Toggle target, int hopeTeam) {
      if (state && target.isOn) {
        target.isOn = false;
        SetHope(hopeTeam);
      }
    }

    private void Fallback() {
      if (!_redToggle.isOn && !_blueToggle.isOn)
        SetHope(-1);
    }

    private void SetHope(int hopeTeam) {
      var props = new Hashtable() {{ "HopeTeam", hopeTeam }};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    [SerializeField] private Toggle _redToggle;
    [SerializeField] private Toggle _blueToggle;
  }
}
