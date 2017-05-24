using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DamagePanel : MonoBehaviour {
  public void Create(int damage, DamageSkin skin) {
    Value = damage;

    string str = damage.ToString();
    var indices = str.ToCharArray().Select(x => Convert.ToInt32(x.ToString()));

    int i = 0;
    foreach(int index in indices) {
      var number = Instantiate(_numberPref, gameObject.transform, false);
      number.transform.Translate(i * 0.3f, 1.0f, 0.0f);
      ++i;

      var renderer = number.GetComponent<SpriteRenderer>();
      renderer.sprite = skin.Hit[index];
      renderer.sortingOrder = i;
    }
  }

  public int Value { get; private set; }
  [SerializeField] private GameObject _numberPref;
}

