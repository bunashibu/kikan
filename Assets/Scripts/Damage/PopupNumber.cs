using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class PopupNumber : MonoBehaviour {
    public void CreateHit(int number, DamageSkin skin) {
      Create(number, skin, DamageType.Hit);
    }

    public void CreateCritical(int number, DamageSkin skin) {
      Create(number, skin, DamageType.Critical);
    }

    public void CreateTake(int number, DamageSkin skin) {
      Create(number, skin, DamageType.Take);
    }

    public void CreateHeal(int number, DamageSkin skin) {
      Create(number, skin, DamageType.Heal);
    }

    private void Create(int number, DamageSkin skin, DamageType type) {
      Number = number;

      // INFO: e.g. number = 8351 -> "8351" -> ['8','3','5','1'] -> indices = [8, 3, 5, 1]
      string str = number.ToString();
      var indices = str.ToCharArray().Select(x => Convert.ToInt32(x.ToString()));

      int i = 0;
      foreach(int index in indices) {
        var digit = Instantiate(_digitPref, gameObject.transform, false);
        digit.transform.Translate(i * 0.3f, 1.0f, 0.0f);
        ++i;

        var renderer = digit.GetComponent<SpriteRenderer>();

        switch (type) {
          case DamageType.Hit:
            renderer.sprite = skin.Hit[index];
            break;
          case DamageType.Critical:
            renderer.sprite = skin.Critical[index];
            break;
          case DamageType.Take:
            renderer.sprite = skin.Take[index];
            break;
          case DamageType.Heal:
            renderer.sprite = skin.Heal[index];
            break;
        }

        renderer.sortingOrder = i;
      }
    }

    public int Number { get; private set; }
    [SerializeField] private GameObject _digitPref;
  }
}

