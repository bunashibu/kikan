using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(EnemyObserver))]
public class Enemy : MonoBehaviour, IKillReward {
  void Awake() {
    StateTransfer = new StateTransfer(_initState, _animator);
  }

  public void AttachPopulationObserver(EnemyPopulationObserver populationObserver) {
    PopulationObserver = populationObserver;
  }

  // Unity
  public PhotonView PhotonView { get { return _photonView; } }

  // Observer
  public EnemyPopulationObserver PopulationObserver { get; private set; }

  // Enemy
  public StateTransfer StateTransfer { get; private set; }

  public int KillExp  { get { return _killExp;  } }
  public int KillGold { get { return _killGold; } }

  [Header("Unity/Photon Components")]
  [SerializeField] private PhotonView _photonView;
  [SerializeField] private Animator   _animator;

  [Header("Kill Reward")]
  [SerializeField] private int _killExp;
  [SerializeField] private int _killGold;

  private static readonly string _initState = "Idle";
}

