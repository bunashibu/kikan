using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class EnemyStatus : ScriptableObject {
  public int id;
  public int life;
  public int gold;
  public int exp;
  public float atk;
  public float dfn;
  public float spd;
  public float jmp;
}

