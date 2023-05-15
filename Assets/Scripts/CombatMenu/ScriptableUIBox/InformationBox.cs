using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationBox : MonoBehaviour
{
    public TMP_Text titleInputField;
    public TMP_Text distanceInputField;
    public TMP_Text hpInputField;

    private UIBoxData uiBoxData;

    public void UpdateInfoData(UIBoxData data)
    {
        uiBoxData = data;

        titleInputField.text = uiBoxData.title;
        distanceInputField.text = uiBoxData.distance.ToString();
        hpInputField.text = uiBoxData.hp.ToString();
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}