using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class BattleStreamBehaviour : SingletonMonoBehaviour<BattleStreamBehaviour> {
    void Start() {
      BattleStream.OnKilledAndDied
        .Subscribe(flow => {
          if (flow.Attacker is Player killPlayer) {
            bool isSameTeam = ((int)PhotonNetwork.player.CustomProperties["Team"] == killPlayer.PlayerInfo.Team) ? true : false;
            bool isClient = killPlayer == Client.Player;

            if (flow.Target is IKillRewardGiver giver) {
              var killReward = KillRewardEnvironment.GetRewardFrom(giver, Client.Teammates.Count);

              KillRewardEnvironment.GiveMainRewardTo(killPlayer, killReward);
              foreach (var teammate in Client.GetTeammates(killPlayer))
                KillRewardEnvironment.GiveSubRewardTo(teammate, killReward);
            }

            if (flow.Target is Player deathPlayer) {
              killPlayer.KillCount.Value += 1;
              deathPlayer.DeathCount.Value += 1;

              if (killPlayer.PhotonView.isMine)
                killPlayer.AudioEnvironment.PlayOneShot("Kill1");

              _killMessagePanel.InstantiateMessage(killPlayer, deathPlayer, isSameTeam);
            }
          }
        })
        .AddTo(gameObject);

      BattleStream.OnDied
        .Subscribe(target => {
          // INFO: Force Kill in case desync occurs
          if (target.Hp.Cur.Value > 0)
            target.Hp.Subtract(target.Hp.Max.Value);
        })
        .AddTo(gameObject);

      BattleStream.OnDied
        .Where(_ => StageReference.Instance.StageData.Name == "Battle")
        .Subscribe(target => {
          if (target is Player player && player.PhotonView.isMine) {
            var respawnPanel = Instantiate(_respawnPanel, _canvas.transform).GetComponent<RespawnPanel>();
            respawnPanel.SetRespawnTime(player.Level.Cur.Value);
            respawnPanel.SetRespawnPlayer(player);
          }
        })
        .AddTo(gameObject);

      BattleStream.OnDied
        .Where(_ => PhotonNetwork.player.IsMasterClient)
        .Where(_ => StageReference.Instance.StageData.Name == "FinalBattle")
        .Where(_ => WinLoseJudger.Instance != null)
        .Subscribe(target => {
          if (target is Player player)
            WinLoseJudger.Instance.UpdateAlivePlayerCount(player);
        })
        .AddTo(gameObject);
    }

    [SerializeField] private Canvas _canvas;
    [SerializeField] private KillMessagePanel _killMessagePanel;
    [SerializeField] private GameObject _respawnPanel;
  }
}
