using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class BattleStreamBehaviour : SingletonMonoBehaviour<BattleStreamBehaviour> {
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

            if (killPlayer.PhotonView.isMine)
              killPlayer.AudioEnvironment.PlayOneShot("Kill1");

            bool isSameTeam = ((int)PhotonNetwork.player.CustomProperties["Team"] == killPlayer.PlayerInfo.Team) ? true : false;
            _killMessagePanel.InstantiateMessage(killPlayer, deathPlayer, isSameTeam);
          }
        })
        .AddTo(gameObject);

      BattleStream.OnDied
        .Subscribe(target => {
          // INFO: Force Kill in case desync occurs
          if (target.Hp.Cur.Value > 0) {
            target.Hp.Subtract(target.Hp.Max.Value);
            Debug.Log("ForceKill!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
          }

          if (target is Player) {
            var player = (Player)target;
            player.StateTransfer.TransitTo("Die", player.Animator);
          }
        })
        .AddTo(gameObject);

      BattleStream.OnDied
        .Where(_ => StageReference.Instance.StageData.Name == "Battle")
        .Subscribe(target => {
          if (target is Player) {
            var player = (Player)target;

            if (player.PhotonView.isMine) {
              var respawnPanel = Instantiate(_respawnPanel, _canvas.transform).GetComponent<RespawnPanel>();
              respawnPanel.SetRespawnTime(player.Level.Cur.Value);
              respawnPanel.SetRespawnPlayer(player);
            }
          }
        })
        .AddTo(gameObject);
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private KillMessagePanel _killMessagePanel;
    [SerializeField] private GameObject _respawnPanel;
  }
}

