using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DemoScene : MonoBehaviour
{

    public GameObject Character;

    private List<string> _charAnimations = new List<string>();
    private int _curAnimPlaying;

	void Awake ()
	{
	    _curAnimPlaying = 0;
	    foreach (AnimationState anim in Character.GetComponent<Animation>())
	    {
	        anim.wrapMode = WrapMode.Loop;
            _charAnimations.Add(anim.name);
	    }

	    Character.GetComponent<Animation>()["sm_run"].speed = 1.25f;
        Character.GetComponent<Animation>()["sm_walk"].speed = 1.25f;
	}

    void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width * 0.5f - 130, 10, 30, 20), "<"))
        {
            SwitchAnim(-1);
        }

        if (GUI.Button(new Rect(Screen.width * 0.5f - 90, 10, 180, 20), "  " + _charAnimations[_curAnimPlaying] + "  "))
        {

        }

        if (GUI.Button(new Rect(Screen.width * 0.5f + 100, 10, 30, 20), ">"))
        {
            SwitchAnim(1);
        }
    }

    void SwitchAnim(int to)
    {
        if (to == -1)
        {
            if (_curAnimPlaying > 0)
                _curAnimPlaying--;
            if (_curAnimPlaying == 0)
                _curAnimPlaying = _charAnimations.Count - 1;
            Character.GetComponent<Animation>().CrossFade(_charAnimations[_curAnimPlaying]);
        }
        else
        {
            if (_curAnimPlaying < _charAnimations.Count - 1)
                _curAnimPlaying++;
            else _curAnimPlaying = 0;
            Character.GetComponent<Animation>().CrossFade(_charAnimations[_curAnimPlaying]);
        }
    }
	
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchAnim(-1);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchAnim(1);
        }
	
	}
}
