using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class BattleStageManager : SingletonMonoBehaviour<BattleStageManager>, IStageManager {
    void Awake() {
      _photonView = GetComponent<PhotonView>();
      StageData = Resources.Load("Data/StageData/Battle") as StageData;

      this.UpdateAsObservable()
        .Where(_ => PhotonNetwork.isMasterClient)
        .Where(_ => StageData.Name == "Battle")
        .Where(_ => _timePanel.TimeSec <= 0)
        .Take(1)
        .Subscribe(_ => SyncFinalMigration() );

      _stage.Emerge();
      _finalStage.Hide();
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

