using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Monster : MonoBehaviour
{
    public static Monster instance;
    private MonsterData monsterData;

    bool isCounting = false;
    float currentTime;
    public TMP_Text timerText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void StartTimer()
    {
        currentTime = monsterData.timeToAttack;
        isCounting = true;
    }

    private void Update()
    {
        if (isCounting)
            CountTime();
    }

    private void CountTime()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            isCounting = false;
            Debug.Log("Monster attack");
            return;
        }

        timerText.text = Mathf.Floor(currentTime).ToString();
    }

    public void SetMonster(MonsterData _data)
    {
        monsterData = _data;
    }
}
