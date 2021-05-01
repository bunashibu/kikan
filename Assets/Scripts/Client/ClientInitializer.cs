using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class ClientInitializer : MonoBehaviour {
    void Awake() {
      Client.Teammates.Clear();
      Client.Opponents.Clear();
    }
  }
}
