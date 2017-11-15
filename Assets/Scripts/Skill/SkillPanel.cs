using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class SkillPanel : MonoBehaviour {
    public List<GameObject> AlphaList { get { return _alphaList; } }

    [SerializeField] private List<GameObject> _alphaList;
  }
}

