using UnityEngine;

namespace Game
{
    public enum PlayerState
    {
        Idle,
        MoveUp,
        MoveDown,
        StraightHand,
        Kick,
        KnockDown,
    }
    public class GameModel : MonoBehaviour
    {
        [SerializeField] private float playerMoveSpeed = 2f;
        [SerializeField] private float kickDelay = 1f;
        [SerializeField] private float punchDelay = 1f;
        [SerializeField] private float lowPunchDeLay = 1f;
        [SerializeField] private float gotHitDeLay = 1f;
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