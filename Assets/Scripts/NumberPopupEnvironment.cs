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

          int damage = (int)args[1];
          bool isCritical = (bool)args[2];

          int skinId = ((GameObject)args[0]).GetComponent<IBattle>().DamageSkinId;

          var damageTaker = (IBattle)args[3];
          var damageTakerObj = ((MonoBehaviour)damageTaker).gameObject;

          if (damageTakerObj.tag == "Player" && damageTaker.PhotonView.owner == PhotonNetwork.player)
            Popup(NumberPopupType.Take, damage, damageTakerObj);
          else if (isCritical)
            Popup(NumberPopupType.Critical, damage, damageTakerObj, skinId);
          else
            Popup(NumberPopupType.Hit, damage, damageTakerObj, skinId);

          break;
        default:
          break;
      }
    }

    private void Popup(NumberPopupType popupType, int damage, GameObject damageTakerObj, int skinId = 0) {
      float posOffsetY = damageTakerObj.GetComponent<SpriteRenderer>().bounds.extents.y + 0.2f;

      // INFO: e.g. damage = 8351 -> "8351" -> ['8','3','5','1'] -> indices = [8, 3, 5, 1]
      var numbers = damage.ToString().ToCharArray().Select(x => Convert.ToInt32(x.ToString()));

      foreach (var prop in numbers.Select((number, i) => new { number, i })) {
        var numberObj = InstantiatePopupNumber(posOffsetY, damageTakerObj, prop.i);
        SetSprite(popupType, numberObj, prop.number, skinId, prop.i);
      }

      _existCount += numbers.Count();

      // To Avoid Overflow
      if (_existCount > 1000000000)
        _existCount = 0;
    }

    private GameObject InstantiatePopupNumber(float posOffsetY, GameObject damageTakerObj, int i) {
      var numberObj = Instantiate(_numberPref, damageTakerObj.transform.position, Quaternion.identity);
      numberObj.transform.Translate(i * 0.3f, posOffsetY, 0.0f);

      return numberObj;
    }

    private void SetSprite(NumberPopupType popupType, GameObject numberObj, int index, int skinId, int i) {
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
        default:
          break;
      }

      renderer.sortingOrder = i + _existCount;
    }

    [SerializeField] private GameObject _numberPref;
    [SerializeField] private AllSkinData _allSkinData;
    private static int _existCount = 0;
  }
}

