using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPlayerAnim : MonoBehaviour {
  void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    if (stream.isWriting) {
      stream.SendNext(_renderer.flipX);
      stream.SendNext(_anim.GetBool("Idle"));
      stream.SendNext(_anim.GetBool("Fall"));
      stream.SendNext(_anim.GetBool("Walk"));
      stream.SendNext(_anim.GetBool("Climb"));
      stream.SendNext(_anim.GetBool("LieDown"));
      stream.SendNext(_anim.GetBool("GroundJump"));
      stream.SendNext(_anim.GetBool("ClimbJump"));
      stream.SendNext(_anim.GetBool("Skill"));
      stream.SendNext(_anim.GetBool("Die"));
    } else {
      _renderer.flipX = (bool)stream.ReceiveNext();
      _anim.SetBool("Idle", (bool)stream.ReceiveNext());
      _anim.SetBool("Fall", (bool)stream.ReceiveNext());
      _anim.SetBool("Walk", (bool)stream.ReceiveNext());
      _anim.SetBool("Climb", (bool)stream.ReceiveNext());
      _anim.SetBool("LieDown", (bool)stream.ReceiveNext());
      _anim.SetBool("GroundJump", (bool)stream.ReceiveNext());
      _anim.SetBool("ClimbJump", (bool)stream.ReceiveNext());
      _anim.SetBool("Skill", (bool)stream.ReceiveNext());
      _anim.SetBool("Die", (bool)stream.ReceiveNext());
    }
  }

  [SerializeField] private SpriteRenderer _renderer;
  [SerializeField] private Animator _anim;
}

