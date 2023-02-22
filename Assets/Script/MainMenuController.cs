using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pong.UI.Main
{
    public class MainMenuController : MonoBehaviour
    {
        public event Action onePlayerPressed;
        public event Action twoPlayerPressed;

        public void OnePlayerUBEvent()
        {
            onePlayerPressed?.Invoke();
        }
        public void TwoPlayerUBEvent()
        {
            twoPlayerPressed?.Invoke();
        }

    }
}
