using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayer : Photon.MonoBehaviour {
  void Awake() {
    Movement   = new LobbyPlayerMovement();
    StateTransfer = new StateTransfer(_initState);
  }

  public PhotonView       PhotonView   { get { return _photonView;   } }
  public SpriteRenderer[] Renderers    { get { return _renderers;    } }
  public Rigidbody2D      Rigid        { get { return _rigid;        } }
  public BoxCollider2D    ColliderFoot { get { return _colliderFoot; } }
  public RigidState       RigidState   { get { return _rigidState;   } }

  public LobbyPlayerMovement Movement      { get; private set; }
  public StateTransfer       StateTransfer { get; private set; }
  public SkillInfo           SkillInfo     { get; private set; }

  [SerializeField] private PhotonView       _photonView;
  [SerializeField] private SpriteRenderer[] _renderers;  // INFO: [PlayerSprite, WeaponSprite]
  [SerializeField] private Rigidbody2D      _rigid;
  [SerializeField] private BoxCollider2D    _colliderFoot;
  [SerializeField] private RigidState       _rigidState;
  private static readonly string _initState = "Idle";
}

