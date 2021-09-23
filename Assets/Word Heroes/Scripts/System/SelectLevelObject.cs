using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectLevelObject : MonoBehaviour
{
    public Image monsterImage;
    public TMP_Text monsterName;
    public Button fightButton;

    private string levelID;

    private void Start()
    {
        fightButton.onClick.AddListener(() => LevelManager.instance.SetLevelToLoad(levelID));
    }
    
    public void SetLevel(string _levelID, MonsterData _data)
    {
        levelID = _levelID;
        monsterName.text = _data.monsterName.ToUpper();
    }
}
