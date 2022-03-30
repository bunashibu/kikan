using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class DataTable : ScriptableObject {
    public ReadOnlyCollection<int> Data {
      get {
        return Array.AsReadOnly(_table);// TODO: Create ReadOnlyCollection all time when called
      }
    }

    [SerializeField] private int[] _table;
  }
}
