using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IResponder : IListener {
    Notifier Notifier { get; }
  }
}

