using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetPlayerName : MonoBehaviour {
  public void SetName() {
    GameManager.playerName = text.text;
  }

  public Text text;
}
