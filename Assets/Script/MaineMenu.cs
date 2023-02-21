using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaineMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("ArcticScene");
    }
}
