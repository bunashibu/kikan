using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Weapon : MonoBehaviour {
    void Awake() {
      _instantiator = new SkillInstantiator(this, _player);
      _ctManager = new SkillCTManager(this, _player);

      Stream = new WeaponStream();
    }

    public bool IsUsable(int i) {
      return _ctManager.CurCT[i].Cur.Value == 0;
    }

    public string        JobName      => _jobName;
    public List<KeyList> KeysList     => _keysList;
    public SkillName[]   SkillNames   => _skillNames;
    public int[]         RequireLv    => _requireLv;
    public Vector3[]     AppearOffset => _appearOffset;
    public float[]       SkillCT      => _skillCT;

    public WeaponStream Stream { get; private set; }

    [SerializeField] private Player _player;
    [SerializeField] private string _jobName;
    [SerializeField] private List<KeyList> _keysList;
    [SerializeField] private SkillName[] _skillNames;
    [SerializeField] private int[] _requireLv;
    [SerializeField] private Vector3[] _appearOffset;
    [SerializeField] private float[] _skillCT;
    [SerializeField] private float[] _rigorCT;

    private SkillInstantiator _instantiator;
    private SkillCTManager _ctManager;

    void Start() {
      _canUseList = new List<bool>(){ true, true, true, true, true, true };
      _isDisabled = new List<bool>(){ false, true, true, true, true, true };
    }

    public void EnableSkill() {
      this.enabled = true;
    }

    public void DisableSkill() {
      this.enabled = false;
    }

    void Update() {
      /*
        if (_isDisabled[i] && (_player.Level.Cur.Value == _requireLv[i]))
          EnableSkill(i);
      */
    }

    public void AttachSkillPanel(SkillPanel panel) {
      _panelUnitList = panel.UnitList;
    }

    public void ResetAllCT() {
      for (int i=0; i<_keysList.Count; ++i) {
        _canUseList[i] = true;
        _player.SkillInfo.SetState(_skillNames[i], SkillState.Ready);
        _player.State.Rigor = false;

        if (!_isDisabled[i]) {
          var preSizeDelta = _panelUnitList[i].AlphaRectTransform.sizeDelta;
          _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, 0);
        }
      }
    }

    private void StartCT(int i) {
      _canUseList[i] = false;
      MonoUtility.Instance.StoppableDelaySec(_skillCT[i], "SkillCanUse" + i.ToString(), () => {
        _canUseList[i] = true;
      });

      // Ignore X Skill CT
      if (i != 0)
        _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(55.0f, 55.0f);

      _player.SkillInfo.SetState(_skillNames[i], SkillState.Using);
      MonoUtility.Instance.StoppableDelaySec(_skillCT[i], "SkillInfoState" + i.ToString(), () => {
        _player.SkillInfo.SetState(_skillNames[i], SkillState.Ready);
      });

      _player.State.Rigor = true;
      MonoUtility.Instance.StoppableDelaySec(_rigorCT[i], "PlayerStateRigor" + i.ToString(), () => {
        _player.State.Rigor = false;
        _player.SkillInfo.SetState(_skillNames[i], SkillState.Used);
      });
    }

    private void UpdateCT(int i) {
      if (i == 0)
        return;

      MonoUtility.Instance.StoppableDelaySec(_skillCT[i] / 5.0f, "SkillPanelAlpha" + i.ToString(), () => {
        // Memo: AlphaMask height == 55.0
        var preSizeDelta = _panelUnitList[i].AlphaRectTransform.sizeDelta;
        _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, preSizeDelta.y - (55.0f / 5.0f));

        if (!_canUseList[i])
          UpdateCT(i);
      });
    }

    private void EnableSkill(int index) {
      var preSizeDelta = _panelUnitList[index].AlphaRectTransform.sizeDelta;
      _panelUnitList[index].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, 0);
      _isDisabled[index] = false;
    }

    [SerializeField] private SpriteRenderer _renderer;
    private List<SkillPanelUnit> _panelUnitList;
    private List<bool> _canUseList;
    private List<bool> _isDisabled;
  }

  [System.SerializableAttribute]
  public class KeyList {
    public List<KeyCode> keys;
  }
}

