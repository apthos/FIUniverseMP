using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ParentSelectWheelScript : MonoBehaviour
{
    public GameObject RightHandPresence;

    public GameObject parentCanvas;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    public Vector2 rightStickAxisPosition;
    public Vector2 normaliseMousePosition;
    public float currentAngle;
    public int selection;
    private int previousSelection;

    public GameObject[] menuItems;

    private SelectionWheel menuItemSc;
    private SelectionWheel previousMenuItemSc;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize();
        parentCanvas.GetComponent<Canvas>().enabled = false;
    }

    void TryInitialize()
    {

        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {


        rightStickAxisPosition = RightHandPresence.GetComponent<HandPresence>().RightThumbStickAxisValue();
        currentAngle = Mathf.Atan2(rightStickAxisPosition.y, rightStickAxisPosition.x) * Mathf.Rad2Deg;

        currentAngle = (currentAngle + 360) % 360;

        selection = (int)currentAngle / 45;

        if (selection != previousSelection)
        {
            previousMenuItemSc = menuItems[previousSelection].GetComponent<SelectionWheel>();
            previousMenuItemSc.Deselect();
            previousSelection = selection;

            menuItemSc = menuItems[selection].GetComponent<SelectionWheel>();
            menuItemSc.Select();
        }

        if (rightStickAxisPosition.x == 0 && rightStickAxisPosition.y == 0)
        {
            previousMenuItemSc = menuItems[previousSelection].GetComponent<SelectionWheel>();
            previousMenuItemSc.Deselect();
        }

        //Debug.Log(selection);

        if (!targetDevice.isValid)
        {
            TryInitialize();
        }

        if (RightHandPresence.GetComponent<HandPresence>().RightThumbStickOnClickBool())
        {
            parentCanvas.GetComponent<Canvas>().enabled = true;
        }

        //This will be when the user selects a face
        if (!RightHandPresence.GetComponent<HandPresence>().RightThumbStickOnTouchBool())
        {
            parentCanvas.GetComponent<Canvas>().enabled = false;
            //It's going to select the face
        }

        /*
        if (RightHandPresence.GetComponent<HandPresence>().RightThumbStickOnClickBool())
        {
            parentCanvas.GetComponent<Canvas>().enabled = true;
        }

        //This will be when the user selects a face
        if (!RightHandPresence.GetComponent<HandPresence>().RightThumbStickOnTouchBool())
        {
            parentCanvas.GetComponent<Canvas>().enabled = false;
            //It's going to select the face
        }
        */

    }


}
