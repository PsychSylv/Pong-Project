using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Pong.Unit.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject playerOne;
        [SerializeField] private GameObject playerTwo;
        [SerializeField] private Rigidbody2D playerOneRigidbody;
        [SerializeField] private Rigidbody2D playerTwoRigidbody;

        [SerializeField] private float playerSpeed;

        private int gameMode;

        private Vector3 playerOneInitialLocation = new Vector3(-8, 0, 0);
        private Vector3 playerTwoInitialLocation = new Vector3(8, 0, 0);
        private Vector3 ballLocation;
        private Vector3 minimumHeight;
        private Vector3 maximumHeight;
        private bool canMove;

        private void Start()
        {
            canMove = true;
            minimumHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            maximumHeight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        }

        private void Update()
        {
            Move();
        }

        public void Activate()
        {
            canMove = true;
        }

        public void Deactivate()
        {
            canMove = false;
        }

        public void Restart()
        {
            playerOne.transform.position = playerOneInitialLocation;
            playerTwo.transform.position = playerTwoInitialLocation;
        }

        public void GetGameMode(int mode)
        {
            gameMode = mode;
        }

        public void GetBallLocation(Vector3 location)
        {
            ballLocation = location;
        }

        private void Move()
        {
            if (gameMode == 1)
            {
                if (Input.GetKey(KeyCode.UpArrow) && playerTwo.transform.localPosition.y < maximumHeight.y && canMove)
                {
                    playerTwoRigidbody.velocity = Vector2.up * playerSpeed;
                }
                else if (Input.GetKey(KeyCode.DownArrow) && playerTwo.transform.localPosition.y > minimumHeight.y && canMove)
                {
                    playerTwoRigidbody.velocity = Vector2.down * playerSpeed;
                }
                else
                {
                    playerTwoRigidbody.velocity = Vector2.zero;
                }
            }

            else
            {
                if (playerTwo.transform.localPosition.y > ballLocation.y && canMove)
                {
                    playerTwoRigidbody.velocity = Vector2.down * playerSpeed;
                }

                else if (playerTwo.transform.localPosition.y < ballLocation.y && canMove)
                {
                    playerTwoRigidbody.velocity = Vector2.up * playerSpeed;
                }

                else
                {
                    playerTwoRigidbody.velocity = Vector3.zero;
                }
            }

            if (Input.GetKey(KeyCode.W) && playerOne.transform.localPosition.y < maximumHeight.y && canMove)
            {
                playerOneRigidbody.velocity = Vector2.up * playerSpeed;
            }
            else if (Input.GetKey(KeyCode.S) && playerOne.transform.localPosition.y > minimumHeight.y && canMove)
            {
                playerOneRigidbody.velocity = Vector2.down * playerSpeed;
            }
            else
            {
                playerOneRigidbody.velocity = Vector2.zero;
            }

        }
    }
}

