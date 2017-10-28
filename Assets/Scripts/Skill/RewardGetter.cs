using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class RewardGetter {
    public void SetRewardReceiver(BattlePlayer receiver) {
      _receiver = receiver;
    }

    public void GetRewardFrom(IKillReward target) {
      var teammateList = GetTeammateList();
      int teamSize = teammateList.Count;

      double ratio = 1;
      if (teamSize == 1)
        ratio = 0.7;
      if (teamSize == 2)
        ratio = 0.6;

      int receiverExp = (int)(target.KillExp * ratio);
      GiveExpToReceiver(receiverExp);

      int receiverGold = (int)(target.KillGold * ratio);
      GiveGoldToReceiver(receiverGold);

      if (teamSize > 0) {
        int teammateExp = (int)((target.KillExp - receiverExp) / teamSize);
        GiveExpToTeammate(teammateExp, teammateList);

        int teammateGold = (int)((target.KillGold - receiverGold) / teamSize);
        GiveGoldToTeammate(teammateGold, teammateList);
      }
    }

    private List<BattlePlayer> GetTeammateList() {
      var teammateList = new List<BattlePlayer>();

      foreach (var player in PhotonNetwork.playerList) {
        var team = (int)player.CustomProperties["Team"];

        if (team == _receiver.PlayerInfo.Team) {
          var viewID = (int)player.CustomProperties["ViewID"];
          var teammate = PhotonView.Find(viewID).gameObject.GetComponent<BattlePlayer>();

          if (teammate != _receiver)
            teammateList.Add(teammate);
        }
      }

      return teammateList;
    }

    private void GiveExpToReceiver(int exp) {
      _receiver.NextExp.Add(exp);
      _receiver.NextExp.UpdateView();
    }

    private void GiveGoldToReceiver(int gold) {
      _receiver.Gold.Add(gold);
      _receiver.Gold.UpdateView();
    }

    private void GiveExpToTeammate(int exp, List<BattlePlayer> teammateList) {
      foreach (var teammate in teammateList) {
        teammate.NextExp.Add(exp);
        teammate.NextExp.UpdateView();
      }
    }

    private void GiveGoldToTeammate(int gold, List<BattlePlayer> teammateList) {
      foreach (var teammate in teammateList) {
        teammate.Gold.Add(gold);
        teammate.Gold.UpdateView();
      }
    }

    private BattlePlayer _receiver;
  }
}

