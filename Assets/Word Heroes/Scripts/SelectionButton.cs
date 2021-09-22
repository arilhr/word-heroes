using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectionButton : MonoBehaviour
{
    public char charSelection;
    public int indexButton;
    public TMP_Text charText;

    public void FillWord()
    {
        List<AnswerBox> answerList = GameManager.instance.answerBoxList;
        AnswerBox blankAnswer = answerList.Find(x => !x.isFilled);

        if (!blankAnswer.isFilled)
        {
            blankAnswer.FillAnswer(charSelection, indexButton);
            SetButtonActive(false);

            GameManager.instance.CheckIfAnswerBoxAlreadyFilled();
        }
    }

    public void SetButtonActive(bool _cond)
    {
        GetComponent<Button>().enabled = _cond;
        GetComponent<Image>().enabled = _cond;
        charText.gameObject.SetActive(_cond);
    }
}
