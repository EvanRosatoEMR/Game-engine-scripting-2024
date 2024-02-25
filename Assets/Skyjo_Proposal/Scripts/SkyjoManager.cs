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

    private bool[,] flipped;

    private int nRows;
    private int nCols;

    private int row;
    private int col;

    private int yourScore;
    private int theirScore;

    private int startTwo;

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

        for (int i = 0; i < nRows * nCols; i++)
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

        if (startTwo < 2)
        {
            startTwo++;
        } else
        {
            discardButton.interactable = true;
            deckButton.interactable = true;
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

    void NewDisCard()
    {
        int ran = Random.Range(-2, 12);
        /*Instantiate(cardPrefab, disCardPos);

        Transform card = GetCurrentCard();
        Transform flip = card.Find("Back");
        TextMeshProUGUI valueFirst;

        flip.gameObject.SetActive(false);
        valueFirst = card.GetComponentInChildren<TextMeshProUGUI>();

        valueFirst.text = ran.ToString();*/
    }

    void NewDeckTop()
    {
        int ran = Random.Range(-2, 12);
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
