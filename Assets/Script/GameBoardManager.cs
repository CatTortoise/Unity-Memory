using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameBoardManager : MonoBehaviour
{
    [SerializeField] Card[] cards;
    [SerializeField] PlayerController[] players; 
    [SerializeField] int numberOfRows;
    [SerializeField] int numberOfColumns;
    [SerializeField] int setSize;
    [SerializeField] float spaceBetweenCards;
    [SerializeField] Transform firstSpawnLocation;
    [SerializeField] float flipAllTimer;
    Card[] gameBoardCards;
    Dictionary<string,int> cardsDictionary;
    private bool checkBoardIsBusy;
    private bool isBoardEmpty;

    public int SetSize { get => setSize;private set => setSize = value; }
    public bool IsBoardEmpty { get => isBoardEmpty;private set => isBoardEmpty = value; }

    // Start is called before the first frame update
    void Start()
    {
        createDictionary();
        GenerateBoard();
        checkBoardIsBusy = false;
        IsBoardEmpty = false;
    }

    private void createDictionary()
    {
        cardsDictionary = new();
        for (int i = 0; i < cards.Length; i++)
        {
            cardsDictionary.Add(cards[i].CardScriptable.name,i);
        }
    }

    private void Update()
    {
        if(!checkBoardIsBusy)
            CheckBoard();
    }
    [ContextMenu("CheckBoard")]
    public void CheckBoard()
    {
        foreach (PlayerController player in players)
        {
            int[] cardIds = CheckForCardSet(player.name);
            if (cardIds.Length == 1)
            {
                checkBoardIsBusy = true;
                Card[] cards = getCardSet(player.name, cardIds[0]);
                if (cards.Length == setSize)
                {
                    StartCoroutine( FullSet(cards));
                }
                else
                {
                    checkBoardIsBusy = false;  
                }
                              
            }
            else if(cardIds.Length >= 1)
            {
                checkBoardIsBusy = true;
                List<Card> cards =new();
                for (int i = 0; i < cardIds.Length; i++)
                {
                    cards.AddRange(getCardSet(player.name, cardIds[i]));
                }
                foreach (Card card in cards)
                {
                    StartCoroutine(FlipACardAfterTimer(card));
                }
            }
        }
    }
    private void CheckIfBoardEmpty()
    {
        IsBoardEmpty = true;
        foreach (Card card in gameBoardCards)
        {
            if (card.gameObject.activeInHierarchy)
            {
                IsBoardEmpty = false;
                break;
            }
        }
    }
    private int[] CheckForCardSet(string playerName)
    {
        List<int> cardsId = new(cards.Length);
        bool isCardInSet;
        for (int i = 0 ; i < gameBoardCards.Length; i++)
        {
            if (gameBoardCards[i].Animal.gameObject.activeInHierarchy)
            {
                if(gameBoardCards[i].CardeBase.ColliderName == playerName)
                {
                    isCardInSet = false;
                    for(int j = 0;j < cardsId.Count; j++)
                    {
                        if(cardsId[j] == gameBoardCards[i].CardSetId)
                        {
                            isCardInSet = true;
                        }
                    }
                    if (!isCardInSet)
                    {
                        cardsId.Add(gameBoardCards[i].CardSetId);
                    }
                }
            }
        }
        return cardsId.ToArray();
    }

    private Card[] getCardSet(string playerName, int cardSetId) {
        List<Card> cardSet = new(setSize);
        foreach (Card card in gameBoardCards)
        {
            if (card.Animal.gameObject.activeInHierarchy && card.CardeBase.ColliderName == playerName)
            {
                if (card.CardSetId == cardSetId)
                {
                    cardSet.Add(card);
                }
            }
        }
        return cardSet.ToArray();
    }

    [ContextMenu("GenerateBoard")]
    public void GenerateBoard()
    {
        int cardIndex;
        int boardSize = numberOfColumns * numberOfRows;
        if (boardSize >= cards.Length * setSize)
        {
            boardSize = cards.Length * setSize;
            numberOfColumns = Mathf.FloorToInt(Mathf.Sqrt(boardSize)) ;
            numberOfRows = Mathf.FloorToInt(boardSize / numberOfColumns);
            numberOfRows += boardSize - (numberOfRows * numberOfColumns);
        }
        Vector3[] UsedLocations = new Vector3[boardSize];
        gameBoardCards = new Card[boardSize];
        List<int> cardMagazine = new List<int>(boardSize);
        Vector3 boardCurrentVector = new(0, firstSpawnLocation.position.y, 0);
        for (int i = 0; i < cards.Length; i ++)
        {
            for (int j = 0; j < setSize; j++)
            {
                cardMagazine.Add(i);
            }
        }
        for (int i = 0; i < boardSize; i++)
        {
            cardIndex = Random.Range(0, cardMagazine.Count);

            gameBoardCards[i] = cards[cardMagazine[cardIndex]];
            cardMagazine.RemoveAt(cardIndex);
        }

        cardIndex = 0;
        for (int x = 0; x < numberOfRows; x++)
        {
            for (int z = 0; z < numberOfColumns && cardIndex < boardSize; z++)
            {
                boardCurrentVector.x = (firstSpawnLocation.position.x + spaceBetweenCards) * x;
                boardCurrentVector.z = (firstSpawnLocation.position.z + spaceBetweenCards) * z;
                gameBoardCards[cardIndex] = Instantiate(gameBoardCards[cardIndex], boardCurrentVector, Quaternion.identity,transform);
                gameBoardCards[cardIndex].name = $"{gameBoardCards[cardIndex].CardScriptable.name} ({x},{z})";
                gameBoardCards[cardIndex].InstantiateCard(cardsDictionary[gameBoardCards[cardIndex].CardScriptable.name]);
                cardIndex++;
            }
        }
    }

    [ContextMenu("HideAnimals")]
    private void HideAnimals()
    {
        foreach (Card card in gameBoardCards)
        {
            if (card.Animal.gameObject.activeInHierarchy)
            {
                StartCoroutine(FlipACardAfterTimer(card));
            }
        }
    }
    private void HideAnimal(Card card)
    {
        card.CardeBase.Flipcard("GameBoardManager");
    }

    [ContextMenu("ClearBoard")]
    public void ClearBoard() 
    {
        for (int i = gameBoardCards.Length - 1; i >= 0 ; i--)
        {
            Destroy(gameBoardCards[i].gameObject);
        }
    }

    
    IEnumerator FlipACardAfterTimer(Card card)
    {
        card.CardeBase.TagolSwhoos();
        for (float i = flipAllTimer; i > 0; i -= 0.1f)
        {
            Debug.Log($"CollisionCoolDown: {i}");
            yield return new WaitForSeconds(0.1f);
        }
        card.CardeBase.TagolSwhoos();
        checkBoardIsBusy = false;
        HideAnimal(card);
        
    }
    IEnumerator FullSet(Card[] cards)
    {
        foreach (Card card in cards)
        {
            card.CardeBase.TagolCircle();
        }
        for (float i = flipAllTimer; i > 0; i -= 0.1f)
        {
            Debug.Log($"CollisionCoolDown: {i}");
            yield return new WaitForSeconds(0.1f);
        }
        foreach (Card card in cards)
        {
            card.CardeBase.TagolCircle();
            card.gameObject.SetActive(false);
        }
        checkBoardIsBusy = false;
        CheckIfBoardEmpty();
    }

}
