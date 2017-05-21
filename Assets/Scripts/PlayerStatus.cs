using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
  public void Init(JobStatus jobStatus) {
    Atk = jobStatus.Atk;
    Dfn = jobStatus.Dfn;
    Spd = jobStatus.Spd;
    Jmp = jobStatus.Jmp;
  }

  public int Atk { get; private set; }
  public int Dfn { get; private set; }
  public int Spd { get; private set; }
  public int Jmp { get; private set; }
}

