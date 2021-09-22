using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string correctWord;
    public string randomedWord;

    public List<AnswerBox> answerBoxList;
    public GameObject answerBoxPrefab;
    public GameObject answerBoxParent;
    public List<SelectionButton> selectionButtonList;
    public GameObject selectionBoxPrefab;
    public GameObject selectionParent;
    
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
        RandomWord();
        SpawnWord();
    }

    public void RandomWord()
    {
        string temp = correctWord;

        for (int i = 0; i < correctWord.Length; i++)
        {
            int rand = Random.Range(0, temp.Length);

            randomedWord += temp[rand];
            temp = temp.Remove(rand, 1);
        }
    }

    private void SpawnWord()
    {
        for (int i = 0; i < correctWord.Length; i++)
        {
            // spawn answer box
            GameObject answerObject = Instantiate(answerBoxPrefab, answerBoxParent.transform);
            AnswerBox answer = answerObject.GetComponent<AnswerBox>();
            answer.charAnswer = correctWord[i];
            answer.charText.text = string.Empty;
            answer.isFilled = false;
            answerBoxList.Add(answer);

            // spawn selection
            GameObject selectionObject = Instantiate(selectionBoxPrefab, selectionParent.transform);
            SelectionButton selection = selectionObject.GetComponent<SelectionButton>();
            selection.charSelection = randomedWord[i];
            selection.charText.text = randomedWord[i].ToString();
            selection.indexButton = i;
            selectionButtonList.Add(selection);
        }

    }
}
