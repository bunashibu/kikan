using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour {
  void Awake() {
    Idle = true;
  }

  public bool Idle { get; set; } // = true; C#6
  public bool Jump { get; set; }
  public bool Walk { get; set; }
  public bool Skill { get; set; }
  public bool LieDown { get; set; }
  public bool Die { get; set; }
}

