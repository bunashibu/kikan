using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(AudioSource))]
  public class MouseOverEvent : MonoBehaviour {
    void Awake() {
      _source = GetComponent<AudioSource>();
    }

    public void PlayMouseOverSE(AudioClip clip) {
      _source.PlayOneShot(clip, 0.5f);
    }

    private AudioSource _source;
  }
}

