using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
  public void Spawn(string name, Vector3 pos) {
    PhotonNetwork.Instantiate("Prehabs/Enemy/" + name, pos, Quaternion.identity, 0);
  }
}

