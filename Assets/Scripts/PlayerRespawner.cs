using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour {
  void Awake() {
    Active = true;
    Ready = false;
  }

  public void Respawn() {
    MonoUtility.Instance.DelaySec(3.0f, () => {
      var pos = _gameData.RespawnPosition;
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        pos.x *= -1;

      gameObject.transform.position = pos;
      _health.FullRecovery();
      _health.Show();
      Ready = true;
    });
  }

  [SerializeField] PlayerStatus _status;
  [SerializeField] PlayerHealth _health;
  [SerializeField] GameData _gameData;
  public bool Active { get; set; }
  public bool Ready { get; set; }
}

