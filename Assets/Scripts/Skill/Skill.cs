using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : Photon.MonoBehaviour {
  public void SetStatus(PlayerStatus statusArg) {
    status = statusArg;
  }

  [System.NonSerialized] public PlayerStatus status;
}

