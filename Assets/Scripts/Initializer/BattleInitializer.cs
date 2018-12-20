using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class BattleInitializer : SingletonMonoBehaviour<BattleInitializer> {
    void Start() {
      BattleStream.OnKilledAndDied
        .Subscribe(entity => {
          if (entity.Attacker is IKillRewardTaker && entity.Target is IKillRewardGiver) {
            var taker = (IKillRewardTaker)entity.Attacker;
            var giver = (IKillRewardGiver)entity.Target;

            KillRewardEnvironment.GiveRewardTo(taker, KillRewardEnvironment.GetRewardFrom(giver, taker.RewardTeammates.Count));
          }

          if (entity.Attacker is Player && entity.Target is Player) {
            var killPlayer  = (Player)entity.Attacker;
            var deathPlayer = (Player)entity.Target;

            killPlayer.KillCount.Value += 1;
            deathPlayer.DeathCount.Value += 1;

            killPlayer.AudioEnvironment.PlayOneShot("Kill1");

            bool isSameTeam = ((int)PhotonNetwork.player.CustomProperties["Team"] == killPlayer.PlayerInfo.Team) ? true : false;
            _killMessagePanel.InstantiateMessage(killPlayer, deathPlayer, isSameTeam);
          }
        });
    }

    [SerializeField] private KillMessagePanel _killMessagePanel;
  }
}

