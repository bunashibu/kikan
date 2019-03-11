using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class BattleStageManager : SingletonMonoBehaviour<BattleStageManager>, IStageManager {
    void Awake() {
      _photonView = GetComponent<PhotonView>();
      StageData = Resources.Load("Data/StageData/Battle") as StageData;

      _stage.Emerge();
      _finalStage.Hide();
    }

    void Update() {
      if (PhotonNetwork.isMasterClient && StageData.Name == "Battle" && _timePanel.TimeSec <= 0)
        SyncFinalMigration();
    }

    [PunRPC]
    private void SyncFinalMigrationRPC() {
      SwapStage();
      _timePanel.SetTime(0);
      _timePanel.UpdateTimePanel();
      _finalStage.Prepare();
    }

    private void SyncFinalMigration() {
      _photonView.RPC("SyncFinalMigrationRPC", PhotonTargets.AllViaServer);
    }

    private void SwapStage() {
      StageData = Resources.Load("Data/StageData/FinalBattle") as StageData;

      _stage.StartRotation();
      _finalStage.Emerge();
      _finalStage.StartRotation();
    }

    public StageData StageData { get; private set; }

    [SerializeField] private TimePanel _timePanel;
    [SerializeField] private BattleStage _stage;
    [SerializeField] private BattleFinalStage _finalStage;
    private PhotonView _photonView;
  }
}

