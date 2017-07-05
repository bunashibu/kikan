using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPopulationObserver : MonoBehaviour {
  public void RegisterToSpawnList() {

  }

  [SerializeField] private List<EnemySpawner> _spawnerList;
  [Space(10)]
  [SerializeField] private int _population;
}

