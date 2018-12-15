using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Bunashibu.Kikan {
  public class AutomaticHideBarView : BarView {
    void Awake() {
      base.Awake();

      gameObject.SetActive(false);
      _id = gameObject.GetInstanceID().ToString();
    }

    public override void UpdateView(int cur, int max) {
      gameObject.SetActive(true);
      Animate(cur, max);

      MonoUtility.Instance.OverwritableDelaySec(_displayTimeSec, "HideBar" + _id, () => {
        gameObject.SetActive(false);
      });
    }

    void OnDestroy() {
      if (MonoUtility.Instance != null)
        MonoUtility.Instance.Stop("HideBar" + _id);
    }

    [SerializeField] private float _displayTimeSec;
    private string _id;
  }
}

