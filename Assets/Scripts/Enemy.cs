using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  void Start() {
    var health = ScriptableObject.CreateInstance<Health>();
    health.Init(100, 100);

    var hs = GetComponent<HealthSystem>();
    hs.Init(health, _bar);
    hs.Show();
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player") {
      collider.gameObject.GetComponent<HealthSystem>().IsDamaged(10);
    }
  }

  [SerializeField] private Bar _bar;
}

