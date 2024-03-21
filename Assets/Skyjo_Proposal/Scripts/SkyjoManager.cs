using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class SkyjoManager : MonoBehaviour
{

    [SerializeField]
    private int[,] yourGrid = new int[,]
    {
        {10, 1, 12, 2},
        {5, 6, -1, 2},
        {4, 3, 1, 0},
    };

    [SerializeField]
    private int[,] theirGrid = new int[,]
    {
        {1, 1, 4, 6},
        {8, 7, 0, 5},
        {11, 4, 1, 8},
    };

    private List<int> disCardNum = new List<int>();

    private bool[,] flipped;

    private int nRows;
    private int nCols;

    private int row;
    private int col;

    private int yourScore;
    private int theirScore;

    private int startTwo;
    private int deckTopNum = -3;

    private bool decking;

    public Button discardButton;
    public Button deckButton;

    [SerializeField] Transform gridRoot;

    [SerializeField] GameObject cardPrefab;
    [SerializeField] GameObject disCard;
    [SerializeField] GameObject deckTop;

    [SerializeField] Transform disCardPos;
    [SerializeField] Transform deckTopPos;
    //[SerializeField] GameObject winLabel;

    [SerializeField] TextMeshProUGUI scoreLabel;

    private void Awake()
    {
        nRows = yourGrid.GetLength(0);
        nCols = yourGrid.GetLength(1);

        startTwo = 0;

        flipped = new bool[nRows, nCols];

        for (int i = 0; i < nRows; i++)
        {
            for (int j = 0; j < nCols; j++)
            {
                yourGrid[i, j] = Random.Range(-2, 12);
            }
        }

        Transform flip = disCardPos.Find("Back");
        flip.gameObject.SetActive(false);

        TextMeshProUGUI disCardVal;
        disCardVal = disCardPos.GetComponentInChildren<TextMeshProUGUI>();

        disCardVal.text = Random.Range(-2, 12).ToString();

        for (int l = 0; l < nRows * nCols; l++)
        {
            Instantiate(cardPrefab, gridRoot);
        }

        SelectCurrentCard();
    }

    Transform GetCurrentCard()
    {
        int index = (row * nCols) + col;

        return gridRoot.GetChild(index);
    }

    void SelectCurrentCard()
    {
        Transform card = GetCurrentCard();
        Transform cursor = card.Find("Cursor");
        cursor.gameObject.SetActive(true);
    }

    void UnselectCurrentCard()
    {
        Transform card = GetCurrentCard();
        Transform cursor = card.Find("Cursor");
        cursor.gameObject.SetActive(false);
    }

    public void MoveHorizontal(int amt)
    {
        UnselectCurrentCard();

        col += amt;
        col = Mathf.Clamp(col, 0, nCols - 1);

        SelectCurrentCard();
    }

    public void MoveVertical(int amt)
    {
        UnselectCurrentCard();

        row += amt;
        row = Mathf.Clamp(row, 0, nRows - 1);

        SelectCurrentCard();
    }

    void FlipUp(int cardVal)
    {
        Transform card = GetCurrentCard();
        Transform flip = card.Find("Back");
        TextMeshProUGUI valueFirst;

        flip.gameObject.SetActive(false);
        valueFirst = card.GetComponentInChildren<TextMeshProUGUI>();

        valueFirst.text = cardVal.ToString();

        if (startTwo < 1)
        {
            startTwo++;
        } else
        {
            discardButton.interactable = true;
            deckButton.interactable = true;
        }

        if (deckTopNum != -3 && decking)
        {

            Transform flipDeck = deckTopPos.Find("Back");

            Transform flipDis = disCardPos.Find("Back");
            valueFirst = disCardPos.GetComponentInChildren<TextMeshProUGUI>();

            disCardNum.Add(deckTopNum);
            flipDeck.gameObject.SetActive(true);
            valueFirst.text = deckTopNum.ToString();
            deckTopNum = -3;
        }
    }

    void FlipDown()
    {
        Transform card = GetCurrentCard();
        Transform flip = card.Find("Back");
        flip.gameObject.SetActive(true);
    }

    public void FlipForReal()
    {
        if (flipped[row, col]) return;

        flipped[row, col] = true;

        int cardVal = yourGrid[row, col];
        
        FlipUp(cardVal);
        IncrementScore(cardVal);

        TryEndGame();
    }

    public void FlipForReal(int deckVal)
    {
        if (flipped[row, col]) return;

        flipped[row, col] = true;

        yourGrid[row, col] = deckVal;

        FlipUp(deckVal);
        IncrementScore(deckVal);

        TryEndGame();
    }

    void TryEndGame()
    {
        for (int row = 0; row < nRows; row++)
        {
            for (int col = 0; col < nCols; col++)
            {

                if (flipped[row, col] == false) return;
            }
        }

        scoreLabel.text = string.Format("Final Score: {0}", yourScore);

    }

    void IncrementScore(int val)
    {
        yourScore += val;
        scoreLabel.text = string.Format("Score: {0}", yourScore);
    }

    public void NewDisCard()
    {
        TextMeshProUGUI valueFirst;
        valueFirst = disCardPos.GetComponentInChildren<TextMeshProUGUI>();

        if (disCardNum.Count <= 0)
        {
            return;
        }
        else
        {
            FlipForReal(disCardNum[disCardNum.Count - 1]);
            disCardNum.Remove(disCardNum.Count - 1);

            if (disCardNum.Count > 0)
            {
                valueFirst.text = disCardNum[disCardNum.Count - 1].ToString();
            }
            else
            {
                valueFirst.text = "";
            }
        }
    }

    public void NewDeckTop()
    {
        Transform flip = deckTopPos.Find("Back");

        TextMeshProUGUI valueFirst;
        valueFirst = deckTopPos.GetComponentInChildren<TextMeshProUGUI>();

        if (valueFirst.text == "test")
        {
            deckTopNum = Random.Range(-2, 12);

            flip.gameObject.SetActive(false);
            valueFirst.text = deckTopNum.ToString();
        }
        else
        {
            FlipForReal(deckTopNum);
            valueFirst.text = "test";
            flip.gameObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
