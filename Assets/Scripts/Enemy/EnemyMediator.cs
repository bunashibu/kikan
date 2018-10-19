using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyMediator : IListener {
    public EnemyMediator(Enemy enemy) {
      _enemy = enemy;
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        default:
          break;
      }
    }

    private Enemy _enemy;
  }
}

