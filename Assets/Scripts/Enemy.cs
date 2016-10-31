using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D collider) {
    collider.gameObject.GetComponentInChildren<HealthSystem>().IsDamaged(10);
  }
}
