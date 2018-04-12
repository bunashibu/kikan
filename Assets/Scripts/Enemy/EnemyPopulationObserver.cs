using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class EnemyPopulationObserver : MonoBehaviour {
    void Start() {
      if (PhotonNetwork.player.IsMasterClient)
        InitialSetupPopulation();
    }

    public void IntervalReplenishPopulation(Enemy enemy) {
      float offsetY = enemy.Renderer.bounds.size.y / 2;
      int index = GetNearestSpawnerIndex(enemy.transform.position, offsetY);
      float seedX = enemy.transform.position.x;

      MonoUtility.Instance.DelaySec(_intervalSec, () => {
        if (StageManager.Instance.StageName == "Battle")
          _spawnerList[index].NetworkSpawn(this, seedX);
      });
    }

    // Compare Enemy's foot with Ground
    private int GetNearestSpawnerIndex(Vector3 pos, float offsetY) {
      var list = _spawnerList.Where(spawner => spawner.SpawnArea.IsInRange(pos.x));

      var distance = list.Min(spawner =>
        System.Math.Abs(pos.y - offsetY - spawner.SpawnArea.CalculateY(pos.x))
      );

      var nearestSpawner = list.First(spawner =>
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

    [Space(10)]
    [SerializeField] private float _intervalSec;

    [Space(10)]
    [SerializeField] private float[] _initialSeedX;
  }
}

