using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class NumberPopupEnvironment : Photon.MonoBehaviour {
    public void Popup(int damage, bool isCritical, int skinId, PopupType popupType) {
      switch (popupType) {
        case PopupType.Player:
          photonView.RPC("SyncPlayerPopup", PhotonTargets.All, damage, isCritical, skinId);
          break;
        case PopupType.Enemy:
          photonView.RPC("SyncEnemyPopup", PhotonTargets.All, damage, isCritical, skinId);
          break;
      }
    }

    [PunRPC]
    private void SyncPlayerPopup(int damage, bool isCritical, int skinId) {
      if (photonView.owner == PhotonNetwork.player) {
        Popup(damage, skinId, DamageType.Take);
        return;
      }

      if (isCritical)
        Popup(damage, skinId, DamageType.Critical);
      else
        Popup(damage, skinId, DamageType.Hit);
    }

    [PunRPC]
    private void SyncEnemyPopup(int damage, bool isCritical, int skinId) {
      if (isCritical)
        Popup(damage, skinId, DamageType.Critical);
      else
        Popup(damage, skinId, DamageType.Hit);
    }

    private void Popup(int number, int skinId, DamageType type) {
      // INFO: e.g. number = 8351 -> "8351" -> ['8','3','5','1'] -> indices = [8, 3, 5, 1]
      var indices = number.ToString().ToCharArray().Select(x => Convert.ToInt32(x.ToString()));

      int i = 0;
      foreach(int index in indices) {
        var numberObj = Instantiate(_numberPref, transform.position, Quaternion.identity);
        numberObj.transform.Translate(i * 0.3f, 1.0f, 0.0f);
        ++i;

        var renderer = numberObj.GetComponent<SpriteRenderer>();

        switch (type) {
          case DamageType.Hit:
            renderer.sprite = _allSkinData.GetSkin(skinId).Hit[index];
            break;
          case DamageType.Critical:
            renderer.sprite = _allSkinData.GetSkin(skinId).Critical[index];
            break;
          case DamageType.Take:
            renderer.sprite = _allSkinData.GetSkin(skinId).Take[index];
            break;
          case DamageType.Heal:
            renderer.sprite = _allSkinData.GetSkin(skinId).Heal[index];
            break;
        }

        renderer.sortingOrder = i + _existCount;
      }

      _existCount += indices.Count();

      // To Avoid Overflow
      if (_existCount > 1000000000)
        _existCount = 0;
    }

    [SerializeField] private GameObject _numberPref;
    [SerializeField] private AllSkinData _allSkinData;
    private int _existCount = 0;
  }
}

