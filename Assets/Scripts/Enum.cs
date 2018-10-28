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

  // Obsolete
  public enum DamageType {
    Hit,
    Critical,
    Take,
    Heal
  }

  // Obsolete
  public enum PopupType {
    Player,
    Enemy
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

    TakeDamage,

    Killed,
    GetKillReward,
    ExpIsMax,
    LevelUp
  }
}

