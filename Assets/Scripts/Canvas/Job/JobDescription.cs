using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobDescription : MonoBehaviour {
    void Awake() {
      _names = new string[4];
      _features = new string[4];
      _names[0] = "卍";
      _names[1] = "魔法使い";
      _names[2] = "投";
      _names[3] = "パンダ";
      _features[0] = "機動力が高い";
      _features[1] = "HPは低いが火力が高い";
      _features[2] = "遠距離から攻撃できる";
      _features[3] = "接近戦に強い";
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

