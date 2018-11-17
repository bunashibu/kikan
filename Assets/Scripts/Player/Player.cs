using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Bunashibu.Kikan {
  public class Player : MonoBehaviour, IPlayer {
    void Awake() {
      State         = new CharacterState();
      StateTransfer = new StateTransfer(_initState, _animator);
      Location      = new LocationJudger();
      Location.InitializeFootJudge(_footCollider);
      Location.InitializeCenterJudge(_centerCollider);
    }

    // THINK: coupling with global reference. to be a stream
    void Start() {
      if (StageReference.Instance.StageData.Name == "Lobby") {
        Movement = new PlayerMovement(_rigid, transform);
        // tmp
        Movement.SetMoveForce(4.0f);
        Movement.SetJumpForce(400.0f);
      }

      if (StageReference.Instance.StageData.Name == "Battle") {
        Assert.IsTrue(_killExpTable.Data.Count  == MaxLevel);
        Assert.IsTrue(_killGoldTable.Data.Count == MaxLevel);
        Assert.IsTrue(_hpTable.Data.Count       == MaxLevel);
        Assert.IsTrue(_expTable.Data.Count      == MaxLevel);

        Movement    = new PlayerMovement(_rigid, transform, _core);
        Teammates   = new List<IPlayer>();
        BuffState   = new BuffState(Observer);
        Status      = new PlayerStatus(_jobStatus);
        SkillInfo   = new SkillInfo();
        PlayerInfo  = new PlayerInfo(this);

        Hp         = new Hp(HpTable[0]);
        Exp        = new Exp(ExpTable[0]);
        Level      = new Level(1, MaxLevel);
        Gold       = new Gold(0, MaxGold);
        KillCount  = new ReactiveProperty<int>(0);
        DeathCount = new ReactiveProperty<int>(0);
        Debuff     = new Debuff(DebuffType.Stun);

        OnAttacked = BattleEnvironment.OnAttacked(this, NumberPopupEnvironment.Instance.PopupNumber);
        OnKilled   = BattleEnvironment.OnKilled(this, KillRewardEnvironment.GetRewardFrom, KillRewardEnvironment.GiveRewardTo);
        OnDebuffed = BattleEnvironment.OnDebuffed(this);

        PlayerInitializer.Instance.Initialize(this);
      }
    }

    void FixedUpdate() {
      Movement.FixedUpdate();
    }

    public Action<IBattle, int, bool> OnAttacked { get; private set; }
    public Action<IBattle>            OnKilled   { get; private set; }
    public Action<DebuffType, float>  OnDebuffed { get; private set; }

    public PhotonView       PhotonView   => _photonView;
    public Transform        Transform    => transform;
    public SpriteRenderer[] Renderers    => _renderers;
    public Rigidbody2D      Rigid        => _rigid;
    public Collider2D       BodyCollider => _bodyCollider;
    public Collider2D       FootCollider => _footCollider;
    public Animator         Animator     => _animator;

    public PlayerObserver Observer => _observer;

    public AudioEnvironment AudioEnvironment => _audioEnvironment;

    public List<IPlayer>  Teammates     { get; private set; }

    public PlayerMovement Movement      { get; private set; }
    public BuffState      BuffState     { get; private set; }
    public PlayerStatus   Status        { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }
    public SkillInfo      SkillInfo     { get; private set; }
    public PlayerInfo     PlayerInfo    { get; private set; }
    public CharacterState State         { get; private set; }

    public int    MaxLevel     => 15;
    public int    MaxGold      => 99999;
    public int    KillExp      => _killExpTable.Data[Level.Cur.Value - 1];
    public int    KillGold     => _killGoldTable.Data[Level.Cur.Value - 1];
    public int    DamageSkinId => 0;
    public int    Power        { get { double ratio = (double)((Core.Attack + 100) / 100.0);
                                       return (int)(Status.Atk * Status.MulCorrectionAtk * ratio); } }
    public int    Critical     => Core.Critical;
    public string Tag          => gameObject.tag;

    public Hp                    Hp         { get; private set; }
    public Exp                   Exp        { get; private set; }
    public Level                 Level      { get; private set; }
    public Gold                  Gold       { get; private set; }
    public ReactiveProperty<int> KillCount  { get; private set; }
    public ReactiveProperty<int> DeathCount { get; private set; }
    public Debuff                Debuff     { get; private set; }

    public ReadOnlyCollection<int> HpTable  => _hpTable.Data;
    public ReadOnlyCollection<int> ExpTable => _expTable.Data;

    public IPlayerLocationJudger Location { get; private set; }

    public NameBackground NameBackground => _nameBackground;
    public PopupRemark    PopupRemark    => _popupRemark;
    public Bar            WorldHpBar     => _worldHpBar;

    // tmp
    public Character2D Character  => _character;
    public PlayerCore  Core       => _core;
    public DamageSkin  DamageSkin => _damageSkin;
    public Weapon      Weapon     => _weapon;

    [Header("Unity/Photon Components")]
    [SerializeField] private PhotonView       _photonView;
    [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
    [SerializeField] private Rigidbody2D      _rigid;
    [SerializeField] private Collider2D       _bodyCollider;
    [SerializeField] private Collider2D       _centerCollider;
    [SerializeField] private Collider2D       _footCollider;
    [SerializeField] private Animator         _animator;
    [SerializeField] private AudioListener    _audioListener;

    [Header("Observer")]
    [SerializeField] private PlayerObserver _observer;

    [Header("Environment")]
    [SerializeField] private AudioEnvironment _audioEnvironment;

    [Header("Data")]
    [SerializeField] private DataTable _hpTable;
    [SerializeField] private DataTable _expTable;
    [SerializeField] private DataTable _killExpTable;
    [SerializeField] private DataTable _killGoldTable;

    // Obsolete
    [Header("Sync On Their Own")]
    [SerializeField] private PlayerCore _core;

    [Header("Canvas")]
    [SerializeField] private NameBackground _nameBackground;
    [SerializeField] private PopupRemark    _popupRemark;
    [SerializeField] private Bar            _worldHpBar;

    [Space(10)]
    [SerializeField] private Character2D _character;

    [Space(10)]
    [SerializeField] private DamageSkin _damageSkin;

    [Space(10)]
    [SerializeField] private JobStatus _jobStatus;
    [SerializeField] private Weapon    _weapon;

    private static readonly string _initState = "Idle";
  }
}

