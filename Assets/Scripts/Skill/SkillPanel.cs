using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class SkillPanel : MonoBehaviour {
    public List<SkillPanelUnit> UnitList { get { return _unitList; } }

    [SerializeField] private List<SkillPanelUnit> _unitList;
  }
}

