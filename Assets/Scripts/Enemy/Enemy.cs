using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Character2D))]
  [RequireComponent(typeof(EnemyObserver))]
  public class Enemy : MonoBehaviour, ICharacter, IBattle {
    void Awake() {
      _mediator     = new EnemyMediator(this);
      State         = new CharacterState(_ladderCollider, _footCollider);
      BuffState     = new BuffState(Observer);
      StateTransfer = new StateTransfer(_initState, _animator);
      Hp            = new Hp(_enemyData.Hp);

      //_mediator.AddListener(Hp.OnNotify);
      //_mediator.AddListener(NumberPopupEnvironment.Instance.OnNotify);
      //_mediator.AddListener(KillRewardEnvironment.Instance.OnNotify);

      var notifier = new Notifier(_mediator.OnNotify);
      notifier.Notify(Notification.EnemyInstantiated);
    }

    void Start() {
      if (StageReference.Instance.StageData.Name == "Battle") {
        OnAttacked = BattleEnvironment.OnAttacked(this, NumberPopupEnvironment.Instance.PopupNumber);
        OnKilled   = BattleEnvironment.OnKilled(this, KillRewardEnvironment.GetRewardFrom, KillRewardEnvironment.GiveRewardTo);
      }

      EnemyInitializer.Instance.Initialize(this);
    }

    public void OnNotify(Notification notification, object[] args) {
      _mediator.OnNotify(notification, args);
    }

    public void AttachPopulationObserver(EnemyPopulationObserver populationObserver) {
      PopulationObserver = populationObserver;
    }

    public Action<IBattle, int, bool> OnAttacked { get; private set; }
    public Action<IBattle>            OnKilled   { get; private set; }

    // Unity
    public PhotonView     PhotonView   => _photonView;
    public Transform      Transform    => transform;
    public SpriteRenderer Renderer     => _spriteRenderer;
    public Rigidbody2D    Rigid        => _rigid;
    public Collider2D     BodyCollider => _bodyCollider;
    public Collider2D     FootCollider => _footCollider;

    // Observer
    public EnemyObserver           Observer           { get { return _observer; } }
    public EnemyPopulationObserver PopulationObserver { get; private set; }

    public List<IBattle>           Teammates     { get; private set; }

    // tmp
    public MonoBehaviour AI => _ai;

    public EnemyData Data => _enemyData;

    // Enemy
    public CharacterState State         { get; private set; }
    public BuffState      BuffState     { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }
    public Hp             Hp            { get; private set; }

    public Bar            WorldHpBar     => _worldHpBar;

    public int    KillExp      => _killExp;
    public int    KillGold     => _killGold;
    public int    DamageSkinId => 0;
    public int    Power        => 0;
    public int    Critical     => 0;
    public string Tag          => gameObject.tag;

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

    [Header("Kill Reward")]
    [SerializeField] private int _killExp;
    [SerializeField] private int _killGold;

    // tmp
    [Space(10)]
    [SerializeField] private MonoBehaviour _ai;

    [Header("Hp")]
    [SerializeField] private Bar _worldHpBar;

    [Header("Data")]
    [SerializeField] private EnemyData _enemyData;

    private static readonly string _initState = "Idle";
    private EnemyMediator _mediator;
  }
}

