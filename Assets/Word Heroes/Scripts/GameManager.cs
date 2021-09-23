using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public QuestionData questions;
    private List<int> uncompletedQuestion = new List<int>();
    public string correctWord;
    public string randomedWord;
    private int currentIndexQuestion;

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
        InitializeStartingData();
        ChangeQuestion(currentIndexQuestion);
        RandomizeWord();
        SpawnWord();
    }

    private void InitializeStartingData()
    {
        attackBtn.SetActive(false);
        currentIndexQuestion = 0;
        
        for (int i = 0; i < questions.questionWord.Count; i++)
        {
            uncompletedQuestion.Add(i);
        }
    }
    

    public void ChangeQuestion(int _index)
    {
        correctWord = questions.questionWord[_index];
    }

    public void RandomizeWord()
    {
        correctWord = questions.questionWord[currentIndexQuestion];
        randomedWord = string.Empty;
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

    public void ClearCurrentQuestion()
    {
        for (int i = 0; i < correctWord.Length; i++)
        {
            Destroy(answerBoxList[i].gameObject);
            Destroy(selectionButtonList[i].gameObject);
        }

        answerBoxList.Clear();
        selectionButtonList.Clear();
    }

    public void NextQuestion()
    {
        if (uncompletedQuestion.Count == 0)
        {
            Debug.Log("Question is All Done");
            return;
        }

        if (currentIndexQuestion == questions.questionWord.Count - 1)
        {
            currentIndexQuestion = 0;
        }
        else
        {
            currentIndexQuestion++;
        }

        if (uncompletedQuestion.Exists(x => x == currentIndexQuestion))
        {
            RandomizeWord();
            SpawnWord();
        }
        else
        {
            NextQuestion();
        }
    }

    public void Attack()
    {
        if (!answerBoxList.Exists(x => x.charAnswer.ToString() != x.charText.text))
        {
            Debug.Log("Correct");
            ClearCurrentQuestion();
            uncompletedQuestion.Remove(currentIndexQuestion);

            NextQuestion();
            
        }
    }
}
