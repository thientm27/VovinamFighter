using UnityEngine;
using UnityEngine.Events;

namespace School.Model
{
    public class PlayerTrigger : MonoBehaviour
    {
        [Header("Game State")]
        [SerializeField] private GameObject gameZone;
        [SerializeField] private UnityEvent onTriggerOpenGameZone;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == gameZone)
            {
                onTriggerOpenGameZone?.Invoke();
            }
        }
    }
}