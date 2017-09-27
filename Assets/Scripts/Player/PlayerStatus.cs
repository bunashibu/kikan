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
    }
  
    public void Init(JobStatus jobStatus) {
      Atk = jobStatus.Atk;
      Dfn = jobStatus.Dfn;
      Spd = jobStatus.Spd;
      Jmp = jobStatus.Jmp;
  
      photonView.RPC("SyncPlayerStatusInit", PhotonTargets.Others, Atk, Dfn, Spd, Jmp);
    }
  
    public int Atk { get; private set; }
    public int Dfn { get; private set; }
    public int Spd { get; private set; }
    public int Jmp { get; private set; }
  }
}

