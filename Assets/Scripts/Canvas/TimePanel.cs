using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class TimePanel : MonoBehaviour {
    void Awake() {
      _type = TimeViewType.MinSec; // default type
    }

    void Update() {
      if (_time <= 0)
        return;

      _time -= Time.deltaTime;
      UpdateTimePanel();
    }

    public int TimeSec => (int)_time;

    public void SetTime(float time) {
      _time = time;
    }

    public void SetView(TimeViewType type) {
      _type = type;
    }

    public void UpdateTimePanel() {
      switch (_type) {
        case TimeViewType.MinSec:
          MinSecView();
          break;
        case TimeViewType.Sec:
          SecView();
          break;
        default:
          break;
      }
    }

    private List<int> ConvertToMinSec(int timeSec) {
      int min = timeSec / 60;
      int sec = timeSec % 60;

      return new List<int>() { min, sec };
    }

    private void MinSecView() {
      var minSec = ConvertToMinSec((int)_time);
      int min = minSec[0];
      int sec = minSec[1];

      string padding = "";
      if (sec < 10)
        padding = "0";

      _text.text = min.ToString() + ":" + padding + sec.ToString();
    }

    private void SecView() {
      _text.text = ((int)_time).ToString();
    }

    [SerializeField] private Text _text;
    private float _time;
    private TimeViewType _type;
  }

  public enum TimeViewType {
    MinSec,
    Sec
  }
}

