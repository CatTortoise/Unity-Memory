
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalCardScriptableObject", menuName = "ScriptableObject/Card")]
public class CardScriptableObject : ScriptableObject
{
    [SerializeField] AnimalScript animal;
    [SerializeField] CardeBase cardBass;

    
    public AnimalScript Animal { get => animal; private set => animal = value; }
    public CardeBase CardBass { get => cardBass; private set => cardBass = value; }
}
