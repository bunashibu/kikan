using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StageManager : MonoBehaviour {
    void Awake() {
      _stage.Emerge();
      _finalStage.Hide();
    }

    void Update() {
      if (_timePanel.TimeSec <= 3)
        _stage.StartRotation();

      if (_timePanel.TimeSec <= 0) {
        _finalStage.Emerge();
        _stage.Hide();
      }
    }

    [SerializeField] private TimePanel _timePanel;
    [SerializeField] private Stage _stage;
    [SerializeField] private FinalStage _finalStage;
  }
}

