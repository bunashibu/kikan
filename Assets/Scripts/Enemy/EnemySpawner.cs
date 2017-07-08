using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemySpawner : MonoBehaviour {
  void Awake() {
    float ratioSum = _spawnRatio.Aggregate((x, y) => x + y);
    Assert.AreApproximatelyEqual(ratioSum, 1.0f, 0.001f);

    NetworkSpawn();
  }

  public void NetworkSpawn() {
    var pos = new Vector3(0.0f, 0.0f, 0.0f);

    int index = 0;
    var threshold = Random.value;
    Debug.Log(threshold);

    _spawnRatio.Aggregate((probability, x) => {
      if (probability > threshold) {
        return probability;
      } else {
        index += 1;
        return probability + x;
      }
    });

    PhotonNetwork.Instantiate("Prefabs/Enemy/" + _spawnEnemyNames[index], pos, Quaternion.identity, 0);
  }

  [SerializeField] private string[] _spawnEnemyNames;
  [SerializeField] private float[] _spawnRatio; // INFO: e.g. [0.1, 0.9]
}

