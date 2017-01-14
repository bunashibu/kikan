using UnityEngine;
using System.Collections;

public class RigidState : MonoBehaviour {
  public bool Ground {
    get {
      return true;
    }
  }

  public bool Air { get; private set; }

  public bool Stand {
    get {
      return true;
    }
  }

  public bool LieDown { get; private set; }
  public bool Ladder { get; private set; }
  public bool Slow { get; private set; }
  public bool Heavy { get; private set; }
  public bool Immobile { get; private set; }

  [SerializeField] private Rigidbody2D _rigid;
}

