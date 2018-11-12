using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  // PLAN: To be a concrete class
  [RequireComponent(typeof(BarView))]
  public abstract class Bar : MonoBehaviour {
    void Awake() {
      _view = GetComponent<BarView>();
    }

    public void UpdateView(int cur, int max) {
      _view.UpdateView(cur, max);
    }

    // Obsolete
    public abstract void OnNotify(Notification notification, object[] args);

    protected BarView _view;
  }
}

