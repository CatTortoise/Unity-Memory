using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private LocationMarkerScript locationMarker;
    [SerializeField] private float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveTo = Vector3.Lerp(transform.position, locationMarker.transform.position, moveSpeed * Time.deltaTime);
        playerRigidbody.MovePosition(moveTo);
    }
}
