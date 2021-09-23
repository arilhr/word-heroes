using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLevelManager : MonoBehaviour
{
    public Transform levelListPageParent;
    public GameObject levelListPage;
    private List<GameObject> pages = new List<GameObject>();
    public GameObject levelObject;

    public int maxLevelObjectOnPage = 3;

    private void Start()
    {
        SpawnLevelList();
    }

    public void SpawnLevelList()
    {
        int manyLevel = LevelManager.instance.levels.Count;
        int pageCount = Mathf.CeilToInt((float) manyLevel / (float) maxLevelObjectOnPage);

        for (int i = 0; i < pageCount; i++)
        {
            // spawn select level page
            GameObject pageObject = Instantiate(levelListPage, levelListPageParent);
            pages.Add(pageObject);

            int indexLevel = i * maxLevelObjectOnPage;
            int maxIndex = maxLevelObjectOnPage < manyLevel - (i * maxLevelObjectOnPage) ? maxLevelObjectOnPage : manyLevel;
            for (int j = 0; j < maxIndex; j++)
            {
                // spawn level object
                GameObject level = Instantiate(levelObject, pageObject.transform);
                level.GetComponent<SelectLevelObject>().SetLevel(LevelManager.instance.levels[indexLevel].levelID, LevelManager.instance.levels[indexLevel].monsterData);
            }

            if (i > 0)
                pageObject.SetActive(false);
            else
                pageObject.SetActive(true);
        }
    }
}
