using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour {
  public void Respawn(Action ActTransition) {
    MonoUtility.Instance.DelaySec(3.0f, () => {
      var pos = _gameData.RespawnPosition;
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        pos.x *= -1;

      gameObject.transform.position = pos;
      _hp.FullRecover();
      //_hp.UpdateView();

      ActTransition();
    });
  }

  [SerializeField] private PlayerStatus _status;
  [SerializeField] private PlayerHp _hp;
  [SerializeField] private GameData _gameData;
}

