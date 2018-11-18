using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IMonoBehaviour {
    GameObject gameObject { get; }
    Transform  transform  { get; }
  }
}

