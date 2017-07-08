using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopulationObserver : MonoBehaviour {
  public void IntervalReplenishPopulation(int spawnerIndex) {
    MonoUtility.Instance.DelaySec(_intervalSec, () => {
      _spawnerList[spawnerIndex].NetworkSpawn();
    });
  }

  [SerializeField] private List<EnemySpawner> _spawnerList;
  [Space(10)]
  [SerializeField] private int _population;
  [SerializeField] private float _intervalSec;
}

