using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Activation : MonoBehaviour
{
    public bool activateUI = false;

    [SerializeField] private GameObject cap;
    [SerializeField] private GameObject capRegulator;
    [SerializeField] private GameObject colorPicker;
    [SerializeField] private GameObject AIModeButton;
    [SerializeField] private GameObject Lip;
    [SerializeField] private Color CanColor;

    private float regulatorScale = 1f;


    // Start is called before the first frame update
    void Start()
    {
        activateUI = false;
        capRegulator.SetActive(false);
        colorPicker.SetActive(false);
        AIModeButton.SetActive(false);
        cap.GetComponent<Outline>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (activateUI)
        {
            startActivation();
        }
    }

    private void startActivation()
    {
        capRegulator.SetActive(true);
        colorPicker.SetActive(true);
        AIModeButton.SetActive(true);
    }
    private void activateAIMode()
    {
        cap.GetComponent<Outline>().enabled = true;
    }

}
