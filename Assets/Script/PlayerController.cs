using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private LocationMarkerScript locationMarker;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float flyingHeight;
    [SerializeField] private float minFlyingVelocity;

    // Update is called once per frame
    private void FixedUpdate()
    {
        float height;
        if (Mathf.Abs(playerRigidbody.velocity.x + playerRigidbody.velocity.z) < minFlyingVelocity)
            height = locationMarker.transform.position.y;
        else
            height = flyingHeight;
        Vector3 moveTo = Vector3.Slerp(transform.position,new(locationMarker.transform.position.x, height, locationMarker.transform.position.z), moveSpeed * Time.deltaTime);
        //playerRigidbody.MovePosition(moveTo);
        //Vector3 moveTo = new()
        //playerRigidbody.AddForce((locationMarker.transform.position - transform.position)* moveSpeed * Time.deltaTime, ForceMode.Acceleration);
        playerRigidbody.AddForce((moveTo - transform.position) * moveSpeed * Time.deltaTime, ForceMode.Acceleration);
    }

}
