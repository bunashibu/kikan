using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SyncStateEffect : MonoBehaviour {
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
      if (stream.isWriting) {
        stream.SendNext(_anim.GetBool("None"));
        stream.SendNext(_anim.GetBool("Stun"));
      } else {
        _anim.SetBool("None", (bool)stream.ReceiveNext());
        _anim.SetBool("Stun", (bool)stream.ReceiveNext());
      }
    }

    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Animator _anim;
  }
}

