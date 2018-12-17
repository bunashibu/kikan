using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class KillMessagePanel : MonoBehaviour {
    void Awake() {
      _messageList = new List<KillMessage>();
    }

    public void InstantiateMessage(Player killPlayer, Player deathPlayer, bool isSameTeam) {
      var killMessage = Instantiate(_killMessagePref, transform).GetComponent<KillMessage>();

      killMessage.SetKillPlayerName(killPlayer.PlayerInfo.Name, isSameTeam);
      killMessage.SetDeathPlayerName(deathPlayer.PlayerInfo.Name);

      AdjustPosition(killMessage);
    }

    private void AdjustPosition(KillMessage killMessage) {
      if (Time.time - _firstTime > 5.0f)
        _index = 0;

      if (_index == 0)
        _firstTime = Time.time;

      if (_messageList.Count < _index + 1)
        _messageList.Add(killMessage);
      else {
        if (_messageList[_index] != null)
          Destroy(_messageList[_index].gameObject);
        _messageList[_index] = killMessage;
      }

      killMessage.transform.position = killMessage.transform.position + new Vector3(0, -20 * _index, 0);
      _index += 1;
    }

    [SerializeField] private GameObject _killMessagePref;
    private List<KillMessage> _messageList;
    private float _firstTime = 0;
    private int _index = 0;
  }
}

