using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class GameData : SingletonMonoBehaviour<GameData> {
    public string GameVersion { get { return _gameVersion; } }

    private readonly string _gameVersion = "0.2.1dev";
  }
}

