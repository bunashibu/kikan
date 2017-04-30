using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {
  public void SetStatus(PlayerStatus status) {
    _status = status;
  }

  protected PlayerStatus _status;
}

