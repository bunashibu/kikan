using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class TeammateHpMemberPanel : MonoBehaviour {
    public void SetName(string name) {
      _nameText.text = name;
    }

    public Bar HpBar => _hpBar;

    [SerializeField] private Text _nameText;
    [SerializeField] private Bar _hpBar;
  }
}

