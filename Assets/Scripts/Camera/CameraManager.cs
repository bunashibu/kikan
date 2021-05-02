using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CameraManager : SingletonMonoBehaviour<CameraManager> {
    protected override void Awake() {
      SetCameraPosition();
    }

    public void SetTrackTarget(GameObject obj) {
      _trackCamera.SetTrackTarget(obj);
    }

    private void SetCameraPosition() {
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        _initialCameraPosition.x *= -1;

      _trackCamera.SetPosition(_initialCameraPosition);
    }

    [SerializeField] private TrackCamera _trackCamera;
    [SerializeField] private Vector3 _initialCameraPosition;
  }
}
