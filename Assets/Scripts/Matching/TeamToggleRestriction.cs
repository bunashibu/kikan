using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class TeamToggleRestriction : MonoBehaviour {
    void Start() {
      _redToggle.onValueChanged.AddListener((state) => Restrict(state, _blueToggle));
      _blueToggle.onValueChanged.AddListener((state) => Restrict(state, _redToggle));
    }

    private void Restrict(bool state, Toggle target) {
      if (state && target.isOn)
        target.isOn = false;
    }

    [SerializeField] private Toggle _redToggle;
    [SerializeField] private Toggle _blueToggle;
  }
}
