using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class Core : ScriptableObject {
    public void LvUp() {
      _level += 1;
    }
  
    public KeyCode Key {
      get {
        return _keyCode;
      }
    }
  
    public int Value {
      get {
        return _valueTable.Data[_level];
      }
    }
  
    public int Gold {
      get {
        return _goldTable.Data[_level];
      }
    }
  
    public int Level {
      get {
        return _level;
      }
    }
  
    [SerializeField] private DataTable _valueTable;
    [SerializeField] private DataTable _goldTable;
    [SerializeField] private KeyCode _keyCode;
    private int _level = 0;
  }
}

