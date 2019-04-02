using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class PlayerPanel : MonoBehaviour {
    public void Register(Player player) {
      UpdateTeam(player.PlayerInfo.Team);
      UpdateName(player.PlayerInfo.Name);
      UpdateJob(player.PlayerInfo.Job);
    }

    private void UpdateTeam(int team) {
      if (team == 0)
        _team.color = new Color(174.0f/255.0f, 27.0f/255.0f, 27.0f/255.0f);
      else if (team == 1)
        _team.color = new Color(0, 138.0f/255.0f, 219.0f/255.0f);
      else
        Debug.Log("Error team color");
    }

    private void UpdateName(string name) {
      _name.text = name;
    }

    private void UpdateJob(string job) {
      _job.text = job;
    }

    [SerializeField] private Image _team;
    [SerializeField] private Text _name;
    [SerializeField] private Text _job;
  }
}

