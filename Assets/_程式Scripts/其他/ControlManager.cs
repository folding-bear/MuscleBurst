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
    
    public bool choose { get; set; }
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
        ChangeConrol();
        //Debug.Log(choose);
    }
    
    private void ChangeConrol()
    {
        if (choose)
        {
            if (Input.anyKeyDown)
            {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                //Debug.Log(""+keyCode);
                        player.up = keyCode;
                        buttons[0].text = keyCode.ToString();
                    //    switch (index)
                    //{
                    //    case 0:
                    //        //buttons[0].text.Equals(keyCode.ToString());
                    //        player.up = keyCode;
                    //        buttons[0].text = keyCode.ToString();
                    //        break;
                    //    case 1:
                    //        player.down = keyCode;
                    //        buttons[1].text = keyCode.ToString();
                    //        break;
                    //    case 2:
                    //        player.left = keyCode;
                    //        buttons[2].text = keyCode.ToString();
                    //        break;
                    //    case 3:
                    //        player.right = keyCode;
                    //        buttons[3].text = keyCode.ToString();
                    //        break;
                    //}
                    choose = false;
                    break;
                }
            }
            }
        }

    }
   public void ChooseButton()
    {
       choose = true;
    }
}
