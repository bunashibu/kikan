using UnityEngine;
using System.Collections;

public class BattleSceneManager : MonoBehaviour {
  void Awake() {
    if (instance == null)
      instance = this;
    else if (instance != this)
      Destroy(gameObject);
  }

  public static BattleSceneManager instance = null;
  public GameObject hud;
}
