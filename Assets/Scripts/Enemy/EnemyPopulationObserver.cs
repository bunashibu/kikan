using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPopulationObserver : MonoBehaviour {
  void Start() {
    if (PhotonNetwork.player.IsMasterClient)
      InitialSetupPopulation();
  }

  public void IntervalReplenishPopulation(Enemy enemy) {
    int index = GetNearestSpawnerIndex(enemy.transform.position);
    float seedX = enemy.transform.position.x;

    MonoUtility.Instance.DelaySec(_intervalSec, () => {
      _spawnerList[index].NetworkSpawn(this, seedX);
    });
  }

  public int GetNearestSpawnerIndex(Vector3 pos) {
    var distance = _spawnerList.Min(spawner => System.Math.Abs(pos.y - spawner.gameObject.transform.position.y));
    var nearestSpawner = _spawnerList.First(spawner => System.Math.Abs(pos.y - spawner.gameObject.transform.position.y) == distance);

    return _spawnerList.IndexOf(nearestSpawner);
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

