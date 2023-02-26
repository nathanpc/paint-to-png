using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;
public class GodCan : MonoBehaviour
{
    private PlayerMovement plyrScpt = null;
    [SerializeField]
    private Menu menuScript;
    private bool CanGrabbed;
    private bool thisOne = false;
    private GameObject can;
    public FlexibleColorPicker picker;
    public GameObject background;
    [SerializeField]
    private GameObject menu_ui;
    [SerializeField]
    private GameObject GodCanUI;

    [SerializeField]
    private Transform UI;
    private Transform placeToBe;
    [SerializeField]
    private GameObject menu_screen;
    [SerializeField]
    private GameObject menu_gameobject;
    public bool GodUI_open = false;

    public bool isHeld = false;
    private int toggle = 0;

    // Start is called before the first frame update
    void Start()
    {
        picker.color = Color.yellow;
        placeToBe = GameObject.FindGameObjectWithTag("go_ui_place").transform;
        if (GameObject.FindGameObjectWithTag("Player").name == "Player_PC")
        {
            plyrScpt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (plyrScpt != null)  // if the game is being played in VR
        {
            CanGrabbed = plyrScpt.IsCanGrabbed;
            can = plyrScpt.currentCan;
            if (can == this.gameObject && CanGrabbed)
            {
                thisOne = true;
            }
            else
            {
                thisOne = false;
            }
            if (thisOne && Input.GetKeyDown(KeyCode.N))
            {
                if (menu_screen.activeSelf)
                {
                    menu_gameobject.GetComponent<Menu>().CloseMenu();
                }
                UI.position = placeToBe.position;
                UI.rotation = placeToBe.rotation;

                menu_ui.SetActive(!menu_ui.activeInHierarchy);
                GodCanUI.SetActive(!GodCanUI.activeInHierarchy);

                plyrScpt.canWalk = !plyrScpt.canWalk;
                plyrScpt.canLook = !plyrScpt.canLook;
            }
            else if (!thisOne)
            {
                GodCanUI.SetActive(false);
            }
            else if (!thisOne && !menuScript.isOpen)
            {
                plyrScpt.canWalk = true;
                plyrScpt.canLook = true;
            }
            background.GetComponent<Image>().color = picker.color;
            this.GetComponent<canScript>().CanColor = picker.color;
        }


        if(isHeld)
        {

            if ( (Input.GetKeyDown(KeyCode.N) || GetVRXButton()) && toggle == 0) 
            {
                toggle = 1;
                if (menu_screen.activeSelf)
                {
                    menu_gameobject.GetComponent<Menu>().CloseMenu();
                }
                UI.position = placeToBe.position;
                UI.rotation = placeToBe.rotation;

                //menu_ui.SetActive(!menu_ui.activeInHierarchy);
                GodCanUI.SetActive(!GodCanUI.activeInHierarchy);


            }

			if (!GetVRXButton()) {
                toggle = 0;
			}

            background.GetComponent<Image>().color = picker.color;
            this.GetComponent<canScript>().CanColor = picker.color;
        }
    }
    public void closeGodUI()
    {
        menu_ui.SetActive(!menu_ui.activeInHierarchy);
        GodCanUI.SetActive(!GodCanUI.activeInHierarchy);
        if (plyrScpt != null)
        {
            plyrScpt.canWalk = !plyrScpt.canWalk;
            plyrScpt.canLook = !plyrScpt.canLook;
        }
     
    }
    public void canGod_selected()
    {
        isHeld = true;
    
   
    }
    public void canGodDiselectd()
    {
        isHeld = false;

        GodCanUI.SetActive(false); 

    }

    private bool GetVRXButton()
    {
        var leftHandedControllers = new List<UnityEngine.XR.InputDevice>();
        var desiredCharacteristics = UnityEngine.XR.InputDeviceCharacteristics.HeldInHand  | UnityEngine.XR.InputDeviceCharacteristics.Controller;
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(desiredCharacteristics, leftHandedControllers);

        bool isXPressed = false;
        foreach (var device in leftHandedControllers)
        {
            if (device.IsPressed(InputHelpers.Button.SecondaryButton, out isXPressed))
                break;
        }

        return isXPressed;
    }
}
