using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pong.UI.GameOver
{
    public class GameOverPopupController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        public event Action playAgainPressed;
        public event Action quitPressed;

        public void PlayAgainUBEvent()
        {
            playAgainPressed?.Invoke();
        }

        public void QuitUBEvent()
        {
            quitPressed?.Invoke();
        }

        public void PlaySFX()
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
}
