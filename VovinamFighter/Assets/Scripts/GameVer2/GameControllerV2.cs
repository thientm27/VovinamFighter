using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Game;
using UnityEngine;

public class GameControllerV2 : MonoBehaviour
{
    [SerializeField] private GameModel model;

    [SerializeField] private GameView view;

    // Start is called before the first frame update
    [SerializeField] private Transform playerOne;

    // Animation
    [SerializeField] private Animator animator;
    [SerializeField] public PlayerState currenPlayer = PlayerState.Idle;

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
        ChangeAnimation(PlayerState.Idle);
    }

    private void PlayerOneMove(float _horizontal)
    {
        var newPos = playerOne.position;
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

        playerOne.position = newPos;
    }

    private IEnumerator Kick(float delayTime)
    {
        _isIdle = false;
        animator.SetBool("Kick", true);
        yield return new WaitForSeconds(delayTime);
        animator.SetBool("Kick", false);
        _isIdle = true;
    }
    private IEnumerator Punch(float delayTime)
    {
        _isIdle = false;
        animator.SetBool("Punch", true);
        yield return new WaitForSeconds(delayTime);
        animator.SetBool("Punch", false);
        _isIdle = true;
    }
    private IEnumerator LowPunch(float delayTime)
    {
        _isIdle = false;
        animator.SetBool("LowPunch", true);
        yield return new WaitForSeconds(delayTime);
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
    private void ChangeAnimation(PlayerState nameAnim)
    {
        if (currenPlayer != nameAnim)
        {
            animator.ResetTrigger(currenPlayer.ToString());
            animator.SetTrigger(nameAnim.ToString());
            currenPlayer = nameAnim;
        }
    }
}