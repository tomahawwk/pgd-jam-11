using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField] public string Title {private set; get;}
    [field: SerializeField] public string Property {private set; get;}
    [field: SerializeField] public Sprite Icon {private set; get;}
}
