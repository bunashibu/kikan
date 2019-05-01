using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SyncPlayer : MonoBehaviour {
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
      if (stream.isWriting) {
        stream.SendNext((Vector2)_player.Rigid.position);
        stream.SendNext((Vector2)_player.Rigid.velocity);

        stream.SendNext(_player.Renderers[0].flipX);
      }
      else {
        _player.Rigid.position = (Vector2)stream.ReceiveNext();
        _player.Rigid.velocity = (Vector2)stream.ReceiveNext();

        /*
        float lag = Mathf.Abs((float) (PhotonNetwork.time - info.timestamp));
        _player.Rigid.position += _player.Rigid.velocity * lag;
        */

        _player.Renderers[0].flipX = (bool)stream.ReceiveNext();
      }
    }

    [SerializeField] private Player _player;
  }
}

