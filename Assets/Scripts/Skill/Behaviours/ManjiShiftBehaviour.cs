using UnityEngine;
using System.Collections;

public class ManjiShiftBehaviour : SkillBehaviour {
  void Awake() {
    _collider.enabled = false;
  }

  void Update() {

  }

  public override void Behave() {
    _collider.enabled = true;
  }

  [SerializeField] private BoxCollider2D _collider;
}
