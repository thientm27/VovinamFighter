using UnityEngine;

namespace Game
{
    [System.Serializable]
    public enum AttackType
    {
        Punch,
        Kick,
        LowPunch,
    }
    public class GameModel : MonoBehaviour
    {
        [SerializeField] private float playerMoveSpeed = 2f;
        [SerializeField] private float kickDelay = 0.8f;
        [SerializeField] private float punchDelay = 0.55f;
        [SerializeField] private float lowPunchDeLay = 0.45f;
        [SerializeField] private float gotHitDeLay = 0.3f;
        [SerializeField] private float jumpDelay = 1f;
        [SerializeField] private float hookDelay = 1f;


        public float PlayerMoveSpeed => playerMoveSpeed;

        public float PlayerMoveSpeed1 => playerMoveSpeed;

        public float KickDelay => kickDelay;

        public float PunchDelay => punchDelay;
        public float LowPunchDeLay => lowPunchDeLay;
        public float GotHitDeLay => gotHitDeLay;

        public float JumpDelay => jumpDelay;

        public float HookDelay => hookDelay;
    }
}