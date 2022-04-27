using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace carPrototype.InputManager
{
    public class Control
    {
        public static float HorizontalAxis()
        {
            //this has to be an axis left to right
            return Input.GetAxis("Horizontal");
        }

        public static bool Gas()
        {
            //this has to be an on/off push button
            return Input.GetKey(KeyCode.W);
        }

        public static bool Brake()
        {
            //this has to be an on/off push button
            return Input.GetKey(KeyCode.S);
        }
    }

}