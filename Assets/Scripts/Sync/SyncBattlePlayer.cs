using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class SyncBattlePlayer : MonoBehaviour {
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
      if (stream.isWriting) {
        stream.SendNext((Vector2)_player.Rigid.position);
        stream.SendNext((Vector2)_player.Rigid.velocity);

        stream.SendNext(_player.Renderers[0].flipX);
        stream.SendNext(_player.Animator.GetBool("Idle"       ));
        stream.SendNext(_player.Animator.GetBool("Fall"       ));
        stream.SendNext(_player.Animator.GetBool("Walk"       ));
        stream.SendNext(_player.Animator.GetBool("Ladder"     ));
        stream.SendNext(_player.Animator.GetBool("LieDown"    ));
        stream.SendNext(_player.Animator.GetBool("GroundJump" ));
        stream.SendNext(_player.Animator.GetBool("LadderJump" ));
        stream.SendNext(_player.Animator.GetBool("Skill"      ));
        stream.SendNext(_player.Animator.GetBool("Die"        ));
        stream.SendNext(_player.Animator.GetBool("Stun"       ));
      } else {
        _player.Rigid.position = (Vector2)stream.ReceiveNext();
        _player.Rigid.velocity = (Vector2)stream.ReceiveNext();

        /*
        float lag = Mathf.Abs((float) (PhotonNetwork.time - info.timestamp));
        _player.Rigid.position += _player.Rigid.velocity * lag;
        */

        _player.Renderers[0].flipX = (bool)stream.ReceiveNext();
        _player.Animator.SetBool("Idle"       , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("Fall"       , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("Walk"       , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("Ladder"     , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("LieDown"    , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("GroundJump" , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("LadderJump" , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("Skill"      , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("Die"        , (bool)stream.ReceiveNext());
        _player.Animator.SetBool("Stun"       , (bool)stream.ReceiveNext());
      }
    }

    [SerializeField] private BattlePlayer _player;
  }
}

