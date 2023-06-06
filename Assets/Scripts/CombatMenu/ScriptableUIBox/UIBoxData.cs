using UnityEngine;

[CreateAssetMenu(fileName = "UIBoxData", menuName = "UI Box Data", order = 1)]
public class UIBoxData : ScriptableObject
{
    public string title;
    public float distance;
    public int hp;
}