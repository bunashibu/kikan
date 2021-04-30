using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  [CreateAssetMenu]
  public class JobStatus : ScriptableObject {
    public float Atk {
      get { return _atk; }
    }

    public float Dfn {
      get { return _dfn; }
    }

    public float Spd{
      get { return _spd; }
    }

    public float Jmp {
      get { return _jmp; }
    }

    [SerializeField] private float _atk;
    [SerializeField] private float _dfn;
    [SerializeField] private float _spd;
    [SerializeField] private float _jmp;
  }
}
