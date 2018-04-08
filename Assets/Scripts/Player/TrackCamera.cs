using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TrackCamera : MonoBehaviour {
    void Update() {
      if (!_isTracking)
        return;

      Vector3 trackPosition = _trackObj.transform.position + _positionOffset;

      if (transform.position != trackPosition) {
        InterpolateTo(trackPosition);
        RestrictEdgeBehaviour();
      }
    }

    public void SetPosition(Vector3 position) {
      transform.position = position + _positionOffset;
    }

    public void SetTrackTarget(GameObject trackObj) {
      _trackObj = trackObj;
      _isTracking = true;
    }

    private void InterpolateTo(Vector3 destination) {
      Vector3 interpolatedPosition = transform.position * _interpolateRatio + destination * (1.0f - _interpolateRatio);
      transform.position = interpolatedPosition;
    }

    private void RestrictEdgeBehaviour() {
      float distance = Mathf.Abs(transform.position.z);
      Vector3 cameraViewBottomLeft  = _camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
      Vector3 cameraViewTopRight    = _camera.ViewportToWorldPoint(new Vector3(1, 1, distance));
      Vector3 cameraViewTopLeft     = new Vector3(cameraViewBottomLeft.x, cameraViewTopRight.y, cameraViewTopRight.z);
      Vector3 cameraViewBottomRight = new Vector3(cameraViewTopRight.x, cameraViewBottomLeft.y, cameraViewTopRight.z);
      float cameraViewWidth  = cameraViewTopRight.x - cameraViewTopLeft.x;
      float cameraViewHeight = cameraViewTopLeft.y - cameraViewBottomLeft.y;

      float nextX = Mathf.Clamp(transform.position.x, _stageData.StageRect.xMin + cameraViewWidth/2, _stageData.StageRect.xMax - cameraViewWidth/2);
      float nextY = Mathf.Clamp(transform.position.y, _stageData.StageRect.yMin + cameraViewHeight/2 - 2, _stageData.StageRect.yMax - cameraViewHeight/2); // -2 is bottom base sprite height.

      transform.position = new Vector3(nextX, nextY, transform.position.z);
    }

    [SerializeField] private Camera _camera;
    [SerializeField] private StageData _stageData;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _interpolateRatio;
    private bool _isTracking;
    private GameObject _trackObj;
  }
}

