using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerName : MonoBehaviour {
  void Start() {
    _text.text = PhotonNetwork.playerName;
  }

  [SerializeField] private Text _text;
}

