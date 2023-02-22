using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pong.Unit.Ball;
using Pong.Unit.Player;
using Pong.Unit.Goal;
using Pong.UI.Main;
using Pong.UI.Pause;
using Pong.UI.GameOver;
using System;

namespace Pong.Manager
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private MainMenuController main;
        [SerializeField] private BallController ball;
        [SerializeField] private PlayerController player;
        [SerializeField] private GoalController goal;
        [SerializeField] private PausePopupController pausePopup;
        [SerializeField] private GameOverPopupController gameOverPopup;

        [SerializeField] private GameObject pauseCanvas;
        [SerializeField] private GameObject gameOverCanvas;
        [SerializeField] private GameObject mainMenuCanvas;
        [SerializeField] private GameObject inGameObject;
        [SerializeField] private Transform ballLocation;

        public event Action<int, int> ScoreChanged;
        public event Action ScoreReseted;

        private int previousGoal;
        private int playerOneScore;
        private int playerTwoScore;
        private float delayTime = 2f;
        private bool isPaused;
        private bool canPause;

        private void Start()
        {
            canPause = false;

            goal.GoalScored += OnGoalScored;
            main.onePlayerPressed += OnOnePlayer;
            main.twoPlayerPressed += OnTwoPlayer;
            pausePopup.continuePressed += OnResume;
            pausePopup.restartPressed += OnRestart;
            pausePopup.quitPressed += OnQuitGame;
            gameOverPopup.playAgainPressed += OnRestart;
            gameOverPopup.quitPressed += OnQuitGame;
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && canPause)
            {
                Pause();
            }

            player.GetBallLocation(ballLocation.localPosition);
        }

        private void Initialize()
        {
            mainMenuCanvas.SetActive(false);
            pauseCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            inGameObject.SetActive(true);

            canPause = true;
            isPaused = false;

            playerOneScore = playerTwoScore = 0;
            ScoreReseted?.Invoke();

            ball.Reset();
            player.Restart();
            
            EnableGame();
            EjectBall();
        }
        private void EjectBall()
        {
            ball.Deactivate();
            StartCoroutine(DelayEjectBall());
        }
        IEnumerator DelayEjectBall()
        {
            yield return new WaitForSeconds(delayTime);

            if(isPaused == true)
            {
                StartCoroutine(DelayEjectBall());
            }
            else
            {
                ball.RandomizeDirection(previousGoal);
            }
        }
        private void Pause()
        {
            if (isPaused == false)
            {
                DisableGame();
                isPaused = true;
                pauseCanvas.SetActive(true);
                pausePopup.PlaySFX();
            }

            else
            {
                OnResume();
            }
        }
        private void DisableGame()
        {
            ball.Deactivate();
            player.Deactivate();
        }

        private void EnableGame()
        {
            ball.Activate();
            player.Activate();
        }
        private void GameOverCheck()
        {
            if (playerOneScore == 10 || playerTwoScore == 10)
            {
                DisableGame();
                canPause = false;
                gameOverCanvas.SetActive(true);
                gameOverPopup.PlaySFX();
            }

            else
            {
                ball.Reset();
                EjectBall();
            }
        }

        //Events
        private void OnGoalScored(int goal)
        {
            previousGoal = goal;
            if (goal == 0)
            {
                playerOneScore++;
            }
            else
            {
                playerTwoScore++;
            }

            ScoreChanged?.Invoke(playerOneScore, playerTwoScore);
            GameOverCheck();
        }
        private void OnOnePlayer()
        {
            player.GetGameMode(0);
            Initialize();
        }
        private void OnTwoPlayer()
        {
            player.GetGameMode(1);
            Initialize();
        }
        private void OnResume()
        {
            EnableGame();
            pauseCanvas.SetActive(false);
            isPaused = false;
        }
        private void OnRestart()
        {
            Initialize();   
        }
        private void OnQuitGame()
        {
            canPause = false;

            inGameObject.SetActive(false);
            pauseCanvas.SetActive(false);
            gameOverCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
    }
}
