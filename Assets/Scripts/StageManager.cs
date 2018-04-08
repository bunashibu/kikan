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
      if (_stageName == "Battle")
        UpdateStage();
    }

    private void UpdateStage() {
      if (_timePanel.TimeSec <= 0) {
        _stage.StartRotation();
        _finalStage.Emerge();
        _finalStage.StartRotation();
        _stageName = "FinalBattle";
      }
    }

    [SerializeField] private TimePanel _timePanel;
    [SerializeField] private Stage _stage;
    [SerializeField] private FinalStage _finalStage;
    private string _stageName = "Battle";
  }
}

