using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(BarView))]
  public abstract class Bar : MonoBehaviour, IListener {
    void Start() {
      _view = GetComponent<BarView>();
    }

    public abstract void OnNotify(Notification notification, object[] args);

    protected BarView _view;
  }
}

