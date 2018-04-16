using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class StageData : ScriptableObject {
    public string Name {
      get {
        return _name;
      }
    }

    public Vector3 RespawnPosition {
      get {
        return _respawnPosition;
      }
    }

    public Vector3 ResetPosition {
      get {
        return _resetPosition;
      }
    }

    public Rect StageRect {
      get {
        return _stageRect;
      }
    }

    [SerializeField] private string _name;
    [SerializeField] private Vector3 _respawnPosition;
    [SerializeField] private Vector3 _resetPosition;
    [SerializeField] private Rect _stageRect;
  }
}

