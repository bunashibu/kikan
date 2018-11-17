using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using UniRx;

namespace Bunashibu.Kikan {
  public class PlayerInitializer : SingletonMonoBehaviour<PlayerInitializer> {
    public void Initialize(Player player) {
      SetViewID(player);

      AllPlayerInitialize(player);

      if (player.PhotonView.isMine)
        PlayerOwnerInitialize(player);
      else
        NonPlayerOwnerInitialize(player);

      InitPlayerTeam(player);
      player.Core.Init(_corePanel);
      player.Movement.SetMoveForce(player.Status.Spd);
      player.Movement.SetJumpForce(player.Status.Jmp);
    }

    private void AllPlayerInitialize(Player player) {
      player.Hp.Cur
        .Where(cur => (cur <= 0))
        .Subscribe(_ => player.StateTransfer.TransitTo("Die", player.Animator))
        .AddTo(player.gameObject);

      player.Exp.Cur
        .Where(cur => (cur == player.Exp.Max.Value))
        .Subscribe(_ => player.Level.LevelUp())
        .AddTo(player.gameObject);

      player.Level.Cur
        .Subscribe(_ => player.Hp.Update(player.HpTable[player.Level.Cur.Value - 1]))
        .AddTo(player.gameObject);

      player.Level.Cur
        .Subscribe(_ => player.Exp.Update(player.ExpTable[player.Level.Cur.Value - 1]))
        .AddTo(player.gameObject);

      player.Level.Cur
        .Subscribe(_ => _kdPanel.UpdateLevel(player.Level.Cur.Value, player.PhotonView.owner))
        .AddTo(player.gameObject);

      player.KillCount
        .Subscribe(killCount => _kdPanel.UpdateKill(killCount, player.PhotonView.owner))
        .AddTo(_kdPanel.gameObject);

      player.DeathCount
        .Subscribe(deathCount => _kdPanel.UpdateDeath(deathCount, player.PhotonView.owner))
        .AddTo(_kdPanel.gameObject);

      player.DebuffState.StateDictionary
        .ObserveReplace()
        .Subscribe(x => Debug.Log(x))
        .AddTo(player.gameObject);
    }

    private void PlayerOwnerInitialize(Player player) {
      Assert.IsNotNull(_instantiator.HpBar);
      Assert.IsNotNull(_instantiator.ExpBar);
      Assert.IsNotNull(_instantiator.LvPanel);

      player.Hp.Cur
        .Subscribe(_ => _instantiator.HpBar.UpdateView(player.Hp.Cur.Value, player.Hp.Max.Value))
        .AddTo(_instantiator.HpBar);

      player.Hp.Max
        .Subscribe(_ => _instantiator.HpBar.UpdateView(player.Hp.Cur.Value, player.Hp.Max.Value))
        .AddTo(_instantiator.HpBar);

      player.Exp.Cur
        .Subscribe(_ => _instantiator.ExpBar.UpdateView(player.Exp.Cur.Value, player.Exp.Max.Value))
        .AddTo(_instantiator.ExpBar);

      player.Exp.Max
        .Subscribe(_ => _instantiator.ExpBar.UpdateView(player.Exp.Cur.Value, player.Exp.Max.Value))
        .AddTo(_instantiator.ExpBar);

      player.Level.Cur
        .Subscribe(cur => _instantiator.LvPanel.UpdateView(cur))
        .AddTo(_instantiator.LvPanel);

      player.Gold.Cur
        .Subscribe(cur => _goldPanel.UpdateView(cur))
        .AddTo(_goldPanel);

      player.WorldHpBar.gameObject.SetActive(false);
      CameraInitializer.Instance.RegisterToTrackTarget(player.gameObject);
    }

    private void NonPlayerOwnerInitialize(Player player) {
      player.Hp.Cur
        .Subscribe(_ => player.WorldHpBar.UpdateView(player.Hp.Cur.Value, player.Hp.Max.Value))
        .AddTo(player.WorldHpBar);

      player.Hp.Max
        .Subscribe(_ => player.WorldHpBar.UpdateView(player.Hp.Cur.Value, player.Hp.Max.Value))
        .AddTo(player.WorldHpBar);

      //_audioListener.enabled = false;
    }

    private void SetViewID(Player player) {
      var viewID = player.PhotonView.viewID;

      var props = new Hashtable() {{"ViewID", viewID}};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    private void InitPlayerTeam(Player player) {
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

