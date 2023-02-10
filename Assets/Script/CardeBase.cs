using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardeBase : MonoBehaviour
{
   [SerializeField] Rigidbody rigidbody;
   bool isRevealed = false;

    public bool IsRevealed { get => isRevealed; private set => isRevealed = value; }

    [ContextMenu("Flipcard")]
    public void Flipcard()
    {
        if (IsRevealed)
        {
            IsRevealed = false;
        }
        else
        {
            IsRevealed = true;
        }
        Debug.Log($"isRevealed: {IsRevealed}");
    }
}
