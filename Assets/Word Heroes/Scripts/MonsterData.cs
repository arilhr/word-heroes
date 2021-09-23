using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Data", menuName ="Monster/Monster Data", order = 1)]
public class MonsterData : ScriptableObject
{
    public string monsterName = string.Empty;
    public int monsterHealth = 0;
    public float monsterDamage = 0;
    public float timeToAttack = 0;

    public List<string> questionWord = new List<string>();
}
