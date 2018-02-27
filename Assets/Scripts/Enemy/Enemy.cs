using UnityEngine;
using System;
using System.Collections;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Character2D))]
  [RequireComponent(typeof(EnemyObserver))]
  public class Enemy : MonoBehaviour, ICharacter, IBattle {
    void Awake() {
      State         = new CharacterState(_ladderCollider, _footCollider);
      BuffState     = new BuffState();
      StateTransfer = new StateTransfer(_initState, _animator);
      Hp            = new EnemyHp(this, _hpBar, _enemyData.Life);
    }

    public void AttachPopulationObserver(EnemyPopulationObserver populationObserver) {
      PopulationObserver = populationObserver;
    }

    // Unity
    public PhotonView     PhotonView     { get { return _photonView;     } }
    public Transform      Transform      { get { return transform;       } }
    public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } }
    public Rigidbody2D    Rigid          { get { return _rigid;          } }
    public Collider2D     BodyCollider   { get { return _bodyCollider;   } }
    public Collider2D     FootCollider   { get { return _footCollider;   } }

    // Observer
    public EnemyObserver           Observer           { get { return _observer; } }
    public EnemyPopulationObserver PopulationObserver { get; private set; }

    // Environment
    public NumberPopupEnvironment NumberPopupEnvironment { get { return _numberPopupEnvironment; } }

    // tmp
    public MonoBehaviour AI { get { return _ai; } }

    // Enemy
    public CharacterState State         { get; private set; }
    public BuffState      BuffState     { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }
    public EnemyHp        Hp            { get; private set; }

    public int KillExp  { get { return _killExp;  } }
    public int KillGold { get { return _killGold; } }

    [Header("Unity/Photon Components")]
    [SerializeField] private PhotonView     _photonView;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D    _rigid;
    [SerializeField] private Collider2D     _bodyCollider;
    [SerializeField] private Collider2D     _ladderCollider;
    [SerializeField] private Collider2D     _footCollider;
    [SerializeField] private Animator       _animator;

    [Header("Observer")]
    [SerializeField] private EnemyObserver _observer;

    [Header("Environment")]
    [SerializeField] private NumberPopupEnvironment _numberPopupEnvironment;

    [Header("Kill Reward")]
    [SerializeField] private int _killExp;
    [SerializeField] private int _killGold;

    // tmp
    [Space(10)]
    [SerializeField] private MonoBehaviour _ai;

    [Header("Hp")]
    [SerializeField] private PlainBar _hpBar;

    [Header("Data")]
    [SerializeField] private EnemyData _enemyData;

    private static readonly string _initState = "Idle";
  }
}

