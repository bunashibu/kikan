﻿using System;
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
        .Subscribe(_ => {
          int maxHp = (int)(player.HpTable[player.Level.Cur.Value - 1] * (1 + player.Core.GetValue(CoreType.Hp) / 100.0));
          player.Hp.Update(maxHp);
        })
        .AddTo(player.gameObject);

      player.Level.Cur
        .Subscribe(_ => player.Exp.Update(player.ExpTable[player.Level.Cur.Value - 1]))
        .AddTo(player.gameObject);

      player.Level.Cur
        .SkipLatestValueOnSubscribe()
        .Subscribe(_ => player.Status.IncreaseAtk(player.Level.Cur.Value) )
        .AddTo(player.gameObject);

      player.Level.Cur
        .Subscribe(_ => _kdPanel.UpdateLevel(player.Level.Cur.Value, player.PhotonView.owner))
        .AddTo(player.gameObject);

      player.Core.State.Level[CoreType.Hp].Cur
        .Subscribe(_ => {
          int maxHp = (int)(player.HpTable[player.Level.Cur.Value - 1] * (1 + player.Core.GetValue(CoreType.Hp) / 100.0));
          player.Hp.Update(maxHp);
        })
        .AddTo(player.gameObject);

      player.KillCount
        .Subscribe(killCount => _kdPanel.UpdateKill(killCount, player.PhotonView.owner))
        .AddTo(_kdPanel.gameObject);

      player.DeathCount
        .Subscribe(deathCount => _kdPanel.UpdateDeath(deathCount, player.PhotonView.owner))
        .AddTo(_kdPanel.gameObject);

      player.Debuff.State
        .ObserveReplace()
        .Where(state => state.NewValue)
        .Subscribe(state => player.Debuff.Instantiate(state.Key))
        .AddTo(player.gameObject);

      player.Debuff.State
        .ObserveReplace()
        .Where(state => !state.NewValue)
        .Subscribe(state => player.Debuff.Destroy(state.Key))
        .AddTo(player.gameObject);

      player.Stream.OnCoreLevelUpped
        .Subscribe(type => player.Gold.Subtract(player.Core.RequiredGold(type)) )
        .AddTo(player.gameObject);

      player.Stream.OnCoreLevelUpped
        .Subscribe(type => player.Core.LevelUp(type) )
        .AddTo(player.gameObject);

      player.Stream.OnCoreLevelUpped
        .Subscribe(type => player.Core.Instantiate(type, player.transform) )
        .AddTo(player.gameObject);

      player.Stream.OnAutoHealed
        .Subscribe(quantity => player.Hp.Add(quantity))
        .AddTo(player.gameObject);

      player.Stream.OnRespawned
        .Subscribe(viewID => {
          var pos = StageReference.Instance.StageData.RespawnPosition;
          if (player.PlayerInfo.Team == 1)
            pos.x *= -1;

          player.transform.position = pos;
          player.BodyCollider.enabled = true;
          player.Hp.FullRecover();
          //player.BuffState.Reset();

          player.StateTransfer.TransitTo("Idle", player.Animator);
        })
        .AddTo(player.gameObject);

      InitNameBackground(player);
      InitSpriteFlip(player);
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

      player.Stream.OnCoreLevelUpped
        .Subscribe(type => _corePanel.UpdateView(type, player.Core.CurLevel(type)) )
        .AddTo(_corePanel);

      Observable
        .Interval(TimeSpan.FromMilliseconds(1000))
        .Where(_ => player.Hp.Cur.Value > 0)
        .Where(_ => player.Hp.Cur.Value < player.Hp.Max.Value)
        .Subscribe(_ => player.Synchronizer.SyncAutoHeal(player.AutoHealQuantity) )
        .AddTo(player.gameObject);

      BattleStream.OnKilledAndDied
        .Subscribe(playerList => {
          var killPlayer = playerList[0];
          var deathPlayer = playerList[1];
          bool isSameTeam = ((int)PhotonNetwork.player.CustomProperties["Team"] == killPlayer.PlayerInfo.Team) ? true : false;

          _killMessagePanel.InstantiateMessage(killPlayer, deathPlayer, isSameTeam);
        })
        .AddTo(player.gameObject);

      player.Stream.OnDied
        .Subscribe(_ => {
          var respawnPanel = Instantiate(_respawnPanel, _canvas.transform).GetComponent<RespawnPanel>();
          respawnPanel.SetRespawnTime(player.Level.Cur.Value);
          respawnPanel.SetRespawnPlayer(player);
        })
        .AddTo(player.gameObject);

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

      int ownerTeam = (int)PhotonNetwork.player.CustomProperties["Team"];

      if (player.PlayerInfo.Team == ownerTeam)
        _teammateHpPanel.Register(player);

      player.AudioEnvironment.DisableListener();
    }

    private void SetViewID(Player player) {
      var props = new Hashtable() {{ "ViewID", player.PhotonView.viewID }};
      PhotonNetwork.player.SetCustomProperties(props);
    }

    private void InitNameBackground(Player player) {
      if (player.PlayerInfo.Team == 0)
        player.NameBackground.SetColor("RED");
      else if (player.PlayerInfo.Team == 1)
        player.NameBackground.SetColor("BLUE");
    }

    private void InitSpriteFlip(Player player) {
      foreach (var sprite in player.Renderers) {
        if (player.PlayerInfo.Team == 0)
          sprite.flipX = false;
        else if (player.PlayerInfo.Team == 1)
          sprite.flipX = true;
      }
    }

    [SerializeField] private StartUpInstantiator _instantiator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private KillDeathPanel _kdPanel;
    [SerializeField] private CorePanel _corePanel;
    [SerializeField] private GoldPanel _goldPanel;
    [SerializeField] private TeammateHpPanel _teammateHpPanel;
    [SerializeField] private KillMessagePanel _killMessagePanel;
    [SerializeField] private GameObject _respawnPanel;
  }
}

