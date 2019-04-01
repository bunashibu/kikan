using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Chair {
    public Chair(Player player) {
      _sprite = Resources.Load<Sprite>("Sprite/Character/character_sit");

      player.gameObject.UpdateAsObservable()
        .Where(_ => Input.GetKeyDown(KeyCode.Alpha0))
        .Where(_ => player.Animator.GetBool("Idle"))
        .Subscribe(_ => Sit(player.Renderers[0], player.Animator));

      player.gameObject.UpdateAsObservable()
        .Where(_ => Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        .Subscribe(_ => player.Animator.enabled = true);
    }

    private void Sit(SpriteRenderer renderer, Animator animator) {
      animator.enabled = false;
      renderer.sprite = _sprite;
    }

    private Sprite _sprite;
  }
}

