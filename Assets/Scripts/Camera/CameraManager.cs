using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class CameraManager : SingletonMonoBehaviour<CameraManager> {
    protected override void Awake() {
      SetCameraPosition();
    }

    public void SetTrackTarget(GameObject obj) {
      _trackCamera.SetTrackTarget(obj);
    }

    public void ActivateWatching() {
      this.UpdateAsObservable()
        .Subscribe(_ => {
          if (Input.GetKeyDown(KeyCode.LeftArrow))
            SetTrackTarget(Client.Teammates[0].gameObject);
          if (Input.GetKeyDown(KeyCode.RightArrow)) {
            if (Client.Teammates.Count == 2)
              SetTrackTarget(Client.Teammates[1].gameObject);
            else
              SetTrackTarget(Client.Teammates[0].gameObject);
          }
        });
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
