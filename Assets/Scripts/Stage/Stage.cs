using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Stage : MonoBehaviour {
    void Awake() {
      _timePanel.SetTime(_time);
      _destRotation = Quaternion.Euler(90, 0, 0);
    }

    void Update() {
      if (_isRotating) {
        float step = _rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _destRotation, step);

        if (transform.rotation == _destRotation) {
          _isRotating = false;
          Hide();
        }
      }
    }

    public void Emerge() {
      gameObject.SetActive(true);
    }

    public void Hide() {
      gameObject.SetActive(false);
    }

    public void StartRotation() {
      _isRotating = true;
    }

    [SerializeField] private TimePanel _timePanel;

    [Header("Battle Time (Second)")]
    [SerializeField] private float _time;

    [Header("Rotation per second")]
    [SerializeField] private float _rotateSpeed;

    private Quaternion _destRotation;
    private bool _isRotating;
  }
}

