using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[CreateAssetMenu]
public class ExpTable : ScriptableObject {
  public ReadOnlyCollection<int> Data {
    get {
      return Array.AsReadOnly(_data);
    }
  }

  [SerializeField] private int[] _data;
}

