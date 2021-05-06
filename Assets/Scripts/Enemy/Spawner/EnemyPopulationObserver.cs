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
      int index = GetSpawnerIndex(enemy);
      float seedX = enemy.transform.position.x;

      MonoUtility.Instance.DelaySec(_intervalSec, () => {
        if (StageReference.Instance.StageData.Name == "Battle")
          _spawnerList[index].NetworkSpawn(this, seedX);
      });
    }

    private int GetSpawnerIndex(Enemy enemy) {
      for (int i=0; i<_spawnerList.Count; ++i) {
        if (_spawnerList[i] == enemy.Spawner)
          return i;
      }

      throw new Exception();
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
