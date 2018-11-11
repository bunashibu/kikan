using UnityEngine;
using System.Collections;

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

      //photonView.RPC("SyncPlayerStatus", PhotonTargets.Others, Atk, Dfn, Spd, Jmp, MulCorrectionAtk);
    }

    public void IncreaseAtk(int level) {
      if (level <= 12)
        Atk += 16;
      else
        Atk += 32;

      //photonView.RPC("SyncPlayerAtk", PhotonTargets.Others, Atk);
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

    /*
    [PunRPC]
    private void SyncPlayerStatus(int atk, int dfn, int spd, int jmp, float mulCorrectionAtk) {
      Atk = atk;
      Dfn = dfn;
      Spd = spd;
      Jmp = jmp;
      MulCorrectionAtk = mulCorrectionAtk;
    }

    [PunRPC]
    private void SyncPlayerAtk(int atk) {
      Atk = atk;
    }

    [PunRPC]
    private void SyncPlayerMulAtk(float mulCorrectionAtk) {
      MulCorrectionAtk = mulCorrectionAtk;
    }
    */
  }
}

