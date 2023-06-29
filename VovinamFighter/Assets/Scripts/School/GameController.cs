using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace School
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameView view;
        [SerializeField] private CharController charController;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(Constants.SceneGamePlay);
            }
        }
        public void OnClickOfflineGame()
        {
            SceneManager.LoadScene(Constants.SceneGamePlay);
        }
        public void OnClickClose()
        {
            charController.ResumeGame();
            view.ShowGameZonePopup(false);
        }

        public void OnTriggerShowLoadGamePopup()
        {
            charController.PauseGame();
            view.ShowGameZonePopup();
        }
    }
    
    
}