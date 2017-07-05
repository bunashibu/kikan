using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour {
  void Awake() {
    float ratioSum = _spawnRatio.Aggregate((x, y) => x + y);
    Assert.AreApproximatelyEqual(ratioSum, 1.0f, 0.001f);
  }

  public void NetworkSpawn(string enemyName, Vector3 pos) {
    PhotonNetwork.Instantiate("Prehabs/Enemy/" + enemyName, pos, Quaternion.identity, 0);
  }

  [SerializeField] private string[] _spawnEnemyNames;
  [SerializeField] private float[] _spawnRatio; // INFO: e.g. [0.1, 0.9]
}

