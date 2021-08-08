using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using System.IO;



public class MyMob : MonoBehaviour {

    public GameObject cam;
    float lookh, lookv, h, v, a = 0, b = 0;
    string t;


    void Update()
    {

        lookh = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("Horizontal") * 0.1f;
        lookv = UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxis("Vertical") * 0.1f;
        a = a + lookv * 15;
        b = b + lookh * 15;
        Debug.Log(a);
        Debug.Log(b);
        this.transform.localRotation = Quaternion.Euler(-a, b, 0);


        if (!cam.GetComponent<CamCon>().lock_movement)
        {
            t = LoadEncodedFile();
            Debug.Log(t);
            h = (t[0] - 52) * 20 / 70f;
            v = (t[1] - 52) * 20 / 70f;


            {
                Vector3 forward = transform.forward * v;
                forward.y = 0f;
                Vector3 right = transform.right * h;
                cam.transform.position += forward;
                cam.transform.position += right;
            }
            //cam.transform.Translate(h, 0, v);
        }

    }


    public string LoadEncodedFile()
    {
        string path = Application.persistentDataPath;
        string s = File.ReadAllText(path + "/" + "Move" + ".txt");
        return (s);
    }


}
