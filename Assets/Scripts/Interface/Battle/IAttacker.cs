using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IAttacker : IMonoBehaviour {
    int Power    { get; }
    int Critical { get; }
  }
}

