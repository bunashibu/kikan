using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public enum SkillName {
    X,
    Shift,
    Z,
    Ctrl,
    Space,
    Alt
  }

  public enum SkillState {
    Ready,
    Using,
    Used
  }

  public enum NumberPopupType {
    Hit,
    Critical,
    Take,
    Heal
  }

  public enum Notification {
    PlayerInstantiated,
    EnemyInstantiated,
    Initialize,

    HpUpdated,
    ExpUpdated,
    GoldUpdated,

    TakeDamage,

    Killed,
    GetKillReward,
    ExpIsMax,
    LevelUp
  }
}

