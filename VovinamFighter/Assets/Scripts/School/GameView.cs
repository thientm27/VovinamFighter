using UnityEngine;

namespace School
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject loadGamePopup;

        public void ShowGameZonePopup(bool isActive = true)
        {
            loadGamePopup.SetActive(isActive);
        }
    }
}