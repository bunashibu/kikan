using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class DataTable : ScriptableObject, IObserver {
    public DataTable() {
      Notifier = new Notifier();
    }

    // to be abstract
    public virtual void OnNotify(Notification notification, object[] args) {}

    public Notifier Notifier { get; private set; }

    public ReadOnlyCollection<int> Data { // doesn't need?
      get {
        return Array.AsReadOnly(_table);
      }
    }

    [SerializeField] private int[] _table;
  }
}

