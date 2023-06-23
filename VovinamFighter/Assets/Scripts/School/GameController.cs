using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace School
{
    public class GameController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(Constants.SceneGamePlay);
            }
        }
    }
}