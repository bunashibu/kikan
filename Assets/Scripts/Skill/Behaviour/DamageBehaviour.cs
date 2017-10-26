using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class DamageBehaviour {
    public void SetSkillUser(GameObject skillUser) {
      _skillUser = skillUser;
    }

    public void DamageToTarget(int power, int maxDeviation, IBattle target) {
      CalcDamage(power, maxDeviation);

      target.Hp.Subtract(Damage);
      target.Observer.SyncCurHp();

      target.Hp.UpdateView();
      target.Observer.SyncUpdateHpView();
    }

    private void CalcDamage(int power, int maxDeviation) {
      int atkPower = (int)(_skillUser.GetComponent<PlayerStatus>().Atk * power / 100.0);
      double ratio = (double)((_skillUser.GetComponent<PlayerCore>().Attack + 100) / 100.0);
      int deviation = (int)((Random.value - 0.5) * 2 * maxDeviation);

      Damage = (int)(atkPower * ratio) + deviation;

      Critical = CriticalCheck();
      if (Critical)
        Damage *= 2;
    }

    private bool CriticalCheck() {
      int probability = _skillUser.GetComponent<PlayerCore>().Critical;
      int threshold = (int)(Random.value * 99);

      if (probability > threshold)
        return true;

      return false;
    }

    private GameObject _skillUser;
    public int Damage { get; private set; }
    public bool Critical { get; private set; }
  }
}

