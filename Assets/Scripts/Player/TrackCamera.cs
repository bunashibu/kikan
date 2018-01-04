using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class TrackCamera : MonoBehaviour {
    void Start() {
      transform.position = transform.position + _positionOffset;
    }

    void Update() {
      Vector3 trackPosition = _trackObj.transform.position + _positionOffset;

      if (transform.position != trackPosition)
        InterpolateTo(trackPosition);
    }

    public void Init(GameObject trackObj) {
      _trackObj = trackObj;
    }

    private void InterpolateTo(Vector3 destination) {
      Vector3 nextPosition = transform.position * 0.92f + destination * 0.08f;
      transform.position = nextPosition;
    }

    [SerializeField] private Vector3 _positionOffset;
    private GameObject _trackObj;
  }
}

