using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private CardScriptableObject cardScriptable;
    private AnimalScript animal;
    private CardeBase cardeBase;
    private int cardSetId;
    public AnimalScript Animal { get => animal; private set => animal = value; }
    public CardeBase CardeBase { get => cardeBase; private set => cardeBase = value; }
    public CardScriptableObject CardScriptable { get => cardScriptable; private set => cardScriptable = value; }
    public int CardSetId { get => cardSetId; private set =>  cardSetId = value; }

    [ContextMenu("InstantiateCard")]
    public void InstantiateCard(int cardId)
    {
        CardSetId = cardId;
        cardeBase = Instantiate(cardScriptable.CardBass, transform.position, Quaternion.identity, transform);
        animal = Instantiate(cardScriptable.Animal, new Vector3( transform.position.x, transform.position.y , transform.position.z) , Quaternion.identity, transform);
    }

    private void Update()
    {
        if (cardeBase.Flip)
        {
            HideAnimal();
        }
    }


    private void HideAnimal()
    {
        if (animal != null)
        {
            animal.gameObject.SetActive(!animal.gameObject.activeInHierarchy);
            cardeBase.Flipcard("Card");
        }
    }



}
