using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardeBase : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float collisionOnCoolDown;
    bool isRevealed = true;

    bool isCollisionOnCoolDown = false;

    public bool IsRevealed { get => isRevealed; private set => isRevealed = value; }

    [ContextMenu("Flipcard")]
    public void Flipcard()
    {
        Debug.Log($"Flipcard");
        if (IsRevealed)
        {
            IsRevealed = false;
        }
        else
        {
            IsRevealed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter "+ other.tag);
        if (other.tag == "Player" && !isCollisionOnCoolDown)
        {
            Flipcard();
            StartCoroutine(CollisionCoolDown());
        }
    }

    IEnumerator CollisionCoolDown()
    {
        isCollisionOnCoolDown = true;
        for (float i = collisionOnCoolDown; i > 0; i -= 0.1f)
        {
            Debug.Log($"CollisionCoolDown: {i}");
            yield return new WaitForSeconds(0.1f);
        }
        isCollisionOnCoolDown = false;
    }
}
