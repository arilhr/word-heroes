using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Monster : MonoBehaviour
{
    public static Monster instance;
    private MonsterData monsterData;

    private bool isCounting = false;
    private float currentTime;

    public Slider monsterHealthBar;
    private int currentMonsterHealth;

    public TMP_Text timerText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void InitializeMonsterData(MonsterData _data)
    {
        monsterData = _data;

        currentMonsterHealth = monsterData.monsterHealth;
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

        UpdatedMonsterHealthBar();
    }

    private void CountTime()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;
            MonsterAttack();
            Debug.Log("Monster attack");
            StartTimer();
            return;
        }

        timerText.text = Mathf.Floor(currentTime).ToString();
    }

    public void DecreasedHealth()
    {
        currentMonsterHealth--;

        if (currentMonsterHealth <= 0)
        {
            currentMonsterHealth = 0;

            Debug.Log("Monster Die");

            // Game Win
        }
    }

    private void UpdatedMonsterHealthBar()
    {
        float targetValue = (float) currentMonsterHealth / (float) monsterData.monsterHealth;

        if (monsterHealthBar.value != targetValue)
        {
            monsterHealthBar.value = Mathf.Lerp(monsterHealthBar.value, targetValue, 0.02f);
        }
    }

    public void AddTime(float _addedTime)
    {
        currentTime += _addedTime;
    }

    private void MonsterAttack()
    {
        Player.instance.DecreaseHealth(monsterData.monsterDamage);

        // animation
    }

    public void SetMonster(MonsterData _data)
    {
        monsterData = _data;
    }
}
