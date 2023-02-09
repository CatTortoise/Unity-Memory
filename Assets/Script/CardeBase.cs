using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardeBase : MonoBehaviour
{
   [SerializeField] Rigidbody rigidbody;
    bool isRevealed = false;

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
