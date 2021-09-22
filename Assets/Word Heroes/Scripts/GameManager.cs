using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public QuestionData questions;
    public string correctWord;
    public string randomedWord;
    private int currentQuestion;

    public List<AnswerBox> answerBoxList;
    public GameObject answerBoxPrefab;
    public GameObject answerBoxParent;
    public List<SelectionButton> selectionButtonList;
    public GameObject selectionBoxPrefab;
    public GameObject selectionParent;

    public GameObject attackBtn;
    public GameObject skipBtn;
    
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
        attackBtn.SetActive(false);
        RandomizeWord();
        SpawnWord();
    }

    public void ChangeQuestion(int _index)
    {
        correctWord = questions.questionWord[_index];
    }

    public void RandomizeWord()
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

    public void CheckIfAnswerBoxAlreadyFilled()
    {
        if (!answerBoxList.Exists(x => !x.isFilled))
        {
            attackBtn.SetActive(true);
        }
        else
        {
            attackBtn.SetActive(false);
        }
    }

    public void Attack()
    {
        if (!answerBoxList.Exists(x => x.charAnswer.ToString() != x.charText.text))
        {
            Debug.Log("Correct");
        }
    }
}
