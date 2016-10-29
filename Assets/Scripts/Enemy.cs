using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D collider) {
    Manji manji = collider.gameObject.GetComponentInChildren<Manji>();
    manji.DecreaseLife(10);
  }

  [SerializeField] protected int _life;
}
