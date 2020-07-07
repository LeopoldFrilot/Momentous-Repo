﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FightingGame.Player.State
{
    public class GroundedChecker : MonoBehaviour
    {
        GeneralPlayerController PC;
        Rigidbody2D rb;
        int bounce = 0;
        int maxBounces = 3;
        Vector2 prevVel;
        Vector2 curVel;
        public void Start()
        {
            PC = GetComponent<GeneralPlayerController>();
            rb = GetComponent<Rigidbody2D>();
        }
        public void Update()
        {
            prevVel = curVel;
            curVel = rb.velocity;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                if (PC.LagType == "hit")
                {
                    //Debug.Log("Bounce");
                    Bounce();
                }
                else
                {
                    //Debug.Log(PC.Player.name);
                    SetGrounded();
                }
            }
        }
        public void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Ground")
            {
                SetAirborne();
            }
        }
        private void Bounce()
        {
            bounce++;
            float bounceScalar = .5f / bounce;
            rb.velocity = new Vector2(prevVel.x * bounceScalar, prevVel.y * -1 * bounceScalar);
            if (bounce >= maxBounces) SetGrounded();
        }
        private void SetGrounded()
        {
            if (PC.IsGrounded == false)
            {
                PC.GroundedReset();
                bounce = 0;
                if (PC.IsInLag)
                {
                    PC.Lag(PC.CD.LagHardLand, "landing");
                }
                else
                {
                    PC.Lag(PC.CD.LagNormalLand, "landing");
                }
            }
            PC.IsGrounded = true;
            PC.PlayerAnimator.SetBool("isAirborne", false);
            //Debug.Log("Grounded");
        }
        private void SetAirborne()
        {
            PC.IsGrounded = false;
            PC.PlayerAnimator.SetBool("isAirborne", true);
            //Debug.Log("State: Aerial");
        }
    }
}

