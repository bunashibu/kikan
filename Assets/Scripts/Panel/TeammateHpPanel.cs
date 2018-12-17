using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Bunashibu.Kikan {
  public class TeammateHpPanel : MonoBehaviour {
    void Awake() {
      _rectTrans = GetComponent<RectTransform>();
      gameObject.SetActive(false);
    }

    public void Register(Player player) {
      gameObject.SetActive(true);
      _count += 1;

      var member = Instantiate(_memberPref, transform).GetComponent<TeammateHpMemberPanel>(); ;

      member.transform.position = member.transform.position + new Vector3(0, -20 * _count, 0);
      _rectTrans.sizeDelta = _rectTrans.sizeDelta + new Vector2(0, 20);

      member.SetName(player.PlayerInfo.Name);

      player.Hp.Cur
        .Subscribe(_ => member.HpBar.UpdateView(player.Hp.Cur.Value, player.Hp.Max.Value))
        .AddTo(player.gameObject);

      player.Hp.Max
        .Subscribe(_ => member.HpBar.UpdateView(player.Hp.Cur.Value, player.Hp.Max.Value))
        .AddTo(player.gameObject);
    }

    [SerializeField] private GameObject _memberPref;
    private RectTransform _rectTrans;
    private int _count = 0;
  }
}

