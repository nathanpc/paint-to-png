using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{

    [SerializeField] private string[] missions_names = new string[] { "goTo", "delivery", "paint", "clean", "final_mission" };
    public List<Mission_things> Missions;


    private float distance;
    public int currentMissionValue = 0;

    private Texture wall;
    public bool entered = false;

   
    private PlayerMovement playerScrpt;
    private int count_ = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerScrpt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        for (int i = 0; i < Missions.Count; i++)
        {
            Missions[i].Place.SetActive(false);
        }
        AbleObjectives();

       
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Missions[currentMissionValue].Type == missions_names[1])
        {

            deliveryMission();

        }else if (Missions[currentMissionValue].Type == missions_names[4])
        {
            final_task();
        }

        if (Missions[currentMissionValue].isCompleted == true)
        {
            MissionCompleted();
        }
                
    }

    public void paintWall()
    {
        //wall = Missions[currentMissionValue].Objects[0].GetComponent<Renderer>().material.GetTexture("_MainText");

        var mpb = new MaterialPropertyBlock();
        Missions[currentMissionValue].Objects[0].GetComponent<Renderer>().GetPropertyBlock(mpb);
        wall =  mpb.GetTexture("_MainTex");
        RenderTexture wall_ = (RenderTexture) wall;

        Texture2D _wall = toTexture2D(wall_);

        var pix_count = 0;
        //Debug.Log("yyyy");

        for (int x = 0; x < _wall.width; x++)
        {
            for (int y = 0; y < _wall.height; y++)
            {
                Color pix = _wall.GetPixel(x, y);
                if (pix != new Color (0,0,0,1) )
                {
                    pix_count++;
                }
            }
        }
        Debug.Log(pix_count);
        if (pix_count >= 0 )
        {
            MissionCompleted();
            GameObject.FindGameObjectWithTag("block").SetActive(false);
        }
        else
        {
            Debug.Log("NOT");
        }
    }
    private void cleanWall()
    {
        //wall = Missions[currentMissionValue].Objects[0].GetComponent<Renderer>().material.GetTexture("_MainText");

        var mpb = new MaterialPropertyBlock();
        Missions[currentMissionValue].Objects[0].GetComponent<Renderer>().GetPropertyBlock(mpb);
        wall = mpb.GetTexture("_MainTex");
        RenderTexture wall_ = (RenderTexture)wall;

        Texture2D _wall = toTexture2D(wall_);

        var pix_count = 0;

        for (int x = 0; x < _wall.width; x++)
        {
            for (int y = 0; y < _wall.height; y++)
            {
                Color pix = _wall.GetPixel(x, y);
                //Debug.Log(pix);
                if (pix == Color.black)
                {
                    pix_count++;
                }
            }
        }
        Debug.Log(pix_count);
        if (pix_count > 150000)
        {
            MissionCompleted();
           
        }
        else
        {
            Debug.Log("NOT");
        }
    }
    private void deliveryMission()
    {
        //UI_Mission_Stat.text = Missions[currentMissionValue].Name;

        //Instantiate(sphere_prefab, Missions[currentMissionValue].Place);

        Missions[currentMissionValue].Place.SetActive(true);

        var count = 0;
        for(int i = 0; i < Missions[currentMissionValue].Objects.Length; i++)
        {

            enable_outline(Missions[currentMissionValue].Objects[i], true);
            if (checkDistance(Missions[currentMissionValue].Objects[i], Missions[currentMissionValue].Place.transform)
                && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().IsCanGrabbed == false)
            {
                count++;
                enable_outline(Missions[currentMissionValue].Objects[i], false);
            }
        }
        if (count == Missions[currentMissionValue].Objects.Length)
        {
            Missions[currentMissionValue].Place.SetActive(false);
            MissionCompleted();
        }

        //Debug.Log(Missions.Count);

        
    }

    private bool checkDistance(GameObject _object, Transform place)
    {
        distance = Vector3.Distance(_object.transform.position, place.position);

        if (distance < 2.0f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Texture2D toTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    public void MissionCompleted()
    {
        
        Debug.Log("Mission_Completed");
        Missions[currentMissionValue].isCompleted = true;

        Destroy(Missions[currentMissionValue].Place); 
        if (currentMissionValue < Missions.Count - 1  )
        {
            currentMissionValue++;
        }
        else
        {
            TasksCompleted();
        }

        AbleObjectives();
    }
   


    public void OnCollision()
    {

        if ( Missions[currentMissionValue].Type == missions_names[0])
        {
            MissionCompleted();
        }
        else if (Missions[currentMissionValue].Type == missions_names[2] && !entered)
        {
            paintWall();
            entered = true;
        }else if (Missions[currentMissionValue].Type == missions_names[3] && !entered)
        {
            cleanWall();
            entered = true;
        }else if (Missions[currentMissionValue].Type == missions_names[4])
        {
            Missions[currentMissionValue].Place.SetActive(false);
        }

    }
    

    private void AbleObjectives()
    {
        for (int i = 0; i < missions_names.Length; i++)
        {
            if (Missions[currentMissionValue].Type == missions_names[i])
            {
                Missions[currentMissionValue].Place.SetActive(true);
            }
            
        }

    }

    private void enable_outline(GameObject out_object, bool set )
    {
        out_object.GetComponent<canScript>().onMission = set;

        
    }
    private void final_task()
    {
        if (count_ == 0)
        {
            Missions[currentMissionValue].Objects[1].SetActive(false);
            enable_outline(Missions[currentMissionValue].Objects[0], true);
            count_++;
        }
        

        if (playerScrpt.IsCanGrabbed && playerScrpt.currentCan == Missions[currentMissionValue].Objects[0])
        {
            enable_outline(Missions[currentMissionValue].Objects[0], false);
            MissionCompleted();
        }


    }
    private void TasksCompleted()
    {

        this.gameObject.SetActive(false);
    }
}
