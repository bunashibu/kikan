using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class EnemyData : ScriptableObject {
    public int Id {
      get { return _id; }
    }

    public int Hp {
      get { return _hp; }
    }

    public int Atk {
      get { return _atk; }
    }

    public int Dfn {
      get { return _dfn; }
    }

    public int Spd{
      get { return _spd; }
    }

    public int Jmp {
      get { return _jmp; }
    }

    [SerializeField] private int _id;
    [SerializeField] private int _hp;
    [SerializeField] private int _atk;
    [SerializeField] private int _dfn;
    [SerializeField] private int _spd;
    [SerializeField] private int _jmp;
  }
}

