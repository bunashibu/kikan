using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class CoreInfo : ScriptableObject {
    public int Value(int index) {
      return _valueTable.Data[index];
    }

    public int Gold(int index) {
      return _goldTable.Data[index];
    }

    public KeyCode Key => _keyCode;

    [SerializeField] private DataTable _valueTable;
    [SerializeField] private DataTable _goldTable;
    [SerializeField] private KeyCode _keyCode;
  }
}

