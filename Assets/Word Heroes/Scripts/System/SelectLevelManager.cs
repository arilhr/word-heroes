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
    private int currentPage;

    public GameObject previousButton;
    public GameObject nextButton;


    private void Start()
    {
        SpawnLevelList();
    }

    public void SpawnLevelList()
    {
        currentPage = 0;
        int manyLevel = LevelManager.instance.levels.Count;
        int pageCount = Mathf.CeilToInt((float)manyLevel / (float)maxLevelObjectOnPage);

        for (int i = 0; i < pageCount; i++)
        {
            // spawn select level page
            GameObject pageObject = Instantiate(levelListPage, levelListPageParent);
            pages.Add(pageObject);

            int indexLevel = i * maxLevelObjectOnPage;
            int maxIndex = maxLevelObjectOnPage < manyLevel - (i * maxLevelObjectOnPage) ? maxLevelObjectOnPage : manyLevel - (i * maxLevelObjectOnPage);
            for (int j = 0; j < maxIndex; j++)
            {
                // spawn level object
                GameObject level = Instantiate(levelObject, pageObject.transform);
                level.GetComponent<SelectLevelObject>().SetLevel(LevelManager.instance.levels[indexLevel].levelID, LevelManager.instance.levels[indexLevel].monsterData);
                indexLevel++;
            }

            if (i > 0)
                pageObject.SetActive(false);
            else
                pageObject.SetActive(true);
        }

        SetPage(0, currentPage);
    }

    public void NextPage()
    {
        if (currentPage < pages.Count)
        {
            currentPage++;
            SetPage(currentPage - 1, currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            SetPage(currentPage + 1, currentPage);
        }
    }

    private void SetPage(int _before, int _after)
    {
        pages[_before].SetActive(false);
        pages[_after].SetActive(true);
        nextButton.SetActive(true);
        if (_after == pages.Count - 1)
            nextButton.SetActive(false);
        else
            nextButton.SetActive(true);

        if (_after == 0)
            previousButton.SetActive(false);
        else
            previousButton.SetActive(true);
    }

    public void ResetPage()
    {
        SetPage(currentPage, 0);
        currentPage = 0;
    }
}
