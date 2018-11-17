using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Character2D))]
  public class Enemy : MonoBehaviour, ICharacter, IBattle, IOnDebuffed {
    void Awake() {
      State         = new CharacterState();
      StateTransfer = new StateTransfer(_initState, _animator);
      Hp            = new Hp(_enemyData.Hp);
      Debuff        = new Debuff(transform);
      Debuff.Register(DebuffType.Stun, _stunPrefab);

      Location      = new LocationJudger();
      Location.InitializeFootJudge(_footCollider);
    }

    void Start() {
      if (StageReference.Instance.StageData.Name == "Battle") {
        OnAttacked = BattleEnvironment.OnAttacked(this, NumberPopupEnvironment.Instance.PopupNumber);
        OnKilled   = BattleEnvironment.OnKilled(this, KillRewardEnvironment.GetRewardFrom, KillRewardEnvironment.GiveRewardTo);
        OnDebuffed = BattleEnvironment.OnDebuffed(this);
      }

      EnemyInitializer.Instance.Initialize(this);
    }

    public void AttachPopulationObserver(EnemyPopulationObserver populationObserver) {
      PopulationObserver = populationObserver;
    }

    public Action<IBattle, int, bool> OnAttacked { get; private set; }
    public Action<IBattle>            OnKilled   { get; private set; }
    public Action<DebuffType, float>  OnDebuffed { get; private set; }

    // Unity
    public PhotonView     PhotonView   => _photonView;
    public Transform      Transform    => transform;
    public SpriteRenderer Renderer     => _spriteRenderer;
    public Rigidbody2D    Rigid        => _rigid;
    public Collider2D     BodyCollider => _bodyCollider;
    public Collider2D     FootCollider => _footCollider;
    public Animator       Animator     => _animator;

    // Observer
    public EnemyPopulationObserver PopulationObserver { get; private set; }

    // tmp
    public MonoBehaviour AI => _ai;

    public CharacterState State         { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }
    public Hp             Hp            { get; private set; }
    public Debuff         Debuff        { get; private set; }

    public IEnemyLocationJudger Location { get; private set; }

    public Bar WorldHpBar => _worldHpBar;
    public EnemyData Data => _enemyData;

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
    [SerializeField] private Collider2D     _footCollider;
    [SerializeField] private Animator       _animator;

    [Header("Kill Reward")]
    [SerializeField] private int _killExp;
    [SerializeField] private int _killGold;

    [Header("Debuff")]
    [SerializeField] private GameObject _stunPrefab;

    // tmp
    [Space(10)]
    [SerializeField] private MonoBehaviour _ai;

    [Header("Hp")]
    [SerializeField] private Bar _worldHpBar;

    [Header("Data")]
    [SerializeField] private EnemyData _enemyData;

    private static readonly string _initState = "Idle";
  }
}

