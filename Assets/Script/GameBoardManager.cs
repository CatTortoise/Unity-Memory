using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameBoardManager : MonoBehaviour
{
    [SerializeField] Card[] cards;
    [SerializeField] int numberOfRows;
    [SerializeField] int numberOfColumns;
    [SerializeField] int setSize;
    [SerializeField] float spaceBetweenCards;
    [SerializeField] Transform firstSpawnLocation;
    Card[] GameBoardCards;

    // Start is called before the first frame update
    void Start()
    {
        GenerateBoard();
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
            numberOfRows = boardSize/ numberOfColumns;
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
            for (int z = 0; z < numberOfColumns; z++)
            {
                boardCurrentVector.x = (firstSpawnLocation.position.x + spaceBetweenCards) * x;
                boardCurrentVector.z = (firstSpawnLocation.position.z + spaceBetweenCards) * z;
                GameBoardCards[cardIndex] = Instantiate(GameBoardCards[cardIndex], boardCurrentVector, Quaternion.identity,transform);
                GameBoardCards[cardIndex].name = $"{GameBoardCards[cardIndex].cardScriptable.name} ({x},{z})";
                GameBoardCards[cardIndex].InstantiateCard();
                cardIndex++;
            }
        }
    }

    [ContextMenu("ClearBoard")]
    public void ClearBoard() 
    {
        for (int i = GameBoardCards.Length - 1; i >= 0 ; i--)
        {
            Destroy(GameBoardCards[i].gameObject);
        }
    }

}
