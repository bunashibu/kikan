using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Weapon : MonoBehaviour {
    public SkillInstantiator SkillInstantiator { get { return _instantiator; } }

    [SerializeField] private SkillInstantiator _instantiator;
  }
}

