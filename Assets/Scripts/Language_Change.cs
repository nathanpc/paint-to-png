using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language_Change : MonoBehaviour
{
    [SerializeField]
    private Dropdown lang_drop;
    public List<set_text> text;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < text.Capacity; i++)
        {
            text[i].currentText = text[i].Text_gameobject.GetComponent<Text>().text;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    public void Next()
    {
        if (lang_drop.value == 0)
        {
            for (int i = 0; i < text.Capacity; i++)
            {
                text[i].Text_gameobject.GetComponent<Text>().text = text[i].currentText;
            }
        }else if (lang_drop.value == 1)
        {
            for (int i = 0; i < text.Capacity; i++)
            {
                text[i].Text_gameobject.GetComponent<Text>().text = text[i].text;
            }
        }
        
    }
}

[System.Serializable]
public class set_text
{
    [SerializeField]
    public string text;
    [SerializeField]
    public GameObject Text_gameobject;
    public string currentText;
}