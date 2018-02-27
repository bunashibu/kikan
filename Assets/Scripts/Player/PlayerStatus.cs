using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class PlayerStatus : Photon.MonoBehaviour {
    [PunRPC]
    private void SyncPlayerStatusInit(int atk, int dfn, int spd, int jmp) {
      Atk = atk;
      Dfn = dfn;
      Spd = spd;
      Jmp = jmp;
      MulCorrectionAtk = 1.0f;
    }

    public void Init(JobStatus jobStatus) {
      Atk = jobStatus.Atk;
      Dfn = jobStatus.Dfn;
      Spd = jobStatus.Spd;
      Jmp = jobStatus.Jmp;
      MulCorrectionAtk = 1.0f;

      photonView.RPC("SyncPlayerStatusInit", PhotonTargets.Others, Atk, Dfn, Spd, Jmp);
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
    }

    public void ResetMulCorrectionAtk() {
      MulCorrectionAtk = 1.0f;
    }

    public int Atk { get; private set; }
    public int Dfn { get; private set; }
    public int Spd { get; private set; }
    public int Jmp { get; private set; }
    public float MulCorrectionAtk { get; private set; }
  }
}

