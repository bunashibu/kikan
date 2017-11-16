using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SkillPanelUnit : MonoBehaviour {
    public RectTransform AlphaRectTransform { get { return _alphaRectTransform; } }

    [SerializeField] private RectTransform _alphaRectTransform;
  }
}

