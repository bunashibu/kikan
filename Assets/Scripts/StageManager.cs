using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StageManager : SingletonMonoBehaviour<StageManager> {
    void Awake() {
      StageName = "Battle";
      StageData = Resources.Load("Data/StageData/Battle") as StageData;

      _stage.Emerge();
      _finalStage.Hide();
    }

    void Update() {
      if (StageName == "Battle")
        UpdateStage();
    }

    private void UpdateStage() {
      if (_timePanel.TimeSec <= 0) {
        StageName = "FinalBattle";
        StageData = Resources.Load("Data/StageData/FinalBattle") as StageData;

        _stage.StartRotation();
        _finalStage.Emerge();
        _finalStage.StartRotation();
        _finalStage.GatherPlayer();
      }
    }

    public StageData StageData { get; private set; }
    public string    StageName { get; private set; }

    [SerializeField] private TimePanel _timePanel;
    [SerializeField] private Stage _stage;
    [SerializeField] private FinalStage _finalStage;
  }
}

