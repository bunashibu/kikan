using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobDescription : MonoBehaviour {
    void Awake() {
      _names = new string[2];
      _features = new string[2];
      _names[0] = "卍";
      _names[1] = "魔法使い";
      _features[0] = "バランスが良い";
      _features[1] = "HPは低いが火力が高い";
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

