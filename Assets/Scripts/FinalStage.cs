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

        GatherPlayer();

        if (transform.rotation == _destRotation) {
          _isRotating = false;
          _timePanel.SetTime(_time);
          ResetPlayerStatus();
        }
      }
    }

    public void GatherPlayer() {
      _player.Rigid.simulated = false;
      _player.Character.enabled = false;

      _player.Transform.position = _easing.GetNextPosition();
    }

    public void Emerge() {
      gameObject.SetActive(true);

      int viewID = (int)PhotonNetwork.player.CustomProperties["ViewID"];
      _player = PhotonView.Find(viewID).GetComponent<BattlePlayer>() as BattlePlayer;

      Vector3 startPosition = _player.Transform.position;
      Vector3 gatherPosition = StageManager.Instance.StageData.RespawnPosition;
      if (_player.PlayerInfo.Team == 1)
        gatherPosition.x *= -1;

      _easing = new QuadraticEaseInOut(startPosition, gatherPosition, 3.0f);
    }

    public void Hide() {
      gameObject.SetActive(false);
    }

    public void StartRotation() {
      _isRotating = true;
    }

    private void ResetPlayerStatus() {
      _player.Rigid.simulated = true;
      _player.Character.enabled = true;

      _player.Hp.FullRecover();
      _player.Hp.UpdateView();

      _player.Weapon.SkillInstantiator.ResetAllCT();
    }

    [SerializeField] private TimePanel _timePanel;

    [Header("Final Battle Time (Second)")]
    [SerializeField] private float _time;

    [Header("Rotation per second")]
    [SerializeField] private float _rotateSpeed;

    private Quaternion _destRotation;
    private bool _isRotating;
    private BattlePlayer _player;
    private QuadraticEaseInOut _easing;
  }
}

