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
    Card[] GameBoardCards;
    Dictionary<string,int> cardsDictionary;
    private bool CheckBoardIsBusy;
    public int SetSize { get => setSize;private set => setSize = value; }

    // Start is called before the first frame update
    void Start()
    {
        createDictionary();
        GenerateBoard();
        CheckBoardIsBusy = false;
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
        if(!CheckBoardIsBusy)
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
                CheckBoardIsBusy = true;
                Card[] cards = getCardSet(player.name, cardIds[0]);
                if (cards.Length == setSize)
                {
                    foreach (Card card in cards)
                    {
                        card.gameObject.SetActive(false);
                    }
                }
                CheckBoardIsBusy = false;
            }
            else if(cardIds.Length >= 1)
            {
                CheckBoardIsBusy = true;
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

    private int[] CheckForCardSet(string playerName)
    {
        List<int> cardsId = new(cards.Length);
        bool isCardInSet;
        for (int i = 0 ; i < GameBoardCards.Length; i++)
        {
            if (GameBoardCards[i].Animal.gameObject.activeInHierarchy)
            {
                if(GameBoardCards[i].CardeBase.ColliderName == playerName)
                {
                    isCardInSet = false;
                    for(int j = 0;j < cardsId.Count; j++)
                    {
                        if(cardsId[j] == GameBoardCards[i].CardSetId)
                        {
                            isCardInSet = true;
                        }
                    }
                    if (!isCardInSet)
                    {
                        cardsId.Add(GameBoardCards[i].CardSetId);
                    }
                }
            }
        }
        return cardsId.ToArray();
    }

    private Card[] getCardSet(string playerName, int cardSetId) {
        List<Card> cardSet = new(setSize);
        foreach (Card card in GameBoardCards)
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
        GameBoardCards = new Card[boardSize];
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

            GameBoardCards[i] = cards[cardMagazine[cardIndex]];
            cardMagazine.RemoveAt(cardIndex);
        }

        cardIndex = 0;
        for (int x = 0; x < numberOfRows; x++)
        {
            for (int z = 0; z < numberOfColumns && cardIndex < boardSize; z++)
            {
                boardCurrentVector.x = (firstSpawnLocation.position.x + spaceBetweenCards) * x;
                boardCurrentVector.z = (firstSpawnLocation.position.z + spaceBetweenCards) * z;
                GameBoardCards[cardIndex] = Instantiate(GameBoardCards[cardIndex], boardCurrentVector, Quaternion.identity,transform);
                GameBoardCards[cardIndex].name = $"{GameBoardCards[cardIndex].CardScriptable.name} ({x},{z})";
                GameBoardCards[cardIndex].InstantiateCard(cardsDictionary[GameBoardCards[cardIndex].CardScriptable.name]);
                cardIndex++;
            }
        }
    }

    [ContextMenu("HideAnimals")]
    private void HideAnimals()
    {
        foreach (Card card in GameBoardCards)
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
        for (int i = GameBoardCards.Length - 1; i >= 0 ; i--)
        {
            Destroy(GameBoardCards[i].gameObject);
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
        CheckBoardIsBusy = false;
        HideAnimal(card);
        
    }

}
