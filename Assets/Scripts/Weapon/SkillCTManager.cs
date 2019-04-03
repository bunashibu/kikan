using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class SkillCTManager {
    public SkillCTManager(Weapon weapon, Player player) {
      InitializeCT(weapon);

      weapon.Stream.OnInstantiated
        .Subscribe(i => CurCT[i].SetFullTime() );

      weapon.UpdateAsObservable()
        .Where(_ => player.PhotonView.isMine )
        .Subscribe(_ => UpdateCT() );
    }

    private void InitializeCT(Weapon weapon) {
      CurCT = new List<CurSkillCT>();

      for (var i=0; i < weapon.SkillCT.Length; ++i)
        CurCT.Add(new CurSkillCT(weapon.SkillCT[i]));
    }

    private void UpdateCT() {
      for (var i=0; i < CurCT.Count; ++i)
        CurCT[i].Subtract(Time.deltaTime);
    }

    public List<CurSkillCT> CurCT { get; private set; }

      /*
      MonoUtility.Instance.StoppableDelaySec(_skillCT[i] / 5.0f, "SkillPanelAlpha" + i.ToString(), () => {
        // Memo: AlphaMask height == 55.0
        var preSizeDelta = _panelUnitList[i].AlphaRectTransform.sizeDelta;
        _panelUnitList[i].AlphaRectTransform.sizeDelta = new Vector2(preSizeDelta.x, preSizeDelta.y - (55.0f / 5.0f));

        if (!_canUseList[i])
          UpdateCT(i);
      });
      */
    /*
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
    */

  }
}

