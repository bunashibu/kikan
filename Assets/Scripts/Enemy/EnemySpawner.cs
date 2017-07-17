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

  public void NetworkSpawn(EnemyPopulationObserver populationObserver) {
    int index = calcEnemyIndex();
    var pos = new Vector3(Random.value * 5.0f, Random.value * 2.0f, 0.0f); // TODO

    var enemyObj = PhotonNetwork.Instantiate("Prefabs/Enemy/" + _spawnEnemyNames[index], pos, Quaternion.identity, 0);
    var enemy    = enemyObj.GetComponent<Enemy>();
    enemy.AttachPopulationObserver(populationObserver);
  }

  private int calcEnemyIndex() {
    int index = 0;
    var threshold = Random.value;

    _spawnRatio.Aggregate((probability, x) => {
      if (probability > threshold) {
        return probability;
      } else {
        index += 1;
        return probability + x;
      }
    });

    return index;
  }

  [SerializeField] private string[] _spawnEnemyNames;
  [SerializeField] private float[] _spawnRatio; // INFO: e.g. [0.1, 0.9]
}

