using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour {
  void Start() {
    var health = ScriptableObject.CreateInstance<Health>();
    health.Init(_data.life, _data.life);

    var hs = GetComponent<EnemyHealth>();
    hs.Init(health, _bar);
  }

  void OnTriggerEnter2D(Collider2D collider) {
    if (collider.gameObject.tag == "Player") {
      collider.gameObject.GetComponent<PlayerHealth>().IsDamaged(_data.atk * 2);
    }
  }

  public void ShowHealthBar() {
    _bar.gameObject.SetActive(true);
    MonoUtility.Instance.DelaySec(5.0f, () => {
      _bar.gameObject.SetActive(false);
    });
  }

  [SerializeField] private Bar _bar;
  [SerializeField] private EnemyStatus _data;
}

