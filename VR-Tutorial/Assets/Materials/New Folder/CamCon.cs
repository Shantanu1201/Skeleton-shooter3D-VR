using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;


public class CamCon : MonoBehaviour
{
    float local_wait;
    float global_wait;
    public GameObject background_screen;
    public GameObject welcom;
    public GameObject lift;
    public GameObject move;
    public Text outer_var;
    public Text mid_var;
    public bool lock_movement = false;
    private static string str;
    public Animation[] door;
    private float lift_ground_floor = -12.44f;
    private float[] floor_value_add = new float[] {0, 0 };
    private int selected_floor = 0;
    string t;
    private static int current_floor = 0;
    string[] mid_arr = new string[] {"Ground Floor", "1st Floor", "2nd Floor", "3rd Floor", "4th Floor", "5th Floor", "6th Floor", "7th Floor", "8th Floor", "9th Floor", "10th Floor", "Cancel", "", null };
    string[] outer_arr = new string[] {"Welcome to\n      Virtua Tour", "Floor:\n\n\n\nPress joystick to select", "", "\n\nMovinng to Seleted floor", "Canceled", "Moved to Selected floor"};
    private float[] CamCon_floor_arr = new float[] { 2.972f, 15.42f, 27.566f, 40.17f, 52.77f, 65.377f, 77.97f, 90.58f, 103.18f, 115.78f, 128.43f};
    private float[] move_floor_arr = new float[] {0, 0, 12.15f, 24.75f, 37.35f, 49.955f, 62.55f, 75.15f, 87.75f, 100.355f, 112.96f};
    private float[] stair_correction = new float[] { 4.4f, 4.4f, 0, -0.06f, -0.3f, 0,0,0,0,0,0};
    private int[] stair_no = new int[] { 0, 1, 1, 2, 3, 4, 5,6,7,8,9,10 };
    public GameObject[] stairs;


    private void Start()
    {
        Display_text_status(false, 2);        
    }

 

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.name == "border")
        {
            Lift_door("open");
            Debug.Log("border");
            lock_movement = false;
        }

        if (other.gameObject.name == "inside")
        {
            //str = "Welcome to\n      Virtua Tour\n\nMove your joystick to select floor, and press to confirm.";
            Display_text_status(true, 1);
            lock_movement = true;
            Debug.Log("triggered with: " + other.gameObject.name);
        }

        if (other.gameObject.name == "Wellcome")
        {
            Display_text_status(true, 0);
        }
    }



    private void OnTriggerExit(Collider other2)
    {
        Debug.Log("stay in trigger" + other2.gameObject.name);
        if (other2.gameObject.name == "border")
        {
            Lift_door("close");
            Debug.Log("border");
            lock_movement = false;
        }

        if (other2.gameObject.name == "Wellcome")
        {
            Display_text_status(false, 2);
            mid_var.text = outer_arr[2];
        }
    }
    


    int i = 0;
    private void Update()
    {
        if (global_wait < Time.time)
        {

            if ((lock_movement) && (local_wait < Time.time))
            {
                t = LoadEncodedFile();                                
                float h = (t[1] - 53);
                
                if (h > 0.7f && i <= 11)
                {
                    local_wait = Time.time + 0.7f;                    
                    if (i < 11)
                        i++;
                    mid_var.text = mid_arr[i];
                    Debug.Log(i);
                }
                if (h < -0.7f && i >= 0)
                {                                        
                    local_wait = Time.time + 0.7f;
                    if (i > 0)
                        i--;
                    mid_var.text = mid_arr[i];
                    Debug.Log(i);
                }
                if ((t[2] - 48) == 1)
                {
                    //Debug.Log("button is pressed");
                    selected_floor = i;
                    //Debug.Log(selected_floor);
                    if (selected_floor != 11)
                    {
                        Display_text_status(true, 3);
                        mid_var.text = mid_arr[12];
                        Elevate();
                        Display_text_status(true, 5);
                        global_wait = Time.time + 0.7f;                        
                    }                        
                    else
                        Display_text_status(true, 4);
                    lock_movement = false;
                    global_wait = Time.time + 0.7f;
                    
                }
            }
        }
    }



    /*--------------------------------------------------------------------------------------------------*/


    private void Display_text_status(bool stat, int constant_text_no)
    {
        background_screen.SetActive(stat);
        if (stat)
            outer_var.text = outer_arr[constant_text_no];
        else
            outer_var.text = outer_arr[2];
    }



    void Lift_door(string st)
    {
        if (st == "close")
        {
            door[0].Play("Right_close_lift");
            door[1].Play("Left_close_lift");
        }
        if (st == "open")
        {
            door[0].Play("Right_open_lift");
            door[1].Play("Left_open_lift");
        }
    }



    public string LoadEncodedFile()
    {
        string path = Application.persistentDataPath;
        string s = File.ReadAllText(path + "/" + "Move" + ".txt");
        return (s);
    }



    void Elevate()
    {
        if (selected_floor == current_floor)
        {
            lock_movement = true;
            outer_var.text = "You have selected same floor";            
        }
        else
        {
            if(selected_floor == 0)
            {
                move.transform.position = new Vector3(move.transform.position.x, move_floor_arr[1], move.transform.position.z);
                lift.transform.position = new Vector3(lift.transform.position.x, lift_ground_floor ,lift.transform.position.z);
                stairs[0].transform.position = new Vector3(stairs[0].transform.position.x, stairs[0].transform.position.y, stair_correction[stair_no[0]]);
                this.transform.position = new Vector3(this.transform.position.x, CamCon_floor_arr[0] ,this.transform.position.z);
            }

            else
            {
                if (lift.transform.localPosition.y != 0)
                {
                    lift.transform.position = new Vector3(0, 0, 0);
                    this.transform.position = new Vector3(this.transform.position.x, CamCon_floor_arr[1], this.transform.position.z);
                    //Debug.Log("starting co");
                    //StartCoroutine(Waiting());
                    //Debug.Log("end co");
                }

                move.transform.localPosition = new Vector3(move.transform.localPosition.x, move_floor_arr[selected_floor], move.transform.localPosition.z);
                this.transform.position = new Vector3(this.transform.position.x, CamCon_floor_arr[selected_floor], this.transform.position.z);
                stairs[stair_no[selected_floor]].transform.localPosition = new Vector3(stairs[stair_no[selected_floor]].transform.localPosition.x, stairs[stair_no[selected_floor]].transform.localPosition.y, stair_correction[stair_no[selected_floor]]);

            }

            current_floor = selected_floor;
        }
        
    }

}