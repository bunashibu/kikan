using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class TeamToggleManager : Photon.MonoBehaviour {
    void Start() {
      var prevHope = PhotonNetwork.player.CustomProperties["HopeTeam"];

      if (prevHope == null)
        SetHope(-1);
      else {
        int hope = (int)prevHope;
        if (hope == 0)
          _redToggle.isOn = true;
        else if (hope == 1)
          _blueToggle.isOn = true;
      }

      _redToggle.onValueChanged.AddListener((state) => {
        UpdateState(state, _blueToggle, 0);
        Fallback();
      });

      _blueToggle.onValueChanged.AddListener((state) => {
        UpdateState(state, _redToggle, 1);
        Fallback();
      });
    }

    private void UpdateState(bool state, Toggle target, int hopeTeam) {
      if (state) {
        SetHope(hopeTeam);

        if (target.isOn)
          target.isOn = false;
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
