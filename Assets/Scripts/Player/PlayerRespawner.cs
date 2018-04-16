using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerRespawner : MonoBehaviour {
    public void Respawn(Action ActTransition) {
      MonoUtility.Instance.DelaySec((float)_player.Level.Lv, () => {
        var pos = StageReference.Instance.StageData.RespawnPosition;
        if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
          pos.x *= -1;

        gameObject.transform.position = pos;

        _player.BodyCollider.enabled = true;

        _player.Hp.FullRecover();
        _player.Observer.SyncCurHp();

        _player.Hp.UpdateView();
        _player.Observer.SyncUpdateHpView();

        _player.BuffState.Reset();

        ActTransition();
      });
    }

    [SerializeField] private BattlePlayer _player;
  }
}

