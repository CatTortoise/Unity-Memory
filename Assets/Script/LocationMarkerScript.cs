using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationMarkerScript : MonoBehaviour
{
    public void ChangeLocation(Vector3 newLocation)
    {
        transform.position = newLocation;
        gameObject.SetActive(true);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            gameObject.SetActive(false);
    }
}
