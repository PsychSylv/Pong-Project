using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pong.Manager;

namespace Pong.UI.Controller
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;

        [SerializeField] private Text playerOneScoreText;
        [SerializeField] private Text playerTwoScoreText;

        private int resetScore = 0;

        private void Start()
        {
            gameManager.ScoreChanged += OnScoreChanged;
            gameManager.ScoreReseted += OnScoreReseted;
        }

        private void OnScoreReseted()
        {
            playerOneScoreText.text = resetScore.ToString();
            playerTwoScoreText.text = resetScore.ToString();
        }

        private void OnScoreChanged(int playerOneScore, int playerTwoScore)
        {
            playerOneScoreText.text = playerOneScore.ToString();
            playerTwoScoreText.text = playerTwoScore.ToString();
        }
    }
}
