using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

namespace Bunashibu.Kikan {
  public class Version : MonoBehaviour {
    void Awake() {
      _tmp = GetComponent<TextMeshPro>();
      _tmp.text = GameData.Instance.GameVersion;
    }

    private TextMeshPro _tmp;
  }
}
