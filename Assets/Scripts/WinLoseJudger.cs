using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class WinLoseJudger : MonoBehaviour {
    void Update() {
      switch (Judge()) {
        case Result.WIN:  ShowWin();  break;
        case Result.LOSE: ShowLose(); break;
        case Result.DRAW: ShowDraw(); break;
      }
    }

    private Result Judge() {
      return Result.WIN;
    }

    private void ShowWin() {
    }

    private void ShowLose() {
    }

    private void ShowDraw() {
    }

    private enum Result {
      WIN,
      LOSE,
      DRAW
    }
  }
}

