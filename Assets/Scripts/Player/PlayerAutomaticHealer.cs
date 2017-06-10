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
      bool isInjured = (_playerHp.Cur < _playerHp.Max);

      if (_isReady && isInjured)
        AutomaticHeal();
    }
  }

  public void UpdateMaxHealQuantity() {
    Assert.IsTrue(photonView.isMine);

    HealQuantity = _healTable.Data[_level.Lv - 1];
  }

  private void AutomaticHeal() {
    _isReady = false;

    MonoUtility.Instance.DelaySec(HealInterval, () => {
      _playerHp.Plus(HealQuantity);
      _playerHp.UpdateView();
      _isReady = true;
    });
  }

  [SerializeField] private DataTable _healTable;
  [SerializeField] private PlayerHp _playerHp;
  [SerializeField] private PlayerLevel _level;
  public int HealQuantity { get; private set; }

  private bool _isReady = true; // Rename
  private static readonly float HealInterval = 2.0f;
}

