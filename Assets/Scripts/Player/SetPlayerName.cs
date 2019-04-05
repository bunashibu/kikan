using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bunashibu.Kikan {
  public class SetPlayerName : MonoBehaviour {
    void Start() {
      string defaultName = "";

      if (PlayerPrefs.HasKey(playerNamePrefKey))
        defaultName = PlayerPrefs.GetString(playerNamePrefKey);

      _inputField.text = defaultName;
    }

    public void SetName() {
      PhotonNetwork.playerName = _inputField.text + " ";
      PlayerPrefs.SetString(playerNamePrefKey, _inputField.text);
    }

    [SerializeField] private InputField _inputField;
    static string playerNamePrefKey = "PlayerName";
  }
}

