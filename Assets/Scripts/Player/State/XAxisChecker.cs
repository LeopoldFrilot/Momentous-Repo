﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame.Player.State
{
    public class XAxisChecker : MonoBehaviour
    {
        GeneralPlayerController PC;
        public void Start()
        {
            PC = GetComponent<GeneralPlayerController>();
        }
        public void Update()
        {
            CheckStickState();
        }
        /* CheckState is a function which manages the grounded and aerial state */
        private void CheckStickState()
        {
            if(PC.CurHorizInput > 0)
            {
                if (PC.IsInLag) return;
                GoRight();
            }
            else if (PC.CurHorizInput < 0)
            {
                if (PC.IsInLag) return;
                GoLeft();
            }
            else
            {
                PC.CurHorizDir = 0;
                PC.PlayerAnimator.SetBool("isRunning", false);
            }
        }

        public void GoRight()
        {
            PC.CurHorizDir = 1;
            if (PC.IsGrounded)
            {
                transform.localScale = new Vector2(PC.PlayerXScale * -1, transform.localScale.y);
                PC.DirFacing = 1;
            }
            PC.PlayerAnimator.SetBool("isRunning", true);
        }

        public void GoLeft()
        {
            PC.CurHorizDir = -1;
            if (PC.IsGrounded)
            {
                transform.localScale = new Vector2(PC.PlayerXScale * 1, transform.localScale.y);
                PC.DirFacing = -1;
            }
            PC.PlayerAnimator.SetBool("isRunning", true);
        }
    }
}

