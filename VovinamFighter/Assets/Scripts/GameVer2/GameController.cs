using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameVer2
{
    public class GameController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(Constants.SceneSchool);
            }
        }
    }
}