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
    float offsetY = enemy.SpriteRenderer.bounds.size.y / 2;
    int index = GetNearestSpawnerIndex(enemy.transform.position, offsetY);
    float seedX = enemy.transform.position.x;

    MonoUtility.Instance.DelaySec(_intervalSec, () => {
      _spawnerList[index].NetworkSpawn(this, seedX);
    });
  }

  // Compare Enemy's foot with Ground
  public int GetNearestSpawnerIndex(Vector3 pos, float offsetY) {
    var distance = _spawnerList.Min(spawner =>
      System.Math.Abs(pos.y - offsetY - spawner.SpawnArea.CalculateY(pos.x))
    );
    var nearestSpawner= _spawnerList.First(spawner =>
      System.Math.Abs(pos.y - offsetY - spawner.SpawnArea.CalculateY(pos.x)) == distance
    );

    return _spawnerList.IndexOf(nearestSpawner);
  }

  private void InitialSetupPopulation() {
    for (int i=0; i<_spawnerList.Count; ++i) {
      for (int k=0; k<_population[i]; ++k)
        _spawnerList[i].NetworkSpawn(this, _initialSeedX[i]);
    }
  }

  [SerializeField] private List<EnemySpawner> _spawnerList;

  [Space(10)]
  [SerializeField] private int[] _population;
  [SerializeField] private float _intervalSec;

  [Space(10)]
  [SerializeField] private float[] _initialSeedX;
}

