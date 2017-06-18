using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : Photon.MonoBehaviour {
  void Awake() {
    Movement = new LobbyPlayerMovement();
  }

  public LobbyPlayerMovement Movement { get; private set; }
}

