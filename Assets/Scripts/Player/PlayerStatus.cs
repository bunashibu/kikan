using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PlayerStatus {
    public PlayerStatus(JobStatus jobStatus) {
      Init(jobStatus);
    }

    public void Init(JobStatus jobStatus) {
      Atk = jobStatus.Atk;
      Dfn = jobStatus.Dfn;
      Spd = jobStatus.Spd;
      Jmp = jobStatus.Jmp;
      MulCorrectionAtk = 1.0f;
    }

    public void IncreaseAtk(int level) {
      if (level <= 12)
        Atk += 16;
      else
        Atk += 32;
    }

    // Manji Space use this
    public void MultipleMulCorrectionAtk(float ratio) {
      MulCorrectionAtk *= ratio;
      //photonView.RPC("SyncPlayerMulAtk", PhotonTargets.Others, MulCorrectionAtk);
    }

    public void ResetMulCorrectionAtk() {
      MulCorrectionAtk = 1.0f;
      //photonView.RPC("SyncPlayerMulAtk", PhotonTargets.Others, MulCorrectionAtk);
    }

    public int Atk { get; private set; }
    public int Dfn { get; private set; }
    public int Spd { get; private set; }
    public int Jmp { get; private set; }
    public float MulCorrectionAtk { get; private set; }
  }
}

