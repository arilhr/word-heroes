using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    public static WordManager instance;

    private MonsterData monsterData;
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
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        CheckIfAllAnswerBoxAlreadyFilled();
    }

    public void InitializeStartingData()
    {
        attackBtn.SetActive(false);
        currentIndexQuestion = 0;
        ChangeQuestion();
        for (int i = 0; i < monsterData.questionWord.Count; i++)
        {
            uncompletedQuestion.Add(i);
        }
    }

    public void ChangeQuestion()
    {
        correctWord = monsterData.questionWord[currentIndexQuestion];
    }

    public void RandomizeWord()
    {
        randomedWord = string.Empty;
        string temp = correctWord;

        for (int i = 0; i < correctWord.Length; i++)
        {
            int rand = Random.Range(0, temp.Length);

            randomedWord += temp[rand];
            temp = temp.Remove(rand, 1);
        }
    }

    public void SpawnWord()
    {
        RandomizeWord();

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

    public void CheckIfAllAnswerBoxAlreadyFilled()
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

        if (currentIndexQuestion == monsterData.questionWord.Count - 1)
        {
            currentIndexQuestion = 0;
        }
        else
        {
            currentIndexQuestion++;
        }

        if (uncompletedQuestion.Exists(x => x == currentIndexQuestion))
        {
            ChangeQuestion();
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

            // effect to monster
            Monster.instance.AddTime(2f);
            Monster.instance.DecreasedHealth();

            // clear and next question
            ClearCurrentQuestion();
            uncompletedQuestion.Remove(currentIndexQuestion);

            NextQuestion();
        }
        else
        {
            Debug.Log("Uncorrect");
            ResetAnswer();
        }
    }

    public void Skip()
    {
        if (uncompletedQuestion.Count <= 1) return;

        ClearCurrentQuestion();
        NextQuestion();
    }

    public void ResetAnswer()
    {
        foreach (AnswerBox a in answerBoxList)
        {
            a.DeleteAnswer();
        }

    }

    public void SetMonsterData(MonsterData _data)
    {
        monsterData = _data;
    }
}
