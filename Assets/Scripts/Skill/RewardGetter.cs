using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardGetter : MonoBehaviour {
  public void SetRewardReceiver(GameObject receiver, int team) {
    _receiver = receiver;
    _receiveTeam = team;
  }

  public void GetRewardFrom(GameObject target) {
    var teammateList = GetTeammateList();
    int teamSize = teammateList.Count;

    double ratio = 1;
    if (teamSize == 1)
      ratio = 0.7;
    if (teamSize == 2)
      ratio = 0.6;

    var killExp = target.GetComponent<KillReward>().Exp;
    var killGold = target.GetComponent<KillReward>().Gold;

    int receiverExp = (int)(killExp * ratio);
    int receiverGold = (int)(killGold * ratio);

    GiveExpToReceiver(receiverExp);
    GiveGoldToReceiver(receiverGold);

    if (teamSize > 0) {
      int teammateExp = (int)((killExp - receiverExp) / teamSize);
      int teammateGold = (int)((killGold - receiverGold) / teamSize);

      GiveExpToTeammate(teammateExp, teammateList);
      GiveGoldToTeammate(teammateGold, teammateList);
    }
  }

  private List<GameObject> GetTeammateList() {
    var teammateList = new List<GameObject>();

    foreach (var player in PhotonNetwork.playerList) {
      var team = (int)player.CustomProperties["Team"];

      if (team == _receiveTeam) {
        var viewID = (int)player.CustomProperties["ViewID"];
        var teammate = PhotonView.Find(viewID).gameObject;

        if (teammate != _receiver)
          teammateList.Add(teammate);
      }
    }

    return teammateList;
  }

  private void GiveExpToReceiver(int exp) {
    var nextExp = _receiver.GetComponent<PlayerNextExp>();

    nextExp.Plus(exp);
    nextExp.UpdateView();
  }

  private void GiveGoldToReceiver(int gold) {
    var playerGold = _receiver.GetComponent<PlayerGold>();

    playerGold.Plus(gold);
    playerGold.UpdateView();
  }

  private void GiveExpToTeammate(int exp, List<GameObject> teammateList) {
    foreach (var teammate in teammateList) {
      var nextExp = teammate.GetComponent<PlayerNextExp>();

      nextExp.Plus(exp);
      nextExp.UpdateView();
    }
  }

  private void GiveGoldToTeammate(int gold, List<GameObject> teammateList) {
    foreach (var teammate in teammateList) {
      var playerGold = teammate.GetComponent<PlayerGold>();

      playerGold.Plus(gold);
      playerGold.UpdateView();
    }
  }

  private GameObject _receiver;
  private int _receiveTeam;
}

