using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using System.Text.RegularExpressions;

namespace Bunashibu.Kikan {
  public class PlayerInfo {
    public PlayerInfo(Player player) {
      Team = (int)player.PhotonView.owner.CustomProperties["Team"];
      Name = player.PhotonView.owner.NickName.Trim();
      SetJobName(player.gameObject.name);
    }

    private void SetJobName(string name) {
      if (Regex.IsMatch(name, "Manji")) {
        Job = "卍";
        return;
      }

      if (Regex.IsMatch(name, "Magician")) {
        Job = "魔法使い";
        return;
      }

      if (Regex.IsMatch(name, "Nage")) {
        Job = "投げ賊";
        return;
      }

      if (Regex.IsMatch(name, "Panda")) {
        Job = "パンダ";
        return;
      }

      if (Regex.IsMatch(name, "Warrior")) {
        Job = "戦士";
        return;
      }
    }

    public int    Team { get; private set; }
    public string Name { get; private set; }
    public string Job  { get; private set; }
  }
}

