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
    Alt,

    X2 // Nage
  }

  public enum SkillState {
    Ready,
    Using,
    Used
  }

  public enum HitEffectType {
    None,
    Heal,
    Manji,
    Magician,
    Nage
  }

  public enum NumberPopupType {
    Hit,
    Critical,
    Take,
    Heal
  }

  public enum DebuffType {
    Stun,
    Heavy
  }

  public enum CoreType {
    Speed,
    Hp,
    Attack,
    Critical,
    Heal
  }

  public enum FixSpdType {
    Replace,
    Ratio
  }
}

