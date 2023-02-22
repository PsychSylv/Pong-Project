using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pong.Unit.Goal
{
    public class GoalController : MonoBehaviour
    {
        [SerializeField] private Collider2D colliderLeft;
        [SerializeField] private Collider2D colliderRight;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;
        private int side;
        
        public event Action<int> GoalScored;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Ball"))
            {
                if (collision.IsTouching(colliderLeft))
                {
                    side = 1;
                }

                else if((collision.IsTouching(colliderRight)))
                {
                    side = 0;                    
                }

                GoalScored?.Invoke(side);
                PlaySFX();
            }
        }

        public void PlaySFX()
        {
            audioSource.PlayOneShot(audioClip);
        }

    }
}
