using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class FinalStage : MonoBehaviour {
    void Awake() {
      _destRotation = Quaternion.Euler(0, 0, 0);
    }

    void Update() {
      if (_isRotating) {
        float step = _rotateSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, _destRotation, step);

        if (transform.rotation == _destRotation) {
          _isRotating = false;
          _timePanel.SetTime(_time);
          ResetPlayerStatus();
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

    private void ResetPlayerStatus() {
      int viewID = (int)PhotonNetwork.player.CustomProperties["ViewID"];
      var player = PhotonView.Find(viewID).GetComponent<BattlePlayer>() as BattlePlayer;

      player.Hp.FullRecover();
      player.Hp.UpdateView();

      player.Weapon.SkillInstantiator.ResetAllCT();
    }

    [SerializeField] private TimePanel _timePanel;

    [Header("Final Battle Time (Second)")]
    [SerializeField] private float _time;

    [Header("Rotation per second")]
    [SerializeField] private float _rotateSpeed;

    private Quaternion _destRotation;
    private bool _isRotating;
  }
}

