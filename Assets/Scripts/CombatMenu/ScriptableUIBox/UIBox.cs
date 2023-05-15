using UnityEngine;

[CreateAssetMenu(fileName = "UIBox", menuName = "UI Box", order = 1)]
public class UIBox : ScriptableObject
{
    public string title;
    public float distance;
    public int hp;
}