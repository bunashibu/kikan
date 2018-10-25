using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SkillInstantiator : Photon.MonoBehaviour {
    void Start() {
      _canUseList = new List<bool>(){ true, true, true, true, true, true };
      _isDisabled = new List<bool>(){ false, true, true, true, true, true };
    }

    void Update() {
      if (!photonView.isMine || _player.Hp.Cur <= 0 || _player.State.Rigor || !IsCorrectAnimationState() || _player.BuffState.Stun)
        return;

      for (int i=0; i<_keysList.Count; ++i) {
        if (_player.Level.Lv < _requireLv[i])
          continue;

        if (_isDisabled[i] && (_player.Level.Lv == _requireLv[i]))
          EnableSkill(i);

        for (int k=0; k<_keysList[i].keys.Count; ++k) {
          if (_canUseList[i] && Input.GetKey(_keysList[i].keys[k])) {
            InstantiateSkill(i);
            StartCT(i);
            UpdateCT(i);
            break;
          }
        }
      }
    }

    public void AttachSkillPanel(SkillPanel panel) {
      _panelUnitList = panel.UnitList;
    }

    public void ResetAllCT() {
      for (int i=0; i<_keysList.Count; ++i) {
        _canUseList[i] = true;
        _player.SkillInfo.SetState(_names[i], SkillState.Ready);
        _player.State.Rigor = false;

        if (!_isDisabled[i]) {
          var preSizeDelta = _panelUnitList[i].AlphaRectTransform.sizeDelta;
          _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, 0);
        }
      }
    }

    private bool IsCorrectAnimationState() {
      return !(_player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Die") ||
               _player.Animator.GetCurrentAnimatorStateInfo(0).IsName("Ladder"));
    }

    private void InstantiateSkill(int i) {
      string path = "Prefabs/Skill/" + _jobName + "/" + _names[i];

      var offset = _appearOffset[i];
      if (_renderer.flipX)
        offset.x *= -1;
      var pos = this.transform.position + offset;

      var skill = PhotonNetwork.Instantiate(path, pos, Quaternion.identity, 0).GetComponent<Skill>();
      skill.SyncInit(_renderer.flipX, _player.PhotonView.viewID);

      SkillReference.Instance.Register(skill);
    }

    private void StartCT(int i) {
      _canUseList[i] = false;
      MonoUtility.Instance.StoppableDelaySec(_skillCT[i], "SkillCanUse" + i.ToString(), () => {
        _canUseList[i] = true;
      });

      // Ignore X Skill CT
      if (i != 0)
        _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(55.0f, 55.0f);

      _player.SkillInfo.SetState(_names[i], SkillState.Using);
      MonoUtility.Instance.StoppableDelaySec(_skillCT[i], "SkillInfoState" + i.ToString(), () => {
        _player.SkillInfo.SetState(_names[i], SkillState.Ready);
      });

      _player.State.Rigor = true;
      MonoUtility.Instance.StoppableDelaySec(_rigorCT[i], "PlayerStateRigor" + i.ToString(), () => {
        _player.State.Rigor = false;
        _player.SkillInfo.SetState(_names[i], SkillState.Used);
      });
    }

    private void UpdateCT(int i) {
      // temp
      if (i == 0)
        return;

      MonoUtility.Instance.StoppableDelaySec(_skillCT[i] / 17.0f, "SkillPanelAlpha" + i.ToString(), () => {
        // Memo: AlphaMask height == 55.0
        //       SkillPanel-Update-Count == 17.0
        var preSizeDelta = _panelUnitList[i].AlphaRectTransform.sizeDelta;
        _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, preSizeDelta.y - (55.0f / 17.0f));

        if (!_canUseList[i])
          UpdateCT(i);
      });
    }

    private void EnableSkill(int index) {
      var preSizeDelta = _panelUnitList[index].AlphaRectTransform.sizeDelta;
      _panelUnitList[index].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, 0);
      _isDisabled[index] = false;
    }

    [SerializeField] private string _jobName;
    [SerializeField] private List<KeyList> _keysList;
    [SerializeField] private SkillName[] _names;
    [SerializeField] private float[] _skillCT;
    [SerializeField] private float[] _rigorCT;
    [SerializeField] private Vector3[] _appearOffset;
    [SerializeField] private int[] _requireLv;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private BattlePlayer _player;
    private List<SkillPanelUnit> _panelUnitList;
    private List<bool> _canUseList;
    private List<bool> _isDisabled;
  }

  [System.SerializableAttribute]
  public class KeyList {
    public List<KeyCode> keys;
  }
}

