using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StageManager : SingletonMonoBehaviour<StageManager> {
    void Awake() {
      _stageName = "Battle";
      StageData = Resources.Load("Data/StageData/Battle") as StageData;

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
        StageData = Resources.Load("Data/StageData/FinalBattle") as StageData;
      }
    }

    public StageData StageData { get; private set; }

    [SerializeField] private TimePanel _timePanel;
    [SerializeField] private Stage _stage;
    [SerializeField] private FinalStage _finalStage;
    private string _stageName;
  }
}

