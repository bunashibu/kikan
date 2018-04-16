using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(IStageManager))]
  public class StageReference : SingletonMonoBehaviour<StageReference> {
    void Start() {
      _stageManager = gameObject.GetComponent<IStageManager>();
    }

    public StageData StageData { get { return _stageManager.StageData; } }

    [SerializeField] private IStageManager _stageManager;
  }
}

