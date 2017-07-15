using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopulationObserver : MonoBehaviour {
  void Start() {
    // Clean
    if (PhotonNetwork.player.IsMasterClient) {
      for (int i=0; i<_spawnerList.Count; ++i) {
        for (int k=0; k<_population[i]; ++k) {
          _spawnerList[i].NetworkSpawn();
        }
      }
    }
  }

  public void IntervalReplenishPopulation(int spawnerIndex) {
    MonoUtility.Instance.DelaySec(_intervalSec, () => {
      _spawnerList[spawnerIndex].NetworkSpawn();
    });
  }

  public int GetNearestSpawnerIndex() {
    return (int)(Random.value * (_spawnerList.Count - 1)); // TODO
  }

  [SerializeField] private List<EnemySpawner> _spawnerList;

  [Space(10)]
  [SerializeField] private int[] _population;
  [SerializeField] private float _intervalSec;
}

