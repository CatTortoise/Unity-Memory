using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameBoardManager boardManager;


    // Update is called once per frame
    void Update()
    {
        if (boardManager.IsBoardEmpty)
        {
            SceneManager.LoadScene("MaineMenu");
        }
    }
}
