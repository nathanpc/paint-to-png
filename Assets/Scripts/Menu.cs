using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class Menu : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField]
    private GameObject menu;
    [SerializeField]
    private GameObject menun_UI;
    private PlayerMovement PlayerScript = null;
    private Transform UI;
    private Transform placeToBe;
    [SerializeField]
    private GameObject godUI;
    [SerializeField]
    private GameObject script_god;
    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player").name == "Player_PC")
        {
            PlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
        UI = menu.transform;
        placeToBe = GameObject.Find("UI_menu").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (  (Input.GetKeyDown(KeyCode.M) || GetVRXButton()) && isOpen == false && count == 0)
        {
            isOpen = true;
            OpenMenu();
            count = 1;

        }else if ((Input.GetKeyDown(KeyCode.M) || GetVRXButton()) && isOpen == true && count == 0)
        {
            isOpen = false;
            CloseMenu();
            count = 1;
        }
		if (!GetVRXButton()) {
            count = 0;
		}
    }
    void OpenMenu()
    {
        if (godUI.activeSelf)
        {
            script_god.GetComponent<GodCan>().closeGodUI();
        }

        UI.position = placeToBe.position;
        UI.rotation = placeToBe.rotation;

        menu.SetActive(true);
        if (menu.activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(menun_UI);
        }
        if (PlayerScript != null)
        {
            PlayerScript.canWalk = false;
            PlayerScript.canLook = false;
        }
    }
    public void CloseMenu()
    {
        menu.SetActive(false);

        if (PlayerScript != null) {
            PlayerScript.canWalk = true;
            PlayerScript.canLook = true;
        }
        

    }
    private bool GetVRXButton()
    {
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Right | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        bool isXPressed = false;
        foreach (var device in leftHandedControllers)
        {
            if (device.IsPressed(InputHelpers.Button.PrimaryButton, out isXPressed))
                break;
        }

        return isXPressed;
    }
}
