using UnityEngine;
using UnityEngine.Events;

namespace GameVer2.AttackTrigger
{
    public class TriggerAttack : MonoBehaviour
    {
        [SerializeField] private AttackType type;
        [SerializeField] private UnityEvent<AttackType> hitType;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                hitType.Invoke(type);
            }
        }
    }
}