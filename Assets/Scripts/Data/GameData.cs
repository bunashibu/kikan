using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class GameData : ScriptableObject {
    public Vector3 RespawnPosition {
      get {
        return _respawnPosition;
      }
    }

    public Rect StageRect {
      get {
        return _stageRect;
      }
    }

    [SerializeField] private Vector3 _respawnPosition;
    [SerializeField] private Rect _stageRect;
  }
}

