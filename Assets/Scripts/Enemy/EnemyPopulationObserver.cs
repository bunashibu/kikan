using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopulationObserver : MonoBehaviour {
  void Start() {
    if (PhotonNetwork.player.IsMasterClient)
      InitialSetupPopulation();
  }

  public void IntervalReplenishPopulation(Enemy enemy) {
    int index = GetNearestSpawnerIndex();

    MonoUtility.Instance.DelaySec(_intervalSec, () => {
      _spawnerList[index].NetworkSpawn(this);
    });
  }

  public int GetNearestSpawnerIndex() {
    return (int)(Random.value * (_spawnerList.Count - 1)); // TODO
  }

  private void InitialSetupPopulation() {
    for (int i=0; i<_spawnerList.Count; ++i) {
      for (int k=0; k<_population[i]; ++k)
        _spawnerList[i].NetworkSpawn(this);
    }
  }

  [SerializeField] private List<EnemySpawner> _spawnerList;

  [Space(10)]
  [SerializeField] private int[] _population;
  [SerializeField] private float _intervalSec;
}

