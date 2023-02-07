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
        int boardSize = numberOfColumns * numberOfRows;
        if (boardSize >= cards.Length * setSize)
            boardSize = cards.Length * setSize;
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
            int cardMagazineIndex = Random.Range(0, cardMagazine.Count);

            GameBoardCards[i] = cards[cardMagazine[cardMagazineIndex]];
            cardMagazine.RemoveAt(cardMagazineIndex);
        }

        for (int x = 0; x < numberOfRows; x++)
        {
            for (int z = 0; z < numberOfColumns; z++)
            {
                boardCurrentVector.x = (firstSpawnLocation.position.x + spaceBetweenCards) * x;
                boardCurrentVector.z = (firstSpawnLocation.position.z + spaceBetweenCards) * z;
                GameBoardCards[x + z] = Instantiate(GameBoardCards[x + z], boardCurrentVector, Quaternion.identity);
            }
        }
    }

    public void ClearBoard() { }
    // Update is called once per frame
    void Update()
    {
        
    }
}
