using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBar : MonoBehaviour{
    public bool MouseOver{get; private set;} = false;

    void OnMouseEnter() => MouseOver = true;
    void OnMouseExit() => MouseOver = false;
}
