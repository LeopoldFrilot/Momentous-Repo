﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame.Player.Movement
{
    public class AirDash : MonoBehaviour
    {
        GeneralPlayerController PC;
        Vector3 curPos;
        Vector3 finalPos;
        float step;
        bool isDashing = false;
        public void Start()
        {
            PC = GetComponent<GeneralPlayerController>();
        }
        public void Update()
        {
            AirDashCheck();
            if (isDashing)
            {
                Move();
            }
        }


        /* AirDashCheck is a function which moves the character completely horizontally
        * and lag for a few frames */
        private void AirDashCheck()
        {
            if (PC.IsGrounded == false)
            {
                if (Input.GetButtonDown("Dash"))
                {
                    Dash();
                }
            }
        }
        // This needs WORK but it works for now. We don't want the dash to be so instant
        private void Dash()
        {
            // Should AirDash cancel lag? currently: no
            if (PC.IsInLag)
            {
                //return;
                PC.Lag(0);
            }
            if (PC.MidairOptionsCount < PC.CD.MaxMidairOptions && PC.CurHorizDir != 0)
            {
                PC.Lag(PC.CD.LagAirDash);
                ChooseDirection();
                PC.PlayerAnimator.SetTrigger("AIRDASH");
                curPos = PC.Player.transform.position;
                finalPos = PC.Player.transform.position + PC.CurHorizDir * new Vector3(PC.CD.AirDashDist * PC.Momentum, 0, 0);
                step = Mathf.Abs(curPos.x - finalPos.x) / PC.CD.LagAirDash;
                PC.MidairOptionsCount++;
                isDashing = true;
            }
        }
        private void Move()
        {
            if (Mathf.Abs(curPos.x - finalPos.x) < Mathf.Epsilon)
            {
                isDashing = false;
            }
            PC.Player.transform.position = Vector3.MoveTowards(curPos, finalPos, step);
            curPos = PC.Player.transform.position;
        }
        private void ChooseDirection()
        {
            if(PC.CurHorizDir > 0)
            {
                PC.Player.transform.localScale = new Vector2(PC.PlayerXScale * -1, PC.Player.transform.localScale.y);
                PC.DirFacing = 1;
            }
            else
            {
                PC.Player.transform.localScale = new Vector2(PC.PlayerXScale * 1, PC.Player.transform.localScale.y);
                PC.DirFacing = -1;
            }
        }
    }
}

