using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DieSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null) {
        _player     = animator.GetComponent<BattlePlayer>();
        _respawner  = animator.GetComponent<PlayerRespawner>();
      }

      _player.BodyCollider.enabled = false;

      Action action = () => { _player.StateTransfer.TransitTo("Idle", animator); };
      _respawner.Respawn(action);

      Debug.Log("Die");
    }

    private BattlePlayer _player;
    private PlayerRespawner _respawner;
  }
}

