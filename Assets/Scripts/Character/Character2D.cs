using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Bunashibu.Kikan {
  public class Character2D {
    public Character2D(ICharacter character) {
      _character = character;
      _character.gameObject.UpdateAsObservable()
        .Subscribe(_ => Update() );
    }

    private void Update() {
      if (_character.PhotonView.isMine && IsOutOfArea() && _isEnabled)
        AdjustPosition();
    }

    public void Enable() {
      _isEnabled = true;
    }

    public void Disable() {
      _isEnabled = false;
    }

    private bool IsOutOfArea() {
      var x = _character.transform.position.x;
      var y = _character.transform.position.y;

      if (x < StageReference.Instance.StageData.StageRect.xMin || StageReference.Instance.StageData.StageRect.xMax < x ||
          y < StageReference.Instance.StageData.StageRect.yMin || StageReference.Instance.StageData.StageRect.yMax < y)
        return true;

      return false;
    }

    private void AdjustPosition() {
      _character.transform.position = StageReference.Instance.StageData.ResetPosition;
      _character.FootCollider.isTrigger = false;
    }

    private ICharacter _character;
    private bool _isEnabled = true;
  }
}

