using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster Question Data", menuName ="Monster/Monster Data", order = 1)]
public class QuestionData : ScriptableObject
{
    public List<string> questionWord = new List<string>();
}
