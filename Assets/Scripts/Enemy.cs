using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
  void OnTriggerEnter2D(Collider2D collider) {
    Job player = collider.gameObject.GetComponentInChildren<Job>();
    player.DecreaseLife(10);
  }

  [SerializeField] protected int _life;
}
