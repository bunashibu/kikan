using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player") {
      collider.gameObject.GetComponent<HealthSystem>().IsDamaged(10);
    }
  }
}

