using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncWeapon : MonoBehaviour {
  void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
    if (stream.isWriting) {
      stream.SendNext(_renderer.flipX);
      stream.SendNext(_anim.GetBool("X"));
      stream.SendNext(_anim.GetBool("Shift"));
      stream.SendNext(_anim.GetBool("Z"));
    } else {
      _renderer.flipX = (bool)stream.ReceiveNext();
      _anim.SetBool("X", (bool)stream.ReceiveNext());
      _anim.SetBool("Shift", (bool)stream.ReceiveNext());
      _anim.SetBool("Z", (bool)stream.ReceiveNext());
    }
  }

  [SerializeField] private SpriteRenderer _renderer;
  [SerializeField] private Animator _anim;
}

