using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public interface IRewardTaker : IMediator {
    List<IRewardTaker> Teammates { get; }
  }
}

