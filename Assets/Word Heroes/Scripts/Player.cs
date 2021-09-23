using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public PlayerData playerData;
    private float currentHP;

    public Slider playerHealthUI;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentHP = playerData.maxHP;
    }

    private void Update()
    {
        UpdatedHealthBar();
    }

    public void DecreaseHealth(float _decreasedHealth)
    {
        currentHP -= _decreasedHealth;
        if (currentHP > playerData.maxHP)
            currentHP = playerData.maxHP;

        if (currentHP <= 0)
        {
            currentHP = 0;

            // lose
            Debug.Log("Lose");
        }
    }

    private void UpdatedHealthBar()
    {
        float targetvalue = currentHP / playerData.maxHP;
        if (playerHealthUI.value != targetvalue)
            playerHealthUI.value = Mathf.Lerp(playerHealthUI.value, targetvalue, 0.02f);
    }
}
