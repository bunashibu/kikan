using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattlePlayerObserver))]
public class BattlePlayer : MonoBehaviour, IKillReward {
  void Awake() {
    Movement      = new BattlePlayerMovement();
    State         = new PlayerState(_colliderCenter, _colliderFoot);
    Hp            = new PlayerHp(this, _hpTable, _worldHpBar);
    StateTransfer = new StateTransfer(_initState);
    SkillInfo     = new SkillInfo();
  }

  void FixedUpdate() {
    Movement.FixedUpdate(_rigid, _trans);
  }

  public PhotonView       PhotonView   { get { return _photonView;   } }
  public SpriteRenderer[] Renderers    { get { return _renderers;    } }
  public Rigidbody2D      Rigid        { get { return _rigid;        } }
  public BoxCollider2D    ColliderFoot { get { return _colliderFoot; } }

  public BattlePlayerObserver Observer { get { return _observer; } }

  public BattlePlayerMovement Movement      { get; private set; }
  public PlayerState          State         { get; private set; }
  public PlayerHp             Hp            { get; private set; }
  public StateTransfer        StateTransfer { get; private set; }
  public SkillInfo            SkillInfo     { get; private set; }

  public int KillExp  { get { return _expTable.Data[Level.Lv - 1];  } }
  public int KillGold { get { return _goldTable.Data[Level.Lv - 1]; } }

  //
  // Consider
  //
  public PlayerNextExp   NextExp   { get { return _nextExp;   } }
  public PlayerLevel     Level     { get { return _level;     } }
  public PlayerGold      Gold      { get { return _gold;      } }
  public PlayerKillDeath KillDeath { get { return _killDeath; } }
  public PlayerCore      Core      { get { return _core;      } }

  public PlayerStatus     Status     { get { return _status;     } }
  public PlayerDamageSkin DamageSkin { get { return _damageSkin; } }

  // portal
  // respawner
  // automatic healer

  [Header("Unity/Photon Components")]
  [SerializeField] private PhotonView       _photonView;
  [SerializeField] private Transform        _trans;
  [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
  [SerializeField] private Rigidbody2D      _rigid;
  [SerializeField] private BoxCollider2D    _colliderCenter;
  [SerializeField] private BoxCollider2D    _colliderFoot;

  [Header("Observer")]
  [SerializeField] private BattlePlayerObserver _observer;

  [Header("Data")]
  [SerializeField] private DataTable _hpTable;
  [SerializeField] private Bar       _worldHpBar;

  // Consider
  [Header("Sync On Their Own")]
  [SerializeField] private PlayerNextExp   _nextExp;
  [SerializeField] private PlayerLevel     _level;
  [SerializeField] private PlayerGold      _gold;
  [SerializeField] private PlayerKillDeath _killDeath;
  [SerializeField] private PlayerCore      _core;

  [Header("Kill Reward")]
  [SerializeField] private DataTable _expTable;
  [SerializeField] private DataTable _goldTable;

  [Space(10)]
  [SerializeField] private PlayerStatus     _status;
  [SerializeField] private PlayerDamageSkin _damageSkin;

  private static readonly string _initState = "Idle";
}

