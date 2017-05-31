using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreChart : MonoBehaviour {
  public void UpdateView(int level) {
    _image.sprite = _sprites[level];
  }

  [SerializeField] private Image _image;
  [SerializeField] private Sprite[] _sprites;
}

