using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (Input.GetKeyDown(KeyCode.M) && isOpen == false)
        {
            isOpen = true;
            OpenMenu();

        }else if (Input.GetKeyDown(KeyCode.M) && isOpen == true)
        {
            isOpen = false;
            CloseMenu();
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

        PlayerScript.canWalk = true;
        PlayerScript.canLook = true;

    }
}
