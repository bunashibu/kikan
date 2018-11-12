using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.Assertions;
using UniRx;

namespace Bunashibu.Kikan {
  // PLAN: BattlePlayer will be merged with LobbyPlayer.
  //       and will be renamed to Player.
  [RequireComponent(typeof(BattlePlayerObserver))]
  public class BattlePlayer : MonoBehaviour, ICharacter, IBattle, IRewardTaker, IKillDeathCounter, ISpeaker {
    void Awake() {
      Assert.IsTrue(_killExpTable.Data.Count  == MaxLevel);
      Assert.IsTrue(_killGoldTable.Data.Count == MaxLevel);
      Assert.IsTrue(_hpTable.Data.Count       == MaxLevel);
      Assert.IsTrue(_expTable.Data.Count      == MaxLevel);

      Teammates     = new List<IBattle>();
      Movement      = new BattlePlayerMovement(_core);
      State         = new CharacterState(_ladderCollider, _footCollider);
      BuffState     = new BuffState(Observer);
      KillDeath     = new PlayerKillDeath();
      Status        = new PlayerStatus(_jobStatus);
      StateTransfer = new StateTransfer(_initState, _animator);
      SkillInfo     = new SkillInfo();
      PlayerInfo    = new PlayerInfo(this);

      Hp         = new Hp(HpTable[0]);
      Exp        = new Exp(ExpTable[0]);
      Level      = new Level(1, MaxLevel);
      Gold       = new Gold(0, MaxGold);
      KillCount  = new ReactiveProperty<int>(0);
      DeathCount = new ReactiveProperty<int>(0);
    }

    void Start() {
      if (StageReference.Instance.StageData.Name == "Battle") {
        OnAttacked = BattleEnvironment.OnAttacked(this, NumberPopupEnvironment.Instance.PopupNumber);
        OnKilled   = BattleEnvironment.OnKilled(this, KillRewardEnvironment.GetRewardFrom, KillRewardEnvironment.GiveRewardTo);
      }

      PlayerInitializer.Instance.Initialize(this);
    }

    void FixedUpdate() {
      Movement.FixedUpdate(_rigid, transform);
    }

    public Action<IBattle, int, bool> OnAttacked { get; private set; }
    public Action<IBattle>            OnKilled   { get; private set; }

    public PhotonView       PhotonView   => _photonView;
    public Transform        Transform    => transform;
    public SpriteRenderer[] Renderers    => _renderers;
    public Rigidbody2D      Rigid        => _rigid;
    public Collider2D       BodyCollider => _bodyCollider;
    public Collider2D       FootCollider => _footCollider;
    public Animator         Animator     => _animator;

    public BattlePlayerObserver Observer => _observer;

    public AudioEnvironment     AudioEnvironment => _audioEnvironment;

    public List<IBattle>        Teammates     { get; private set; }

    public BattlePlayerMovement Movement      { get; private set; }
    public CharacterState       State         { get; private set; }
    public BuffState            BuffState     { get; private set; }
    public PlayerKillDeath      KillDeath     { get; private set; }
    public PlayerStatus         Status        { get; private set; }
    public StateTransfer        StateTransfer { get; private set; }
    public SkillInfo            SkillInfo     { get; private set; }
    public PlayerInfo           PlayerInfo    { get; private set; }

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

    public ReadOnlyCollection<int> HpTable  => _hpTable.Data;
    public ReadOnlyCollection<int> ExpTable => _expTable.Data;

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
    [SerializeField] private Collider2D       _ladderCollider;
    [SerializeField] private Collider2D       _footCollider;
    [SerializeField] private Animator         _animator;
    [SerializeField] private AudioListener    _audioListener;

    [Header("Observer")]
    [SerializeField] private BattlePlayerObserver _observer;

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

