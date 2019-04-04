using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class Enemy : MonoBehaviour, ICharacter, IOnDebuffed, IOnAttacked, IOnForced, IAttacker, IPhotonBehaviour, IKillRewardGiver {
    void Awake() {
      Movement      = new EnemyMovement();
      Movement.SetMoveForce(1.0f);
      Movement.SetJumpForce(400.0f);
      State         = new CharacterState();
      StateTransfer = new StateTransfer(_initState, _animator);
      Hp            = new Hp(_enemyData.Hp);
      Debuff        = new Debuff(transform);
      Debuff.Register(DebuffType.Stun, _stunEffect);
      Debuff.Register(DebuffType.Heavy, _heavyEffect);

      Location      = (IEnemyLocation)new Location(this);
      Location.InitializeFoot(_footCollider);

      Character = new Character2D(this);
    }

    void Start() {
      EnemyStreamBehaviour.Instance.Initialize(this);
    }

    public void AttachPopulationObserver(EnemyPopulationObserver populationObserver) {
      PopulationObserver = populationObserver;
    }

    // Unity
    public PhotonView     PhotonView   => _photonView;
    public Transform      Transform    => transform;
    public SpriteRenderer Renderer     => _spriteRenderer;
    public Rigidbody2D    Rigid        => _rigid;
    public Collider2D     BodyCollider => _bodyCollider;
    public Collider2D     FootCollider => _footCollider;
    public Animator       Animator     => _animator;

    public EnemyPopulationObserver PopulationObserver { get; private set; }

    // tmp
    public MonoBehaviour AI => _ai;

    public EnemyMovement  Movement      { get; private set; }
    public Character2D    Character     { get; private set; }
    public CharacterState State         { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }
    public Hp             Hp            { get; private set; }
    public Debuff         Debuff        { get; private set; }

    public IEnemyLocation Location { get; private set;}

    public TargetChaseSystem TargetChaseSystem => _targetChaseSystem;

    public Bar WorldHpBar => _worldHpBar;
    public EnemyData Data => _enemyData;

    public int    KillExp      => _killExp;
    public int    KillGold     => _killGold;
    public int    DamageSkinId => 0;
    public int    Power        => 0;
    public int    Critical     => 0;

    [Header("Unity/Photon Components")]
    [SerializeField] private PhotonView     _photonView;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D    _rigid;
    [SerializeField] private Collider2D     _bodyCollider;
    [SerializeField] private Collider2D     _footCollider;
    [SerializeField] private Animator       _animator;

    [Header("Kill Reward")]
    [SerializeField] private int _killExp;
    [SerializeField] private int _killGold;

    [Header("Debuff")]
    [SerializeField] private GameObject _stunEffect;
    [SerializeField] private GameObject _heavyEffect;

    // tmp
    [Space(10)]
    [SerializeField] private MonoBehaviour _ai;

    [SerializeField] private TargetChaseSystem _targetChaseSystem;

    [Header("Hp")]
    [SerializeField] private Bar _worldHpBar;

    [Header("Data")]
    [SerializeField] private EnemyData _enemyData;

    private static readonly string _initState = "Idle";
  }
}

