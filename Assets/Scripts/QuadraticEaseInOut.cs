using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  public class QuadraticEaseInOut {
    public QuadraticEaseInOut(Vector3 start, Vector3 dest, float duration) {
      _start = start;
      _distance = dest - start;
      _duration = duration;
      _curTime = 0;
    }

    public Vector3 GetNextPosition() {
      _curTime += Time.deltaTime;
      float x = Calculate(_curTime, _start.x, _distance.x, _duration);
      float y = Calculate(_curTime, _start.y, _distance.y, _duration);
      float z = Calculate(_curTime, _start.z, _distance.z, _duration);

      return new Vector3(x, y, z);
    }

    /* t: current time                                          *
     * b: start value                                           *
     * c: dest - start value (e.g. dest:100, start:10, c is 90) *
     * d: duration                                              */
    private float Calculate(float t, float b, float c, float d) {
      t /= d/2;
      if (t < 1)
        return c/2*t*t + b;
      --t;
      return -c/2 * (t*(t-2) - 1) + b;
    }

    private float _curTime;
    private float _duration;
    private Vector3 _start;
    private Vector3 _distance;
  }
}

