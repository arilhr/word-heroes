using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string correctWord;
    public string randomedWord;

    public GameObject answerBoxPrefab;
    public GameObject answerBoxParent;
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

            Debug.Log($"{rand}, {randomedWord}, {temp}");
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

            // spawn selection
            GameObject selectionObject = Instantiate(selectionBoxPrefab, selectionParent.transform);
            SelectionButton selection = selectionObject.GetComponent<SelectionButton>();
            selection.charSelection = randomedWord[i];
            selection.charText.text = randomedWord[i].ToString();
        }
        
    }
}
