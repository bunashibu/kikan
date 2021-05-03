using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class GameData : SingletonMonoBehaviour<GameData> {
    public string GameVersion { get { return _gameVersion; } }

    private readonly string _gameVersion = "v0.7.0";
  }
}
