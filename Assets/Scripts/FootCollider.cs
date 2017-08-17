using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider {
  public FootCollider(BoxCollider2D boxCollider, CircleCollider2D circleCollider) {
    _boxCollider = boxCollider;
    _circleCollider = circleCollider;
  }

  public bool IsTouchingLayers(LayerMask mask) {
    return _boxCollider.IsTouchingLayers(mask) || _circleCollider.IsTouchingLayers(mask);
  }

  public void TriggerON() {
    _boxCollider.isTrigger    = true;
    _circleCollider.isTrigger = true;
  }

  public void TriggerOFF() {
    _boxCollider.isTrigger    = false;
    _circleCollider.isTrigger = false;
  }

  public void UseBox() {
    _boxCollider.enabled    = true;
    _circleCollider.enabled = false;
  }

  public void UseCircle() {
    _boxCollider.enabled    = false;
    _circleCollider.enabled = true;
  }

  private BoxCollider2D _boxCollider;
  private CircleCollider2D _circleCollider;
}

