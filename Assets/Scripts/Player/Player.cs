using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Bunashibu.Kikan {
  public class Player : MonoBehaviour, IPhotonBehaviour, ICharacter, ISpeaker, IPlayer, IBattleMovementPlayer, ILobbyMovementPlayer, ICorePlayer, IAttacker, IOnAttacked, IOnDebuffed, IKillRewardTaker, IKillRewardGiver {
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
        Movement = new PlayerMovement((ILobbyMovementPlayer)this);
        // tmp
        Movement.SetMoveForce(4.0f);
        Movement.SetJumpForce(400.0f);
        Movement.SetMaxFallVelocity(-11.0f);

        // tmp
        SkillInfo  = new SkillInfo();
      }

      if (StageReference.Instance.StageData.Name == "Battle") {
        Assert.IsTrue(_killExpTable.Data.Count  == MaxLevel);
        Assert.IsTrue(_killGoldTable.Data.Count == MaxLevel);
        Assert.IsTrue(_hpTable.Data.Count       == MaxLevel);
        Assert.IsTrue(_expTable.Data.Count      == MaxLevel);

        Movement   = new PlayerMovement((IBattleMovementPlayer)this);
        Movement.SetMaxFallVelocity(-11.0f);

        Teammates  = new List<Player>();
        Status     = new PlayerStatus(_jobStatus);
        SkillInfo  = new SkillInfo();
        PlayerInfo = new PlayerInfo(this);

        Hp         = new Hp(HpTable[0]);
        Exp        = new Exp(ExpTable[0]);
        Level      = new Level(1, MaxLevel);
        Gold       = new Gold(0, MaxGold);
        KillCount  = new ReactiveProperty<int>(0);
        DeathCount = new ReactiveProperty<int>(0);

        Debuff     = new Debuff(transform);
        Debuff.Register(DebuffType.Stun, _stunEffect);

        Core       = new Core((ICorePlayer)this);
        Core.Register(CoreType.Speed,    _speedCoreInfo,    _speedCoreEffect   );
        Core.Register(CoreType.Hp,       _hpCoreInfo,       _hpCoreEffect      );
        Core.Register(CoreType.Attack,   _attackCoreInfo,   _attackCoreEffect  );
        Core.Register(CoreType.Critical, _criticalCoreInfo, _criticalCoreEffect);
        Core.Register(CoreType.Heal,     _healCoreInfo,     _healCoreEffect    );

        OnAttacked = BattleEnvironment.OnAttacked(this, NumberPopupEnvironment.Instance.PopupNumber,
                                                        HitEffectPopupEnvironment.Instance.PopupHitEffect);
        OnKilled   = BattleEnvironment.OnKilled(this, KillRewardEnvironment.GetRewardFrom, KillRewardEnvironment.GiveRewardTo);
        OnDebuffed = BattleEnvironment.OnDebuffed(this);

        PlayerInitializer.Instance.Initialize(this);
      }
    }

    public Action<IAttacker, int, bool, HitEffectType> OnAttacked { get; private set; }
    public Action<IAttacker>                           OnKilled   { get; private set; }
    public Action<DebuffType, float>                   OnDebuffed { get; private set; }

    public PhotonView       PhotonView   => _photonView;
    public SpriteRenderer[] Renderers    => _renderers;
    public Rigidbody2D      Rigid        => _rigid;
    public Collider2D       BodyCollider => _bodyCollider;
    public Collider2D       FootCollider => _footCollider;
    public Animator         Animator     => _animator;

    public PlayerSynchronizer Synchronizer => _synchronizer;
    public PlayerObserver     Observer     => _observer;

    public AudioEnvironment AudioEnvironment => _audioEnvironment;

    public List<Player> Teammates { get; private set; }
    public List<IKillRewardTaker> RewardTeammates => Teammates.Select(player => (IKillRewardTaker)player).ToList();

    public PlayerMovement Movement      { get; private set; }
    public PlayerStatus   Status        { get; private set; }
    public StateTransfer  StateTransfer { get; private set; }
    public SkillInfo      SkillInfo     { get; private set; }
    public PlayerInfo     PlayerInfo    { get; private set; }
    public CharacterState State         { get; private set; }

    public int MaxLevel         => 15;
    public int MaxGold          => 99999;
    public int DamageSkinId     => 0;
    public float HealInterval   => 1.0f;
    public int KillExp          => _killExpTable.Data[Level.Cur.Value - 1];
    public int KillGold         => _killGoldTable.Data[Level.Cur.Value - 1];
    public int Critical         => Core.GetValue(CoreType.Critical);

    public int Power { get {
      double ratio = (double)((Core.GetValue(CoreType.Attack) + 100) / 100.0);
      return (int)(Status.Atk * Status.MulCorrectionAtk * ratio);
    } }
    public int AutoHealQuantity { get {
      double ratio = (double)(Core.GetValue(CoreType.Heal) / 100.0);
      return (int)(_autoHealTable.Data[Level.Cur.Value - 1] * ratio);
    } }

    public Hp                    Hp         { get; private set; }
    public Exp                   Exp        { get; private set; }
    public Level                 Level      { get; private set; }
    public Gold                  Gold       { get; private set; }
    public ReactiveProperty<int> KillCount  { get; private set; }
    public ReactiveProperty<int> DeathCount { get; private set; }
    public Debuff                Debuff     { get; private set; }
    public Core                  Core       { get; private set; }

    public ReadOnlyCollection<int> HpTable  => _hpTable.Data;
    public ReadOnlyCollection<int> ExpTable => _expTable.Data;

    public IPlayerLocationJudger Location { get; private set; }

    public NameBackground NameBackground => _nameBackground;
    public PopupRemark    PopupRemark    => _popupRemark;
    public Bar            WorldHpBar     => _worldHpBar;

    // tmp
    public Character2D Character  => _character;
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

    [Header("Observer")]
    [SerializeField] private PlayerSynchronizer _synchronizer;
    [SerializeField] private PlayerObserver _observer;

    [Header("Environment")]
    [SerializeField] private AudioEnvironment _audioEnvironment;

    [Header("Hp/Exp/AutoHeal")]
    [SerializeField] private DataTable _hpTable;
    [SerializeField] private DataTable _expTable;
    [SerializeField] private DataTable _autoHealTable;

    [Header("Kill Reward")]
    [SerializeField] private DataTable _killExpTable;
    [SerializeField] private DataTable _killGoldTable;

    [Header("Debuff")]
    [SerializeField] private GameObject _stunEffect;

    [Header("CoreInfo")]
    [SerializeField] private CoreInfo _speedCoreInfo;
    [SerializeField] private CoreInfo _hpCoreInfo;
    [SerializeField] private CoreInfo _attackCoreInfo;
    [SerializeField] private CoreInfo _criticalCoreInfo;
    [SerializeField] private CoreInfo _healCoreInfo;

    [Header("CoreEffect")]
    [SerializeField] private GameObject _speedCoreEffect;
    [SerializeField] private GameObject _hpCoreEffect;
    [SerializeField] private GameObject _attackCoreEffect;
    [SerializeField] private GameObject _criticalCoreEffect;
    [SerializeField] private GameObject _healCoreEffect;

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

