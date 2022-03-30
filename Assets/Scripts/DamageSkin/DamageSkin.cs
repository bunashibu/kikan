using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class DamageSkin : ScriptableObject {
    public int Id { get { return _id; } }

    public ReadOnlyCollection<Sprite> Hit { // TODO: Create ReadOnlyCollection all time when called
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

    [SerializeField] private int _id;
    [SerializeField] private Sprite[] _hit;
    [SerializeField] private Sprite[] _critical;
    [SerializeField] private Sprite[] _take;
    [SerializeField] private Sprite[] _heal;
  }
}
