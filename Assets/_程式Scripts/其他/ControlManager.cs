using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    public Text[] buttons = new Text[4];
    private readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));
    public PlayerBasicAction player;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBasicAction>();
        buttons[0].text = player.up.ToString();
        buttons[1].text = player.down.ToString();
        buttons[2].text = player.left.ToString();
        buttons[3].text = player.right.ToString();
    }


    void Update()
    {
        
    }
    
    public void ChangeConrol(int index)
    {
        //Event e = Event.current;
        //if (e.isKey)
        //{
        //    switch (index)
        //    {
        //        case 0:
        //            player.up = e.keyCode;
        //            buttons[0].text = e.keyCode.ToString();
        //            break;
        //        case 1:
        //            player.down = e.keyCode;
        //            break;
        //        case 2:
        //            player.left = e.keyCode;
        //            break;
        //        case 3:
        //            player.right = e.keyCode;
        //            break;
        //    }
        //}
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    switch (index)
                    {
                        case 0:
                            player.up = keyCode;
                            buttons[0].text = keyCode.ToString();
                            break;
                        case 1:
                            player.down = keyCode;
                            buttons[1].text = keyCode.ToString();
                            break;
                        case 2:
                            player.left = keyCode;
                            buttons[2].text = keyCode.ToString();
                            break;
                        case 3:
                            player.right = keyCode;
                            buttons[3].text = keyCode.ToString();
                            break;
                    }
                    break;
                }
            }
        }
    }
}
