using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDeathRecoder : Photon.MonoBehaviour {
  void Awake() {
    KillCnt = 0;
    DeathCnt = 0;
  }

  public void Kill(GameObject target) {
    KillCnt += 1;
  }

  public void Death() {
    DeathCnt += 1;
  }

  public int KillCnt { get; private set; }
  public int DeathCnt { get; private set; }
}

