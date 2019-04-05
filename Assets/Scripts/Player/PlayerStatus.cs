using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerStatus {
    public PlayerStatus(JobStatus jobStatus) {
      Atk = jobStatus.Atk;
      Dfn = jobStatus.Dfn;
      Spd = jobStatus.Spd;
      Jmp = jobStatus.Jmp;
      FixAtk = 1.0f;
      FixCritical = 0;
    }

    public void IncreaseAtk(int level) {
      if (level <= 12)
        Atk += 16;
      else
        Atk += 32;
    }

    public void SetFixAtk(float ratio) {
      FixAtk = ratio;
    }

    public void SetFixCritical(int quantity) {
      FixCritical = quantity;
    }

    public int Atk { get; private set; }
    public int Dfn { get; private set; }
    public int Spd { get; private set; }
    public int Jmp { get; private set; }
    public float FixAtk      { get; private set; }
    public int   FixCritical { get; private set; } // Unit(%)
  }
}

