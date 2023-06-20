using System;
using System.Collections;
using DefaultNamespace;
using Game;
using UnityEngine;

namespace GameVer2
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Scripts references")]
        [SerializeField] private GameModel model;
        [SerializeField] private GameView view;
        [SerializeField] private Player2Controller otherPlayer;

        [Header("Player controll")]
        [SerializeField] private Transform playerTransform;
        [SerializeField] private Animator animator;
    
        // Hit box
        [Header("HixBox controller")]
        [SerializeField] private GameObject punchTrigger;
        [SerializeField] private GameObject kickTrigger;
        [SerializeField] private GameObject lowPunchTrigger;

        private bool _isIdle = true;

        // Update is called once per frame
        void Update()
        {
            // move
            if (!_isIdle)
            {
                return;
            }

            // Moving
            var _horizontal = Input.GetAxisRaw(Constants.Horizontal);
            if (_horizontal != 0)
            {
                PlayerOneMove(_horizontal);
                return;
            }
            else
            {
                animator.SetBool("MoveUp", false);
                animator.SetBool("MoveDown", false);
            }
        

            if (Input.GetKeyDown(KeyCode.K))
            {
                StartCoroutine(Kick(model.KickDelay));
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                StartCoroutine(Punch(model.PunchDelay));
            }
        
            if (Input.GetKeyDown(KeyCode.L))
            {
                StartCoroutine(LowPunch(model.LowPunchDeLay));
            }
        
            if (Input.GetKeyDown(KeyCode.H))
            {
                StartCoroutine(GotHit(model.GotHitDeLay));
            }
            // ChangeAnimation(PlayerState.Idle);
        }

        private void PlayerOneMove(float _horizontal)
        {
            var newPos = playerTransform.position;
            newPos.x += _horizontal * Time.deltaTime * model.PlayerMoveSpeed;
            // Animation
            if (_horizontal > 0)
            {
                animator.SetBool("MoveUp", true);
            }
            else
            {
                animator.SetBool("MoveDown", true);
            }

            playerTransform.position = newPos;
        }

        private IEnumerator Kick(float delayTime)
        {
            _isIdle = false;
            animator.SetBool("Kick", true);
            yield return new WaitForSeconds(delayTime/2);
            kickTrigger.SetActive(true);
            yield return new WaitForSeconds(delayTime/2);
            kickTrigger.SetActive(false);
            animator.SetBool("Kick", false);
            _isIdle = true;
        }
        private IEnumerator Punch(float delayTime)
        {
            _isIdle = false;
            animator.SetBool("Punch", true);
            yield return new WaitForSeconds(delayTime/2);
            punchTrigger.SetActive(true);
            yield return new WaitForSeconds(delayTime/2);
            punchTrigger.SetActive(false);
            animator.SetBool("Punch", false);
            _isIdle = true;
        }
        private IEnumerator LowPunch(float delayTime)
        {
            _isIdle = false;
            animator.SetBool("LowPunch", true);
            yield return new WaitForSeconds(delayTime/2);
            lowPunchTrigger.SetActive(true);
            yield return new WaitForSeconds(delayTime/2);
            lowPunchTrigger.SetActive(false);
            animator.SetBool("LowPunch", false);
            _isIdle = true;
        }
    
        private IEnumerator GotHit(float delayTime)
        {
            _isIdle = false;
            animator.SetBool("GotHit", true);
            yield return new WaitForSeconds(delayTime);
            animator.SetBool("GotHit", false);
            _isIdle = true;
        }

        public void GotHitByOther()
        {
            StartCoroutine(GotHit(model.GotHitDeLay));
        }

        public void HandleHitEnemy(AttackType type)
        {
            Debug.Log("HITTed");
            otherPlayer.GotHitByOther();
            switch (type)
            {
                case AttackType.Punch:
                    break;
                case AttackType.Kick:
                    break;
                case AttackType.LowPunch:
                    break;

            }
        }
    }
}