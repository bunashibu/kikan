using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class NumberPopupEnvironment : Photon.MonoBehaviour {
    public void Popup(int damage, bool isCritical, int skinId) {
      photonView.RPC("SyncPopup", PhotonTargets.All, damage, isCritical, skinId);
    }

    [PunRPC]
    private void SyncPopup(int damage, bool isCritical, int skinId) {
      if (photonView.owner == PhotonNetwork.player) {
        Popup(damage, skinId, DamageType.Take);
        return;
      }

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
        var digit = Instantiate(_digitPref, gameObject.transform, false);
        digit.transform.Translate(i * 0.3f, 1.0f, 0.0f);
        ++i;

        var renderer = digit.GetComponent<SpriteRenderer>();

        switch (type) {
          case DamageType.Hit:
            renderer.sprite = _skinData.GetSkin(skinId).Hit[index];
            break;
          case DamageType.Critical:
            renderer.sprite = _skinData.GetSkin(skinId).Critical[index];
            break;
          case DamageType.Take:
            renderer.sprite = _skinData.GetSkin(skinId).Take[index];
            break;
          case DamageType.Heal:
            renderer.sprite = _skinData.GetSkin(skinId).Heal[index];
            break;
        }

        renderer.sortingOrder = i;
      }
    }

    [SerializeField] private GameObject _digitPref;
    [SerializeField] private AllSkinData _skinData;
  }
}

