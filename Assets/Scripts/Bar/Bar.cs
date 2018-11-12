using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(BarView))]
  public class Bar : MonoBehaviour {
    void Awake() {
      _view = GetComponent<BarView>();
    }

    public void UpdateView(int cur, int max) {
      _view.UpdateView(cur, max);
    }

    protected BarView _view;
  }
}

