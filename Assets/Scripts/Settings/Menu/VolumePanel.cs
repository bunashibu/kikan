using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class VolumePanel : MonoBehaviour {
    void Awake() {
      _slider.value = _big; // 0db
      _slider.onValueChanged.AddListener((volume) => UpdateVolume(volume));
    }

    private void UpdateVolume(float v) {
      float min, max;
      float ratio = 1.0f;

      if (v < _small) {
        min = _minDB;
        max = _mediumDB;
        ratio = 1.0f/_small * v;
      }
      else if (v < _big) {
        min = _mediumDB;
        max = 0;
        ratio = 1.0f/(_big-_small) * (v-_small);
      }
      else {
        min = 0;
        max = _maxDB;
        ratio = 1.0f/(1.0f-_big) * (v-_big);
      }

      _mixer.SetFloat("SE", Mathf.Lerp(min, max, ratio));
    }

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;
      /******************************
      // -80db(_minDB)    == 0.0f
      // -20db(_mediumDB) == 0.1f(_small)
      //   0db            == 0.3f(_big)
      //  10db(_maxDB)    == 1.0f
      *******************************/
    private static float _minDB = -80.0f;
    private static float _mediumDB = -20.0f;
    private static float _maxDB = 10.0f;

    private static float _small = 0.1f;
    private static float _big = 0.3f;
  }
}
