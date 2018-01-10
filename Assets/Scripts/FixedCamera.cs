using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class FixedCamera : Photon.MonoBehaviour {
    void Start() {
      int sign = 1;
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        sign *= -1;

      transform.position = new Vector3(_gameData.RespawnPosition.x * sign, _gameData.RespawnPosition.y, _z);
    }

    [SerializeField] private GameData _gameData;
    [SerializeField] private float _z;
  }
}

