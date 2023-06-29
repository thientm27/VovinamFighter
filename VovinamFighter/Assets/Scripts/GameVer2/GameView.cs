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

        private int _timer;
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
        
        public void StartTimerGame(int gameTime, UnityAction callback)
        {
            _timer = gameTime;
            timerGame.text = _timer.ToString();
            timerCountDown = StartTimeCount(callback);
            StartCoroutine(timerCountDown);
        }

        public void StopTimer()
        {
            StopCoroutine(timerCountDown);
        }
        private IEnumerator StartTimeCount( UnityAction callback)
        {
            while (_timer > 0)
            {
                _timer--;
                yield return new WaitForSeconds(1);
                timerGame.text = _timer.ToString();
            }
        }
        
    }
}