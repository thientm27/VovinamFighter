using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace GameVer2
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timerGame;
        [SerializeField] private TextMeshProUGUI player1Score;
        [SerializeField] private TextMeshProUGUI player2Score;
        [SerializeField] private GameObject startGamePopup;

        [Header("Win popup")]
        [SerializeField] private GameObject winPopup;
        [SerializeField] private TextMeshProUGUI winText;

        private int timer;
        private IEnumerator timerCountDown;

       
        public void ShowStartGamePopup(bool isActive = true)
        {
            startGamePopup.SetActive(isActive);
        }

        public void UpdatePlayerScore(int index, int score)
        {
            if (index == 0)
            {
                player1Score.text = score.ToString();
            }
            else
            {
                player2Score.text = score.ToString();
            }
        }

        public void ShowWinner(string message, bool isActive = true)
        {
            winPopup.SetActive(isActive);
            winText.text = message;
        }

        public void StartTimerGame(int gameTime, UnityAction callback)
        {
            timer = gameTime;
            timerGame.text = timer.ToString();
            timerCountDown = StartTimeCount(callback);
            StartCoroutine(timerCountDown);
        }

        public void StopTimer()
        {
            StopCoroutine(timerCountDown);
        }

        private IEnumerator StartTimeCount(UnityAction callback)
        {
         
            while (timer > 0)
            {
                timer--;
                yield return new WaitForSeconds(1);
                timerGame.text = timer.ToString();
                if (timer <= 0)
                {
                    callback?.Invoke();
                }
            }
        }
    }
}