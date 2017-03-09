using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
  public void Enter(GameObject target) {
    Debug.Log("Portal Enter() was called");
    target.transform.position = _exit.transform.position;
  }

  [SerializeField] private GameObject _exit;
}

