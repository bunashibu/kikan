using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class LobbyStageManager : SingletonMonoBehaviour<LobbyStageManager>, IStageManager {
    protected override void Awake() {
      StageData = Resources.Load("Data/StageData/Lobby") as StageData;
    }

    public StageData StageData { get; private set; }
  }
}
