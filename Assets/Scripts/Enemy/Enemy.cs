using UnityEngine;
using System;
using System.Collections;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Character2D))]
  [RequireComponent(typeof(EnemyObserver))]
  public class Enemy : MonoBehaviour, ICharacter {
    void Awake() {
      State         = new CharacterState(_ladderCollider, _footCollider);
      StateTransfer = new StateTransfer(_initState, _animator);
    }

    public void AttachPopulationObserver(EnemyPopulationObserver populationObserver) {
      PopulationObserver = populationObserver;
    }

    // Unity
    public PhotonView     PhotonView     { get { return _photonView;     } }
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }
    public Rigidbody2D    Rigid          { get { return _rigid;          } }
    public Collider2D     LadderCollider { get { return _ladderCollider; } }
    public Collider2D     FootCollider   { get { return _footCollider;   } }

    // Observer
    public EnemyPopulationObserver PopulationObserver { get; private set; }

    // Enemy
    public CharacterState State         { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }

    public int KillExp  { get { return _killExp;  } }
    public int KillGold { get { return _killGold; } }

    [Header("Unity/Photon Components")]
    [SerializeField] private PhotonView     _photonView;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D    _rigid;
    [SerializeField] private Collider2D     _ladderCollider;
    [SerializeField] private Collider2D     _footCollider;
    [SerializeField] private Animator       _animator;

    [Header("Kill Reward")]
    [SerializeField] private int _killExp;
    [SerializeField] private int _killGold;

    private static readonly string _initState = "Idle";
  }
}

