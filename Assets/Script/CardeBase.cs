using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardeBase : MonoBehaviour
{
    [SerializeField] Rigidbody rigidbody;
    [SerializeField] float collisionOnCoolDown;
    [SerializeField] bool flip ;
    [SerializeField] GameObject swhoos;
    [SerializeField] GameBoardManager gameBoard;
    private string colliderName;

    bool isCollisionOnCoolDown = false;

    public bool Flip { get => flip; private set => flip = value; }
    public string ColliderName { get => colliderName; set => colliderName = value; }

    private void Start()
    {
        flip = true;
        swhoos.SetActive(false);
    }


    [ContextMenu("Flipcard")]
    public void Flipcard(string fliperName)
    {
        flip =! flip;
        if (flip)
        {
            ColliderName = fliperName;
        }
    }
    public void TagolSwhoos()
    {
        swhoos.SetActive(!swhoos.activeInHierarchy);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnCollisionEnter "+ other.tag);
        if (other.tag == "Player" && !isCollisionOnCoolDown)
        {
            Flipcard(other.name);
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
