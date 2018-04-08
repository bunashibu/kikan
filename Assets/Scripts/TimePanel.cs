using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class TimePanel : MonoBehaviour {
    void Update() {
      _timeSec -= Time.deltaTime;
      if (_timeSec < 0) {
        Debug.Log("Go to Final Battle");
      }

      UpdateTimePanel();
    }

    private void UpdateTimePanel() {
      var minSec = ConvertToMinSec((int)_timeSec);
      int min = minSec[0];
      int sec = minSec[1];

      string padding = "";
      if (sec < 10)
        padding = "0";

      _text.text = min.ToString() + ":" + padding + sec.ToString();
    }

    private List<int> ConvertToMinSec(int timeSec) {
      int min = timeSec / 60;
      int sec = timeSec % 60;

      return new List<int>() { min, sec };
    }

    [SerializeField] private Text _text;
    [SerializeField] private float _timeSec;
  }
}

