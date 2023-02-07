using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] CardScriptableObject cardScriptable;
    [SerializeField] Rigidbody cardRigidbody  ;
    bool isRevealed = false;

    private void Start()
    {
        Instantiate(cardScriptable.Animal, transform);
    }

    [ContextMenu("Flipcard")]
    public void Flipcard()
    {
        if (isRevealed)
        {
            isRevealed = false;
        }
        else
        {
            isRevealed = true;
        }
        Debug.Log($"isRevealed: {isRevealed}");
    }
}
