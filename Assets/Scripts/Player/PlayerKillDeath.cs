using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class PlayerKillDeath : Duplexer {
    public PlayerKillDeath() {
      KillCount  = 0;
      DeathCount = 0;
    }

    public override void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.GetKillReward:
          Assert.IsTrue(args.Length == 2);

          break;
        default:
          break;;
      }
    }

    public int KillCount  { get; private set; }
    public int DeathCount { get; private set; }
  }
}

