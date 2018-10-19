using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyMediator : IResponder {
    public EnemyMediator(Enemy enemy) {
      Notifier = new Notifier();
      _enemy = enemy;
    }

    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        default:
          break;
      }
    }

    public Notifier Notifier { get; private set; }

    private Enemy _enemy;
  }
}

