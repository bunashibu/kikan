using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class StateChanger {
    public void ChangeTo(string state, float sec, GameObject target) {
      /*
      switch (state) {
        case "Rigor":
          var rigidState = target.GetComponent<RigidState>();
          rigidState.Rigor = true;
          rigidState.UpdateRigor();
  
          MonoUtility.Instance.DelaySec(sec, () => {
            rigidState.Rigor = false;
            rigidState.UpdateRigor();
          });
          break;
        default:
          Debug.Log("StateChanger-ChangeTo(string state <-this arg is wrong");
          break;
      }
      */
    }
  }
}

