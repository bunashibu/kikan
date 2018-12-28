using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class NumberPopupEnvironment : SingletonMonoBehaviour<NumberPopupEnvironment> {
    public void PopupDamage(int damage, bool isCritical, int skinId, IPhotonBehaviour damageTaker) {
      bool isPhotonOwner = damageTaker.PhotonView.owner == PhotonNetwork.player;

      if (damageTaker is Player && isPhotonOwner)
        PopupNumberByType(NumberPopupType.Take, damage, damageTaker.gameObject);
      else if (isCritical)
        PopupNumberByType(NumberPopupType.Critical, damage, damageTaker.gameObject, skinId);
      else
        PopupNumberByType(NumberPopupType.Hit, damage, damageTaker.gameObject, skinId);
    }

    public void PopupHeal(int quantity, IPhotonBehaviour target) {
      PopupNumberByType(NumberPopupType.Heal, quantity, target.gameObject);
    }

    private void PopupNumberByType(NumberPopupType popupType, int damage, GameObject damageTakerObj, int skinId = 0) {
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

