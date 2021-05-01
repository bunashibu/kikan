using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Bunashibu.Kikan {
  public static class Client {
    public static Player Player;
    public static List<Player> Teammates = new List<Player>();
    public static List<Player> Opponents = new List<Player>();

    public static List<Player> GetTeammates(Player target) {
      if (target == Player)
        return Teammates;
      if (Teammates.Contains(target)) {
        var list = Teammates.Where(teammate => target != teammate).ToList();
        list.Add(Player);
        return list;
      }
      if (Opponents.Contains(target)) {
        var list = Opponents.Where(opponent => target != opponent).ToList();
        return list;
      }

      throw new System.Exception();
    }

    public static int TeammateCount(Player player) {
      if (player == Player)
        return Teammates.Count;
      if (Teammates.Contains(player))
        return Teammates.Count;
      if (Opponents.Contains(player))
        return Opponents.Count - 1;

      throw new System.Exception();
    }
  }
}
