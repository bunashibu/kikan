using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TrackCamera : MonoBehaviour {
    void Start() {
      transform.position = transform.position + _positionOffset;
      _destination = new Vector3(0, 0, 0);
    }

    void Update() {
      Vector3 trackPosition = _trackObj.transform.position + _positionOffset;

      if (transform.position != trackPosition)
        InterpolateTo(trackPosition);
      else
        _elapsedTime = 0;
    }

    public void Init(GameObject trackObj) {
      _trackObj = trackObj;
    }

    private void InterpolateTo(Vector3 destination) {
      if (_destination != destination)
        _elapsedTime = 0;

      _destination = destination;
      _elapsedTime += Time.deltaTime;

      float t = _elapsedTime / 3.0f;
      if (t > 1.0f)
        t = 1.0f;

      float ratio = t * t * (3.0f - 2.0f * t);

      Vector3 nextPosition = transform.position * (1.0f - ratio) + destination * ratio;
      transform.position = nextPosition;
    }

    [SerializeField] private Vector3 _positionOffset;
    private GameObject _trackObj;
    private Vector3 _destination;
    private float _elapsedTime;
  }
}

