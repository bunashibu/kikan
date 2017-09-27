using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Bunashibu.Kikan {
  public class TimeLimit : MonoBehaviour {
    void Start() {
      GetComponent<Text>().text = ((int)time).ToString();
    }

    void Update() {
      time -= Time.deltaTime;
      if (time < 0)
        _sceneChanger.ChangeScene("final_battle");
      GetComponent<Text>().text = ((int)time).ToString();
    }

    private float time = 6000;
    [SerializeField] SceneChanger _sceneChanger;
  }
}

