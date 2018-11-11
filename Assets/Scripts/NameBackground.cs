using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class NameBackground : MonoBehaviour {
    void Awake() {
      ColorName = "DEFAULT";
    }

    public void SetColor(string name) {
      if (name == "RED") {
        ColorName = name;
        _image.color = Color.HSVToRGB(0.0f, 183.0f/ 255.0f, 1.0f);
      }
      else if (name == "BLUE") {
        ColorName = name;
        _image.color = Color.HSVToRGB(217.0f / 359.0f, 183.0f / 255.0f, 1.0f);
      }
      else
        Debug.Log("Color Error in NameBackground");
    }

    public string ColorName { get; private set; }

    [SerializeField] private Image _image;
  }
}

