using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class LevelPanel : MonoBehaviour {
    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.LevelUp:
          Assert.IsTrue(args.Length == 1);

          int level = (int)args[0];
          _text.text = "Lv " + level.ToString();

          break;
        default:
          break;
      }
    }

    [SerializeField] private Text _text;
  }
}

