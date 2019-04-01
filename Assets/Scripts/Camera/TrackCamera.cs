using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TrackCamera : MonoBehaviour {
    void Awake() {
      _shouldRestrict = true;
    }

    void Update() {
      if (!_isTracking)
        return;

      Vector3 trackPosition = _trackObj.transform.position + _positionOffset;

      if (transform.position != trackPosition) {
        InterpolateTo(trackPosition);

        if (_shouldRestrict)
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

    public void EnableTracking() {
      _isTracking = true;
    }

    public void DisableTracking() {
      _isTracking = false;
    }

    public void EnableRestrict() {
      _shouldRestrict = true;
    }

    public void DisableRestrict() {
      _shouldRestrict = false;
    }

    public float OffsetZ { get { return _positionOffset.z; } }

    private void InterpolateTo(Vector3 destination) {
      var distance = Vector3.Distance(transform.position, destination);

      if (distance > _slowDistance)
        transform.position = Interpolate(destination, _ratio);
      else
        transform.position = Interpolate(destination, _ratio + (_slowDistance - distance) * 0.01f);
    }

    private Vector3 Interpolate(Vector3 destination, float ratio) {
      return transform.position * ratio + destination * (1.0f - ratio);
    }

    private void RestrictEdgeBehaviour() {
      float distance = Mathf.Abs(transform.position.z);
      Vector3 cameraViewBottomLeft  = _camera.ViewportToWorldPoint(new Vector3(0, 0, distance));
      Vector3 cameraViewTopRight    = _camera.ViewportToWorldPoint(new Vector3(1, 1, distance));
      Vector3 cameraViewTopLeft     = new Vector3(cameraViewBottomLeft.x, cameraViewTopRight.y, cameraViewTopRight.z);
      Vector3 cameraViewBottomRight = new Vector3(cameraViewTopRight.x, cameraViewBottomLeft.y, cameraViewTopRight.z);
      float cameraViewWidth  = cameraViewTopRight.x - cameraViewTopLeft.x;
      float cameraViewHeight = cameraViewTopLeft.y - cameraViewBottomLeft.y;

      float nextX = Mathf.Clamp(transform.position.x, StageReference.Instance.StageData.StageRect.xMin + cameraViewWidth/2, StageReference.Instance.StageData.StageRect.xMax - cameraViewWidth/2);
      float nextY = Mathf.Clamp(transform.position.y, StageReference.Instance.StageData.StageRect.yMin + cameraViewHeight/2 - 2, StageReference.Instance.StageData.StageRect.yMax - cameraViewHeight/2); // -2 is bottom base sprite height.

      transform.position = new Vector3(nextX, nextY, transform.position.z);
    }

    [SerializeField] private Camera _camera;
    [SerializeField] private Vector3 _positionOffset;
    [SerializeField] private float _ratio;
    private float _slowDistance = 7.0f;
    private bool _isTracking;
    private bool _shouldRestrict;
    private GameObject _trackObj;
  }
}

