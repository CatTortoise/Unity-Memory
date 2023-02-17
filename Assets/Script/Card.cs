using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardScriptableObject cardScriptable;
    private AnimalScript animal;
    private CardeBase cardeBase;
    private bool isHiden = false;

    [ContextMenu("InstantiateCard")]
    public void InstantiateCard()
    {
        cardeBase = Instantiate(cardScriptable.CardBass, transform.position, Quaternion.identity, transform);
        animal = Instantiate(cardScriptable.Animal, new Vector3( transform.position.x, transform.position.y , transform.position.z) , Quaternion.identity, transform);
    }

    private void Update()
    {
        if (cardeBase.IsRevealed != isHiden)
        {
            HideAnimal();
        }
    }


    private void HideAnimal()
    {
        if (animal != null)
        {
            animal.gameObject.active = cardeBase.IsRevealed;
            isHiden = cardeBase.IsRevealed;
        }
    }



}
