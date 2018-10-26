using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace Bunashibu.Kikan {
  public class NumberPopupEnvironment : SingletonMonoBehaviour<NumberPopupEnvironment> {
    public void OnNotify(Notification notification, object[] args) {
      switch (notification) {
        case Notification.TakeDamage:
          Assert.IsTrue(args.Length == 4);

          var skinId = ((IBattle)args[0]).DamageSkinId;
          int damage = (int)args[1];
          bool isCritical = (bool)args[2];

          var taker = (IBattle)args[3];
          var takerObj = ((MonoBehaviour)taker).gameObject;
          bool isTakerMe = (takerObj.tag == "Player") && (taker.PhotonView.owner == PhotonNetwork.player);

          if (isTakerMe)
            Popup(NumberPopupType.Take, damage, takerObj);
          else if (isCritical)
            Popup(NumberPopupType.Critical, damage, takerObj, skinId);
          else
            Popup(NumberPopupType.Hit, damage, takerObj, skinId);

          break;
        default:
          break;
      }
    }

    public void Popup(int damage, bool isCritical, int skinId, PopupType popupType) {
      /*
      switch (popupType) {
        case PopupType.Player:
          photonView.RPC("SyncPlayerPopup", PhotonTargets.All, damage, isCritical, skinId);
          break;
        case PopupType.Enemy:
          photonView.RPC("SyncEnemyPopup", PhotonTargets.All, damage, isCritical, skinId);
          break;
      }
      */
    }

    [PunRPC]
    private void SyncPlayerPopup(int damage, bool isCritical, int skinId) {
      /*
      if (photonView.owner == PhotonNetwork.player) {
        Popup(damage, skinId, DamageType.Take);
        return;
      }

      if (isCritical)
        Popup(damage, skinId, DamageType.Critical);
      else
        Popup(damage, skinId, DamageType.Hit);
        */
    }

    [PunRPC]
    private void SyncEnemyPopup(int damage, bool isCritical, int skinId) {
      /*
      if (isCritical)
        Popup(damage, skinId, DamageType.Critical);
      else
        Popup(damage, skinId, DamageType.Hit);
        */
    }

    //private void Popup(int number, int skinId, DamageType type) {
    private void Popup(NumberPopupType popupType, int number, GameObject takerObj, int skinId = 0) {
      // INFO: e.g. number = 8351 -> "8351" -> ['8','3','5','1'] -> indices = [8, 3, 5, 1]
      var indices = number.ToString().ToCharArray().Select(x => Convert.ToInt32(x.ToString()));

      var posOffsetY = takerObj.GetComponent<SpriteRenderer>().bounds.extents.y + 0.2f;
      int i = 0;
      foreach(int index in indices) {
        var numberObj = Instantiate(_numberPref, takerObj.transform.position, Quaternion.identity);
        numberObj.transform.Translate(i * 0.3f, posOffsetY, 0.0f);
        ++i;

        var renderer = numberObj.GetComponent<SpriteRenderer>();

        switch (popupType) {
          case NumberPopupType.Hit:
            renderer.sprite = _allSkinData.GetSkin(skinId).Hit[index];
            break;
          case NumberPopupType.Critical:
            renderer.sprite = _allSkinData.GetSkin(skinId).Critical[index];
            break;
          case NumberPopupType.Take:
            renderer.sprite = _allSkinData.GetSkin(skinId).Take[index];
            break;
          case NumberPopupType.Heal:
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
    private static int _existCount = 0;
  }
}

