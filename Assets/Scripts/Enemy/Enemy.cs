using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(EnemyObserver))]
public class Enemy : MonoBehaviour {
  public PhotonView PhotonView { get { return _photonView; } }

  [SerializeField] private PhotonView _photonView;
}

