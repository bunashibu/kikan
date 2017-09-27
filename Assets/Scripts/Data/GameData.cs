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
  
    [SerializeField] private Vector3 _respawnPosition;
  }
}

