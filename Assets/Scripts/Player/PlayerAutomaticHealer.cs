using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerAutomaticHealer : Photon.MonoBehaviour {
  void Awake() {
    HealQuantity = _healTable.Data[0];
  }

  void Update() {
    if (photonView.isMine) {
      bool isInjured = (_player.Hp.Cur < _player.Hp.Max);

      if (isInjured)
        AutomaticHeal();
    }
  }

  public void UpdateMaxHealQuantity() {
    Assert.IsTrue(photonView.isMine);

    double ratio = (double)(_core.Heal / 100.0);
    HealQuantity = (int)(_healTable.Data[_level.Lv - 1] * ratio);
  }

  private void AutomaticHeal() {
    if (_isActive) return;

    _isActive = true;

    MonoUtility.Instance.DelaySec(HealInterval, () => {
      _isActive = false;

      if (_player.Hp.IsDead) return;

      _player.Hp.Plus(HealQuantity);
      //_playerHp.UpdateView();
    });
  }

  [SerializeField] private BattlePlayer _player;
  [SerializeField] private DataTable _healTable;
  [SerializeField] private PlayerLevel _level;
  [SerializeField] private PlayerCore _core;
  public int HealQuantity { get; private set; }

  private bool _isActive = false;
  private static readonly float HealInterval = 2.0f;
}

