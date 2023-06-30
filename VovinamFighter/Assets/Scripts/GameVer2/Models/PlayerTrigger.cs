using UnityEngine;
using UnityEngine.Events;

namespace GameVer2.Models
{
    public class PlayerTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject outGameObject;
        [SerializeField] private UnityEvent<int> onPlayerOut;
        [SerializeField] private int playerValue;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == outGameObject)
            {
                onPlayerOut.Invoke(playerValue);
            }
        }
    }
}