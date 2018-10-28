using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class GoldPanel : MonoBehaviour {
    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.GoldUpdated:
          Assert.IsTrue(args.Length == 1);

          int gold = (int)args[0];
          _text.text = gold.ToString();

          break;
        default:
          break;
      }
    }

    [SerializeField] private Text _text;
  }
}

