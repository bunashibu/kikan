using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class BattleStage : MonoBehaviour {
    void Awake() {
      _timePanel.SetTime(_time);
      _destRotation = Quaternion.Euler(90, 0, 0);

      this.UpdateAsObservable()
        .Where(_ => _isRotating)
        .Subscribe(_ => Rotate() );

      this.UpdateAsObservable()
        .Where(_ => _isRotating)
        .Where(_ => transform.rotation == _destRotation)
        .Subscribe(_ => {
          _isRotating = false;
          Hide();
        });
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

    private void Rotate() {
      float step = _rotateSpeed * Time.deltaTime;
      transform.rotation = Quaternion.RotateTowards(transform.rotation, _destRotation, step);
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

