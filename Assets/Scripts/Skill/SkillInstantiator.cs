using UnityEngine;
using System;
using System.Collections;

namespace Bunashibu.Kikan {
  public class SkillInstantiator : Photon.MonoBehaviour {
    void Update() {
      if (!photonView.isMine || _player.Hp.Cur <= 0)
        return;

      for (int i=0; i<_keys.Length; ++i) {
        if (_canUse && Input.GetKey(_keys[i])) {
          InstantiateSkill(i);
          StartCT(i);
          break;
        }

        if (!_canUse)
          UpdateCT(i);
      }
    }

    private void InstantiateSkill(int i) {
      string path = "Prefabs/Skill/" + _jobName + "/" + _names[i];

      var offset = _appearOffset[i];
      if (_renderer.flipX)
        offset.x *= -1;
      var pos = this.transform.position + offset;

      var skill = PhotonNetwork.Instantiate(path, pos, Quaternion.identity, 0).GetComponent<Skill>();
      skill.Init(_renderer.flipX, _player.PhotonView.viewID);
    }

    private void StartCT(int i) {
      _canUse = false;
      MonoUtility.Instance.DelaySec(_skillCT[i], () => {
        _canUse = true;
      });

      _player.SkillInfo.SetState(_names[i], SkillState.Using);
      MonoUtility.Instance.DelaySec(_skillCT[i], () => {
        _player.SkillInfo.SetState(_names[i], SkillState.Ready);
      });

      _player.State.Rigor = true;
      MonoUtility.Instance.DelaySec(_rigorCT[i], () => {
        _player.State.Rigor = false;
        _player.SkillInfo.SetState(_names[i], SkillState.Used);
      });
    }

    private void UpdateCT(int i) {
      MonoUtility.Instance.DelaySec(_skillCT[i] / (float)17.0, () => {
          /*
        var rectTransform = skillPanel.AlphaList[0].GetComponent<RectTransform>();

        // AlphaMask height == 55.0
        // SkillPanel Update Count == 17.0
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - (55.0 / 17.0));
        */
      });
    }

    [SerializeField] private string _jobName;
    [SerializeField] private KeyCode[] _keys;
    [SerializeField] private SkillName[] _names;
    [SerializeField] private float[] _skillCT;
    [SerializeField] private float[] _rigorCT;
    [SerializeField] private Vector3[] _appearOffset;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private BattlePlayer _player;
    private bool _canUse = true;
  }
}

