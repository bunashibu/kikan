using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(EnemyObserver))]
public class Enemy : MonoBehaviour, IKillReward {
  public PhotonView PhotonView { get { return _photonView; } }

  public int KillExp  { get { return _killExp;  } }
  public int KillGold { get { return _killGold; } }

  [SerializeField] private PhotonView _photonView;

  [Header("Kill Reward")]
  [SerializeField] private int _killExp;
  [SerializeField] private int _killGold;
}

