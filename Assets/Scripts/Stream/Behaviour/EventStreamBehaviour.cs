using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class EventStreamBehaviour : SingletonMonoBehaviour<EventStreamBehaviour> {
    void Start() {
      EventStream.OnPlayerInitialized
        .Subscribe(player => {
          PlayerStreamBehaviour.Instance.Initialize(player);
        })
        .AddTo(gameObject);

      EventStream.OnClientPlayerDied
        .Where(_ => StageReference.Instance.StageData.Name == "FinalBattle")
        .Subscribe(_ => {
          CameraManager.Instance.ActivateWatching();
        })
        .AddTo(gameObject);
    }
  }
}
