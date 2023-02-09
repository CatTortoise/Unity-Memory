using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{
    [SerializeField] Rigidbody animalRigidbody;
    [SerializeField] Transform animalTransform;
    //[SerializeField] float upForce;
    //[SerializeField] float rotateRightSpeed;
    //[SerializeField] float pulseTimer;
    //[SerializeField] float animalGravity;
    //private float timerPast;

    public Transform AnimalTransform { get => animalTransform; private set => animalTransform = value; }

    //private void Start()
    //{
    //    timerPast = 0;

    //}

    //private void FixedUpdate()
    //{
    //    timerPast += 1;
    //    if (timerPast <= pulseTimer)
    //    {
    //        animalRigidbody.AddForce(Vector3.up * upForce , ForceMode.VelocityChange);
    //        animalRigidbody.AddTorque(Vector3.right * rotateRightSpeed ,ForceMode.VelocityChange);
    //        Debug.Log("Up");
    //    }
    //    else
    //    {
    //        animalRigidbody.AddForce(Vector3.down * upForce, ForceMode.VelocityChange);
    //        Debug.Log("down");
    //    }
    //    if (timerPast >= pulseTimer * 2)
    //    {
    //        timerPast = 0;
    //    }
    //    animalRigidbody.AddForce(Vector3.down * animalGravity, ForceMode.Acceleration);

    //}

}
