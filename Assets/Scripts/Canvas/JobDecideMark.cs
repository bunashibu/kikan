using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class JobDecideMark : MonoBehaviour {
    void Awake() {
      foreach (var image in _images)
        image.enabled = false;
    }

    public void Put() {
      if (_index < _images.Length)
        _images[_index].enabled = true;

      _index += 1;
    }

    [SerializeField] private Image[] _images;
    private int _index = 0;
  }
}

