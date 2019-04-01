using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Chair {
    public Chair(Player player) {
      _sprite = Resources.Load<Sprite>("Sprite/Character/character_sit");

      if (player.PhotonView.isMine) {
        player.gameObject.UpdateAsObservable()
          .Where(_ => Input.GetKeyDown(KeyCode.Alpha0))
          .Where(_ => player.Animator.GetBool("Idle"))
          .Subscribe(_ => player.Synchronizer.SyncChair(true) );

        player.gameObject.UpdateAsObservable()
          .Where(_ => Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
          .Subscribe(_ => player.Synchronizer.SyncChair(false) );
      }

      player.gameObject.UpdateAsObservable()
        .Where(_ => ShouldSit)
        .Subscribe(_ => Sit(player.Renderers[0], player.Animator));

      player.gameObject.UpdateAsObservable()
        .Where(_ => !ShouldSit)
        .Subscribe(_ => player.Animator.enabled = true);
    }

    public void UpdateShouldSit(bool shouldSit) {
      ShouldSit = shouldSit;
    }

    private void Sit(SpriteRenderer renderer, Animator animator) {
      animator.enabled = false;
      renderer.sprite = _sprite;
    }

    public bool ShouldSit { get; private set;}

    private Sprite _sprite;
  }
}

