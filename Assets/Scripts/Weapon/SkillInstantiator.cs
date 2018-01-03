using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SkillInstantiator : Photon.MonoBehaviour {
    void Start() {
      _canUseList = new List<bool>(){ true, true, true, true, true, true };
    }

    void Update() {
      if (!photonView.isMine || _player.Hp.Cur <= 0 || _player.State.Rigor)
        return;

      for (int i=0; i<_keys.Length; ++i) {
        if (_canUseList[i] && Input.GetKey(_keys[i])) {
          InstantiateSkill(i);
          StartCT(i);
          UpdateCT(i);
          break;
        }
      }
    }

    public void AttachSkillPanel(SkillPanel panel) {
      _panelUnitList = panel.UnitList;
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
      _canUseList[i] = false;
      MonoUtility.Instance.DelaySec(_skillCT[i], () => {
        _canUseList[i] = true;
      });

      // Ignore X Skill CT
      if (i != 0)
        _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(55.0f, 55.0f);

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
      MonoUtility.Instance.DelaySec(_skillCT[i] / 17.0f, () => {
        // Memo: AlphaMask height == 55.0
        //       SkillPanel-Update-Count == 17.0
        var preSizeDelta = _panelUnitList[i].AlphaRectTransform.sizeDelta;
        _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, preSizeDelta.y - (55.0f / 17.0f));

        if (!_canUseList[i])
          UpdateCT(i);
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
    private List<SkillPanelUnit> _panelUnitList;
    private List<bool> _canUseList;
  }
}

