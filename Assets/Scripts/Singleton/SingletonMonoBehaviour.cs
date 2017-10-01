using UnityEngine;
using System.Collections;

namespace Bunashibu.Kikan {
  public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    protected virtual void Awake() {
      if (this != Instance) {
        Debug.LogError("Error: Exsit multiple instances(Singleton class)", this);
        Destroy(this);
      }
    }

    public static T Instance {
      get {
        if (instance == null)
          instance = FindObjectOfType(typeof(T)) as T;

        return instance;
      }
    }

    private static T instance;
  }
}

