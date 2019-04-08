using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Bunashibu.Kikan {
  public class RespawnPanel : MonoBehaviour {
    void Update() {
      _restTimeText.text = ((int)_restTime + 1).ToString() + "秒後に復活します。";
      _bar.UpdateView((int)(_restTime * 1000), _respawnTime * 1000);
      _restTime -= Time.deltaTime;

      if (_restTime < 0) {
        _respawnPlayer.Synchronizer.SyncRespawn(_respawnPlayer.PhotonView.viewID);
        Destroy(gameObject);
      }

      if (StageReference.Instance.StageData.Name == "FinalBattle")
        Destroy(gameObject);
    }

    public void SetRespawnTime(int time) {
      _respawnTime = time;
      _restTime = time;
    }

    public void SetRespawnPlayer(Player player) {
      _respawnPlayer = player;
    }

    [SerializeField] private Text _restTimeText;
    [SerializeField] private Bar _bar;
    private Player _respawnPlayer;
    private int _respawnTime;
    private float _restTime;
  }
}

