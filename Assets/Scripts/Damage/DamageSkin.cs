using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu]
public class DamageSkin : ScriptableObject {
  public ReadOnlyCollection<Sprite> Hit {
    get {
      return Array.AsReadOnly(_hit);
    }
  }

  public ReadOnlyCollection<Sprite> Critical {
    get {
      return Array.AsReadOnly(_critical);
    }
  }

  public ReadOnlyCollection<Sprite> Take {
    get {
      return Array.AsReadOnly(_take);
    }
  }

  public ReadOnlyCollection<Sprite> Heal {
    get {
      return Array.AsReadOnly(_heal);
    }
  }

  [SerializeField] private Sprite[] _hit;
  [SerializeField] private Sprite[] _critical;
  [SerializeField] private Sprite[] _take;
  [SerializeField] private Sprite[] _heal;
}

