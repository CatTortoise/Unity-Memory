
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalCardScriptableObject", menuName = "ScriptableObject/Card")]
public class CardScriptableObject : ScriptableObject
{
    [SerializeField] GameObject animal;
    [SerializeField] GameObject terrain;

    public GameObject Animal { get => animal; private set => animal = value; }
    public GameObject Terrain { get => terrain; private set => terrain = value; }


}
