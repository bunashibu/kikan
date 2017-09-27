using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SyncEnemy : MonoBehaviour {
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
      if (stream.isWriting) {
        stream.SendNext(_renderer.flipX);
        stream.SendNext(_anim.GetBool("Idle"));
        stream.SendNext(_anim.GetBool("Die"));
      } else {
        _renderer.flipX = (bool)stream.ReceiveNext();
        _anim.SetBool("Idle", (bool)stream.ReceiveNext());
        _anim.SetBool("Die", (bool)stream.ReceiveNext());
      }
    }
  
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _anim;
  }
}

