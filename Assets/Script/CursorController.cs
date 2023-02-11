using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    [SerializeField] private Texture2D coursorTexture;
    [SerializeField] private LocationMarkerScript locationMarker;

    void Start()
    {
        Cursor.SetCursor (coursorTexture, new Vector3(coursorTexture.width / 2, coursorTexture.height / 2), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButton(0))
        {
            Physics.Raycast(ray, out hit);
            locationMarker.ChangeLocation(new( hit.point.x,0, hit.point.z));
        }
        
        
    }
}
