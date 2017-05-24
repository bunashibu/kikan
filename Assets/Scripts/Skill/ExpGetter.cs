using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGetter : MonoBehaviour {
  public void SetExpReceiver(GameObject receiver, int team) {
    _receiver = receiver;
    _receiveTeam = team;
  }

  public void GetExpFrom(GameObject target) {
    var teammateList = GetTeammateList();
    var killExp = target.GetComponent<KillExp>().Exp;

    int size = teammateList.Count;
    double ratio = 1;

    if (size == 1)
      ratio = 0.7;
    if (size == 2)
      ratio = 0.6;

    int receiverExp = (int)(killExp * ratio);
    GiveExpToReceiver(receiverExp);

    if (size > 0) {
      int teammateExp = (int)((killExp - receiverExp) / size);
      GiveExpToTeammate(teammateExp, teammateList);
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
    nextExp.Show();
  }

  private void GiveExpToTeammate(int exp, List<GameObject> teammateList) {
    foreach (var teammate in teammateList) {
      var nextExp = teammate.GetComponent<PlayerNextExp>();

      nextExp.Plus(exp);
      nextExp.Show();
    }
  }

  private GameObject _receiver;
  private int _receiveTeam;
}

