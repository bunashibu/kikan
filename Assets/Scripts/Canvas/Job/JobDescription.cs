using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobDescription : MonoBehaviour {
    void Awake() {
      _names = new string[6];
      _features = new string[6];
      _names[0] = "卍";
      _names[1] = "魔法使い";
      _names[2] = "投げ賊";
      _names[3] = "パンダ";
      _names[4] = "戦士";
      _names[5] = "弓";
      _features[0] = "バランスのとれた職";
      _features[1] = "最高火力のスキルがある";
      _features[2] = "設置型のスキルが多い";
      _features[3] = "空中戦に強い";
      _features[4] = "機動力は低いが耐久力がある";
      _features[5] = "HPは低いが攻撃力と機動力が高い";
    }

    public void UpdateLabel(int n) {
      _nameText.text = _names[n];
      _featureText.text = _features[n];
    }

    [SerializeField] private Text _nameText;
    [SerializeField] private Text _featureText;

    private string[] _names;
    private string[] _features;
  }
}
