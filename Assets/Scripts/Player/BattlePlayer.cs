using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BattlePlayerSyncObserver))]
public class BattlePlayer : MonoBehaviour {
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

  public BattlePlayerSyncObserver SyncObserver { get { return _syncObserver; } }

  public BattlePlayerMovement Movement      { get; private set; }
  public PlayerState          State         { get; private set; }
  public PlayerHp             Hp            { get; private set; }
  public StateTransfer        StateTransfer { get; private set; }
  public SkillInfo            SkillInfo     { get; private set; }

  //  Data + Viewer
  // exp
  // gold
  // core
  // killdeath
  // level

  //  only Data
  // killreward
  // status
  // damageskin

  //
  // portal
  // respawner
  // automatic healer

  [SerializeField] private PhotonView       _photonView;
  [SerializeField] private Transform        _trans;
  [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
  [SerializeField] private Rigidbody2D      _rigid;
  [SerializeField] private BoxCollider2D    _colliderCenter;
  [SerializeField] private BoxCollider2D    _colliderFoot;

  [SerializeField] private BattlePlayerSyncObserver _syncObserver;

  [SerializeField] private DataTable _hpTable;
  [SerializeField] private Bar _worldHpBar;

  private static readonly string _initState = "Idle";
}

