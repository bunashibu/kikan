using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Bunashibu.Kikan {
  public class PlayerDieSMB : StateMachineBehaviour {
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      if (_player == null)
        _player = animator.GetComponent<Player>();

      _player.BodyCollider.enabled = false;

      if (_player.PhotonView.isMine) {
        _player.AudioEnvironment.PlayOneShot("Die", 0.5f);
        SkillReference.Instance.DeleteAll();
      }

      if (StageReference.Instance.StageData.Name == "FinalBattle")
        WinLoseJudger.Instance.UpdateAlivePlayerCount(_player);
    }

    private Player _player;
  }
}

