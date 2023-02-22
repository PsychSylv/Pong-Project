using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong.Unit.Ball
{
    public class BallController : MonoBehaviour
    {
        public Vector2 direction;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        private float randomInitialX;
        private float randomInitialY;
        private float minInitialX = 5;
        private float maxInitialX = 10;
        private float minInitialY = 10;
        private float maxInitialY = -10;
        private Vector2 collisionX = new Vector2(-1, 1);
        private Vector2 collisionY = new Vector2(1, -1);

        public void Reset()
        {
            transform.position = Vector3.zero;
        }

        public void Activate()
        {
            rb.velocity = direction;
        }
        public void Deactivate()
        {
            rb.velocity = Vector2.zero;
        }
        public void RandomizeDirection(int goal)
        {
            randomInitialX = Random.Range(minInitialX, maxInitialX);
            randomInitialY = Random.Range(minInitialY, maxInitialY);

            if (goal == 1)
            {
                randomInitialX *= -1;
            }

            direction = new Vector2(randomInitialX, randomInitialY);
            rb.velocity = direction;
        }

        private void PlaySFX()
        {
            audioSource.PlayOneShot(audioClip);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Wall"))
            {
                direction *= collisionY;
                rb.velocity = direction;
                PlaySFX();
            }

            else if(collision.CompareTag("Player"))
            {
                randomInitialY = Random.Range(minInitialY, maxInitialY);

                direction *= collisionX;
                direction.y = randomInitialY;

                rb.velocity = direction;
                PlaySFX();
            }

            else if(collision.CompareTag("Goal"))
            {
                Reset();
            }
        }
    }
}
