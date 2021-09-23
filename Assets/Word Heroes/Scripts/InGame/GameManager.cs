using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public MonsterData currentMonster;
    public UnityEvent OnStartGame;
    public UnityEvent OnPauseGame;
    public UnityEvent OnGameEnded;
    public UnityEvent OnGameWin;
    public UnityEvent OnGameLose;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        WordManager.instance.SetMonsterData(currentMonster);
        WordManager.instance.InitializeStartingData();
        WordManager.instance.SpawnWord();
        Monster.instance.SetMonster(currentMonster);
        Monster.instance.InitializeMonsterData(currentMonster);
        Monster.instance.StartTimer();

    }

    public void GameStart()
    {
        OnStartGame?.Invoke();
    }

    public void GamePause()
    {
        OnPauseGame?.Invoke();
    }

    public void GameEnded()
    {
        OnGameEnded?.Invoke();
    }

    public void GameWin()
    {
        OnGameWin?.Invoke();
    }

    public void GameLose()
    {
        OnGameLose?.Invoke();
    }
    
}
