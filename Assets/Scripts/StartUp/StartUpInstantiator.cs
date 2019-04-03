using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StartUpInstantiator : MonoBehaviour {
    public void InstantiatePlayer(string jobName) {
      var pos = StageReference.Instance.StageData.RespawnPosition;

      // NOTE: Team 0 is Red(Right), Team 1 is Blue(Left)
      if ((int)PhotonNetwork.player.CustomProperties["Team"] == 1)
        pos.x *= -1;

      _player = PhotonNetwork.Instantiate("Prefabs/Job/" + jobName, pos, Quaternion.identity, 0).GetComponent<Player>();
    }

    public void InstantiateHudObjects(Canvas canvas, SkillPanel skillPanel) {
      _hpBar = Instantiate(_hpBar) as Bar;
      _hpBar.transform.SetParent(canvas.transform, false);

      _expBar = Instantiate(_expBar) as Bar;
      _expBar.transform.SetParent(canvas.transform, false);

      _lvPanel = Instantiate(_lvPanel) as LevelPanel;
      _lvPanel.transform.SetParent(canvas.transform, false);

      skillPanel = Instantiate(skillPanel) as SkillPanel;
      skillPanel.transform.SetParent(canvas.transform, false);
      _player.Weapon.AttachSkillPanel(skillPanel);
    }

    public Bar        HpBar   => _hpBar;
    public Bar        ExpBar  => _expBar;
    public LevelPanel LvPanel => _lvPanel;

    [SerializeField] private Bar _hpBar;
    [SerializeField] private Bar _expBar;
    [SerializeField] private LevelPanel _lvPanel;
    private Player _player;
  }
}

