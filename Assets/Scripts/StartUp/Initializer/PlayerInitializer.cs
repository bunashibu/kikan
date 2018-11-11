using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UniRx;

namespace Bunashibu.Kikan {
  public class PlayerInitializer : SingletonMonoBehaviour<PlayerInitializer> {
    public void Initialize(BattlePlayer player) {
      SetViewID(player);

      player.KillCount.Subscribe(killCount => _kdPanel.UpdateKill(killCount, player.PhotonView.owner));
      player.DeathCount.Subscribe(deathCount => _kdPanel.UpdateDeath(deathCount, player.PhotonView.owner));

      InitPlayerTeam(player);
      player.Core.Init(_corePanel);
      player.Movement.SetMoveForce(player.Status.Spd);
      player.Movement.SetJumpForce(player.Status.Jmp);

      if (player.PhotonView.isMine) {
        Assert.IsNotNull(_instantiator.HpBar);
        Assert.IsNotNull(_instantiator.ExpBar);
        Assert.IsNotNull(_instantiator.LvPanel);

        player.Hp.AddListener(_instantiator.HpBar.OnNotify);
        player.Exp.AddListener(_instantiator.ExpBar.OnNotify);
        player.Level.AddListener(_instantiator.LvPanel.OnNotify);
        player.Gold.AddListener(_goldPanel.OnNotify);
      }

      var notifier = new Notifier(player.OnNotify);
      notifier.Notify(Notification.PlayerInstantiated);
    }

    private void SetViewID(BattlePlayer player) {
      var viewID = player.PhotonView.viewID;

      var props = new Hashtable() {{"ViewID", viewID}};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    private void InitPlayerTeam(BattlePlayer player) {
      int team = (int)PhotonNetwork.player.CustomProperties["Team"];

      if (team == 0)
        player.NameBackground.SetColor("RED");
      else if (team == 1)
        player.NameBackground.SetColor("BLUE");

      player.Observer.SyncNameBackground();

      foreach (var sprite in player.Renderers) {
        if (team == 0)
          sprite.flipX = false;
        else if (team == 1)
          sprite.flipX = true;
      }
    }

    [SerializeField] private StartUpInstantiator _instantiator;
    [SerializeField] private KillDeathPanel _kdPanel;
    [SerializeField] private CorePanel _corePanel;
    [SerializeField] private GoldPanel _goldPanel;
  }
}

