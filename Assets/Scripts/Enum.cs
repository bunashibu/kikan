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

  public enum DamageType {
    Hit,
    Critical,
    Take,
    Heal
  }

  public enum PopupType {
    Player,
    Enemy
  }

  public enum Notification {
    HpInit,
    HpAdd,
    HpSubtract,
    ExpAdd
  }
}

