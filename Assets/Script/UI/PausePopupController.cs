using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pong.UI.Pause
{
    public class PausePopupController : MonoBehaviour
    {
        public event Action continuePressed;
        public event Action restartPressed;
        public event Action quitPressed;

        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        public void ContinueUBEvent()
        {
            continuePressed?.Invoke();
        }

        public void RestartUBEvent()
        {
            restartPressed.Invoke();
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
