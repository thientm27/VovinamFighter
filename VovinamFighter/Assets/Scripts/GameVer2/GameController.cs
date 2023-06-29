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
            view.UpdatePlayerScore(0,    playerScore[0]);
            view.UpdatePlayerScore(1,    playerScore[1]);
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
            view.UpdatePlayerScore(playerGet,    playerScore[playerGet]);
        }
        public void OnPlayerOut(int playerGet)
        {
            playerScore[playerGet == 0 ? 1 : 0]++; // add score for opponient
            // view??
            player1.IsPause = true;
            player2.IsPause = true;
            StartCoroutine(RestartGame());
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
                view.StartTimerGame(model.Mathtime, null);
            }
          
        }
        
    }
}