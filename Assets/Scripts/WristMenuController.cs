using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WristMenuController : MonoBehaviour
{
    public GameObject wristUI;

    public bool activeWristUI = true;

    [SerializeField] TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        ToggleWristUI();
    }

    //public void MenuPressed(){
    //    DisplayWristUI();
    //}

    public void ToggleWristUI()
    {
        if(activeWristUI)
        {
            wristUI.SetActive(false);
            activeWristUI = false;
        }else
        {
            wristUI.SetActive(true);
            activeWristUI = true;
        }
    }
    public void SetWristState(bool state)
    {
        wristUI.SetActive(state);
        activeWristUI = state;
    }

    public void ResetText()
    {
        textMeshPro.text = "";
    }

    public void SetText(string message)
    {
        textMeshPro.text = message;
    }

}
