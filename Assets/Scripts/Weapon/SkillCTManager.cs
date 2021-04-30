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
        .Where(_ => player.PhotonView.isMine )
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

        var flow = new CurCTFlow(i, _curCT[i]);
        weapon.Stream.OnNextCurCT(flow);
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
  }
}
