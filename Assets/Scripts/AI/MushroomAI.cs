using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bunashibu.Kikan {
  [RequireComponent(typeof(Enemy))]
  public class MushroomAI : MonoBehaviour {

    [SerializeField] private Enemy _enemy;
  }
}

