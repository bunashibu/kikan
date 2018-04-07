using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class PopupRemark : MonoBehaviour {
    void Start() {
      gameObject.SetActive(false);
    }

    public void Set(string message, int viewID) {
      gameObject.SetActive(true);
      _text.text = message;

      MonoUtility.Instance.OverwritableDelaySec(5.0f, "Remark" + viewID.ToString(), () => {
        _text.text = "";
        gameObject.SetActive(false);
      });
    }

    [SerializeField] private Text _text;
  }
}

