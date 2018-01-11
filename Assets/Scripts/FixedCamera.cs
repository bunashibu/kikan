using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public class FixedCamera : Photon.MonoBehaviour {
    void Start() {
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        _x *= -1;

      transform.position = new Vector3(_x, _y, _z);
    }

    [SerializeField] private float _x;
    [SerializeField] private float _y;
    [SerializeField] private float _z;
  }
}

