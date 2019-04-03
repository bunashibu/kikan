using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class SkillCTManager {
    public SkillCTManager(Weapon weapon, Player player) {
      _curCT = new List<float>{ 0, 0, 0, 0, 0, 0 };

      weapon.Stream.OnInstantiated
        .Subscribe(i => _curCT[i] = weapon.SkillCT[i] )
        .AddTo(weapon.gameObject);

      weapon.UpdateAsObservable()
        .Where(_ => player.PhotonView.isMine )
        .Subscribe(_ => UpdateCT(weapon) );
    }

    private void UpdateCT(Weapon weapon) {
      for (var i=0; i < _curCT.Count; ++i) {
        _curCT[i] -= Time.deltaTime;

        if (_curCT[i] < 0)
          _curCT[i] = 0;

        var flowEntity = new CurCTFlowEntity(i, _curCT[i]);
        weapon.Stream.OnNextCurCT(flowEntity);
      }
    }

    public bool IsUsable(int i) {
      return (_curCT[i] == 0) ? true : false;
    }

    public void ResetAllCT() {
      for (var i=0; i < _curCT.Count; ++i)
        _curCT[i] = 0;
    }

    private List<float> _curCT;

    /*
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

