using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class FixedCamera : Photon.MonoBehaviour {
    void Start() {
      float x = 31.3f;
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        x *= -1;

      transform.position = new Vector3(x, 0.0f, -1.0f);
    }
  }
}

