using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnswerBox : MonoBehaviour
{
    public char charAnswer;
    public bool isFilled;
    public TMP_Text charText;

    private int indexSelectionButton;

    public void FillAnswer(char _w, int _indexSelectionButton)
    {
        isFilled = true;
        indexSelectionButton = _indexSelectionButton;
        charText.text = _w.ToString().ToUpper();
    }

    public void DeleteAnswer()
    {
        if (!isFilled) return;

        List<SelectionButton> buttons = WordManager.instance.selectionButtonList;
        buttons[indexSelectionButton].SetButtonActive(true);

        charText.text = string.Empty;
        indexSelectionButton = -1;
        isFilled = false;
    }
}
