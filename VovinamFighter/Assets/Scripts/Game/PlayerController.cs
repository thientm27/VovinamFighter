using System;
using DefaultNamespace;
using UnityEngine;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameModel model;
        [SerializeField] private GameView view;
        [SerializeField] private Transform playerOne;
        [SerializeField] private Transform playerTwo;
        [SerializeField] private PlayerAnimationController playerAnimationController;
        [SerializeField] private PlayerAnimationController playerTwoAnimationController;

        [SerializeField] public PlayerState playerOneState;
        [SerializeField] public PlayerState playerTwoState;

        private float _timer;
        private float _timer2;
        
        private float _horizontal = 0;
        private float _horizontal2 = 0;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                playerOneState = PlayerState.KnockDown;
            }
            switch (playerOneState)
            {
                case PlayerState.Idle:
                    HandleInput(); // get input for move if idle
                    break;
                case PlayerState.MoveUp:
                    PlayerOneMove();
                    break;
                case PlayerState.MoveDown:
                    PlayerOneMove();
                    break;
                case PlayerState.StraightHand:
                    
                    AttackStraightHand();
                    break;
                case PlayerState.Kick:
                    
                    AttackKick();
                    break;
                case PlayerState.KnockDown:
                    
                    playerAnimationController.ChangeAnimation(PlayerState.KnockDown);
                    break;
            }
            
            switch (playerTwoState)
            {
                case PlayerState.Idle:
                    HandleInputTwo(); // get input for move if idle
                    break;
                case PlayerState.MoveUp:
                    PlayerTwoMove();
                    break;
                case PlayerState.MoveDown:
                    PlayerTwoMove();
                    break;
                case PlayerState.StraightHand:
                    
                    AttackStraightHandTwo();
                    break;
                case PlayerState.Kick:
                    
                    AttackKickTwo();
                    break;
                case PlayerState.KnockDown:
                    
                    playerAnimationController.ChangeAnimation(PlayerState.KnockDown);
                    break;
            }
        }

        private void HandleInput()
        {
            playerAnimationController.ChangeAnimation(PlayerState.Idle);
            _horizontal = Input.GetAxisRaw(Constants.Horizontal);
            _timer = 0f;
            
            // player move
            if (Mathf.Abs(_horizontal) > 0)
            {
                playerOneState = _horizontal > 0
                    ? PlayerState.MoveUp
                    : PlayerState.MoveDown;
                return;
            }
            
            // player attack
            var input = Input.GetAxisRaw(Constants.StraightHand);
            if (input > 0)
            {
                playerOneState = PlayerState.StraightHand;
                return;
            }

            // player attack2
            var input2 = Input.GetAxisRaw(Constants.Kick);
            if (input2 > 0)
            {
                playerOneState = PlayerState.Kick;
                return;
            }

        }

        private void HandleInputTwo()
        {
            playerTwoAnimationController.ChangeAnimation(PlayerState.Idle);
            _horizontal2 = Input.GetAxisRaw(Constants.HorizontalTwo);
            _timer2 = 0f;
            
            // player move
            if (Mathf.Abs(_horizontal2) > 0)
            {
                playerTwoState = _horizontal2 > 0
                    ? PlayerState.MoveUp
                    : PlayerState.MoveDown;
                return;
            }
            
            // player attack
            var input = Input.GetAxisRaw(Constants.StraightHandTwo);
            if (input > 0)
            {
                playerTwoState = PlayerState.StraightHand;
                return;
            }

            // player attack2
            var input2 = Input.GetAxisRaw(Constants.KickTwo);
            if (input2 > 0)
            {
                playerTwoState = PlayerState.Kick;
                return;
            }

        }
        private void PlayerOneMove()
        {
            _horizontal = Input.GetAxisRaw(Constants.Horizontal);
            var newPos = playerOne.position;
            newPos.x += _horizontal * Time.deltaTime * model.PlayerMoveSpeed;
            if (Mathf.Abs(_horizontal) > 0)
            {
                playerAnimationController.ChangeAnimation(_horizontal > 0
                    ? PlayerState.MoveUp
                    : PlayerState.MoveDown);
                playerOne.position = newPos;
            }
            else
            {
                playerOneState = PlayerState.Idle;
            }
        }

        private void AttackStraightHand()
        {
            playerAnimationController.ChangeAnimation(PlayerState.StraightHand);
            _timer += Time.deltaTime;
            if (_timer >= 0.5f)
            {
                playerOneState = PlayerState.Idle;
            }
        }
        
        private void AttackKick()
        {
            playerAnimationController.ChangeAnimation(PlayerState.Kick);
            _timer += Time.deltaTime;
            if (_timer >= 0.83f)
            {
                playerOneState = PlayerState.Idle;
            }
        }
         private void PlayerTwoMove()
        {
            _horizontal2 = Input.GetAxisRaw(Constants.HorizontalTwo);
            var newPos = playerTwo.position;
            newPos.x += _horizontal2 * Time.deltaTime * model.PlayerMoveSpeed;
            if (Mathf.Abs(_horizontal2) > 0)
            {
                playerTwoAnimationController.ChangeAnimation(_horizontal2 > 0
                    ? PlayerState.MoveUp
                    : PlayerState.MoveDown);
                playerTwo.position = newPos;
            }
            else
            {
                playerTwoState = PlayerState.Idle;
            }
        }

        private void AttackStraightHandTwo()
        {
            playerTwoAnimationController.ChangeAnimation(PlayerState.StraightHand);
            _timer2 += Time.deltaTime;
            if (_timer2 >= 0.5f)
            {
                playerTwoState = PlayerState.Idle;
            }
        }
        
        private void AttackKickTwo()
        {
            playerTwoAnimationController.ChangeAnimation(PlayerState.Kick);
            _timer2 += Time.deltaTime;
            if (_timer2 >= 0.83f)
            {
                playerTwoState = PlayerState.Idle;
            }
        }
        
    }
}