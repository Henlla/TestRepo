using UnityEngine;

[CreateAssetMenu(fileName = "EnclosureLib", menuName = "SO/Object/Enclosure")]
public class EnclosureObjectSO : BaseObjectSO
{
    public float Level;
    public EnclosureType EnclosureType;
}

public enum EnclosureType
{
    Chicken
}
