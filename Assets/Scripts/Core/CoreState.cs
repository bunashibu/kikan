using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class CoreState{
    public CoreState() {
      _level = new Dictionary<CoreType, Level>();
      _isNeedReconfirm = new Dictionary<CoreType, bool>();
    }

    public void Register(CoreType type) {
      _level[type] = new Level(0, 5);
      _isNeedReconfirm[type] = true;
    }

    public void LevelUp(CoreType type) {
      _level[type].LevelUp();
    }

    public bool IsNeedReconfirm(CoreType type) {
      if (_isNeedReconfirm[type]) {
        _isNeedReconfirm[type] = false;
        return true;
      }
      else {
        _isNeedReconfirm[type] = true;
        return false;
      }
    }

    public int CurLevel(CoreType type) {
      return _level[type].Cur.Value;
    }

    public int MaxLevel(CoreType type) {
      return _level[type].Max.Value;
    }

    public Dictionary<CoreType, Level> Level => _level;

    private Dictionary<CoreType, Level> _level;
    private Dictionary<CoreType, bool> _isNeedReconfirm;
  }
}

