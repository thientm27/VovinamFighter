using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameVer2
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameView view;
        [SerializeField] private GameModel model;
        [SerializeField] private PlayerController player1;
        [SerializeField] private Player2Controller player2;
        private int[] playerScore;

        private void Start()
        {
            playerScore = new[] { 0, 0 };
            view.UpdatePlayerScore(0, playerScore[0]);
            view.UpdatePlayerScore(1, playerScore[1]);
            player1.IsPause = true;
            player2.IsPause = true;
            StartCoroutine(RestartGame(true));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(Constants.SceneSchool);
            }
        }

        public void OnGetScored(int playerGet)
        {
            playerScore[playerGet]++;
            view.UpdatePlayerScore(playerGet, playerScore[playerGet]);
            CheckWinner();
        }

        public void OnPlayerOut(int playerGet)
        {
            playerScore[playerGet == 0 ? 1 : 0]++; // add score for opponient
            // view??
            view.UpdatePlayerScore(playerGet == 0 ? 1 : 0, playerScore[playerGet == 0 ? 1 : 0]);
            StartCoroutine(HandleOut(playerGet));
        }

        private IEnumerator RestartGame(bool isFirst = false)
        {
            view.ShowStartGamePopup();
            yield return new WaitForSeconds(4);
            player1.IsPause = false;
            player2.IsPause = false;
            player1.ResetPos();
            player2.ResetPos();
            view.ShowStartGamePopup(false);
            if (isFirst)
            {
                view.StartTimerGame(model.Matchtime, OnTimeOut);
            }
        }

        private void CheckWinner()
        {
            if (playerScore[0] >= model.MatchWinScore)
            {
                player1.IsPause = true;
                player2.IsPause = true;
                StartCoroutine(HandleEndGame("Player 1 Win"));
            }
            else if (playerScore[1] >= model.MatchWinScore)
            {
                player1.IsPause = true;
                player2.IsPause = true;
                StartCoroutine(HandleEndGame("Player 2 Win"));
            }
        }

        private IEnumerator HandleEndGame(string message)
        {
            view.ShowWinner(message);
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(Constants.SceneSchool);
        }
        
        private IEnumerator HandleOut(int playerOut)
        {
            player1.IsPause = true;
            player2.IsPause = true;
            player1.ResetPos();
            player2.ResetPos();
            view.ShowWinner("Player " + (playerOut == 0 ? 1 : 2) + " out", true);
            yield return new WaitForSeconds(2);
            view.ShowWinner("", false);
            StartCoroutine(RestartGame());
        }

        private void OnTimeOut()
        {
            player1.IsPause = false;
            player2.IsPause = false;
            if (playerScore[0] > playerScore[1])
            {
                StartCoroutine(HandleEndGame("Player 1 Win"));
            }else if (playerScore[0] < playerScore[1])
            {
                StartCoroutine(HandleEndGame("Player 2 Win"));
            }
            else
            {
               StartCoroutine(HandleEndGame("Tie"));
            }
        }
    }
}