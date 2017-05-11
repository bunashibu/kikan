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
      gameObject.transform.position = _gameData.RespawnPosition; // turn by team
      Ready = true;
    });
  }

  [SerializeField] PlayerStatus _status;
  [SerializeField] PlayerHealth _health;
  [SerializeField] GameData _gameData;
  public bool Active { get; set; }
  public bool Ready { get; set; }
}

