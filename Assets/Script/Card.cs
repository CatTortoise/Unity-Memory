using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardScriptableObject cardScriptable;
    private AnimalScript animal;
    private CardeBase cardeBase;

    [ContextMenu("InstantiateCard")]
    public void InstantiateCard()
    {
        cardeBase = Instantiate(cardScriptable.CardBass, transform.position, Quaternion.identity, transform);
        animal = Instantiate(cardScriptable.Animal, new Vector3( transform.position.x, transform.position.y , transform.position.z) , Quaternion.identity, transform);
    }
    [ContextMenu("HideAnimal")]
    public void HideAnimal()
    {
        if(animal != null)
        {
            if(animal.gameObject.active)
            {
                animal.gameObject.active = false;
            }
            else
            {
                animal.gameObject.active = true;
            }
        }
    }

}
