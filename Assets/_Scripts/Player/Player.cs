using System.Collections.Generic;
using _Scripts.Player.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Movement Tweaks")] [SerializeField]
        private float maxSpeed = 9.0f;

        [SerializeField] private float maxAccel = 6.0f;
        [SerializeField] private float maxDecel = 15.0f;
        [SerializeField] private float boostMultiplier = 2.0f;
        [SerializeField] private float boostCooldown = 2.0f;
        [SerializeField] private float boostDuration = 1.5f;

        private Vector2 moveInput = new Vector2();
        private bool isShooting;
        private bool isBoosting;
        private float lastBoost;

        private const float ButtonDeadZone = 0.15f;

        private Rigidbody2D rb;
        [SerializeField] List<GameObject> weapons;
        [HideInInspector] private Weapon[] weaponScripts;
        
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            /* @TODO: We probably don't need a weapon script at all anymore?
             Most of it can be rolled into the Player script so that we don't
             have to do this nonsense anymore. Either way, this hacky solution
             is ***VERY TEMPORARY*** */
            weaponScripts = new Weapon[weapons.Count];
            for (int i = 0; i < weapons.Count; i++)
            {
                var component = weapons[i].GetComponent<Weapon>();
                if (!component) component = weapons[i].AddComponent<Weapon>();

                weaponScripts[i] = component;
            }
        }

        private void FixedUpdate()
        {
            if (isBoosting && Time.time - lastBoost > boostDuration) isBoosting = false;

            var effectiveBoost = isBoosting ? boostMultiplier : 1.0f;
            var desiredVelocity = moveInput * (maxSpeed * effectiveBoost);
            var isAccelerating = Vector2.Dot(rb.velocity, desiredVelocity) > 0;
            var accel = isAccelerating ? maxAccel * effectiveBoost : maxDecel * effectiveBoost;

            rb.velocity = Vector2.MoveTowards(rb.velocity, desiredVelocity, accel * Time.fixedDeltaTime);

            if (isShooting)
            {
                foreach (var weaponScript in weaponScripts)
                {
                        weaponScript.TryShoot();
                }
            }
        }

        public void OnShoot(InputValue val)
        {
            isShooting = val.Get<float>() > ButtonDeadZone;
        }

        public void OnMove(InputValue val)
        {
            moveInput = val.Get<Vector2>();
        }

        public void OnBoost(InputValue val)
        {
            if (val.isPressed && Time.time - lastBoost > boostCooldown)
            {
                lastBoost = Time.time;
                isBoosting = true;
            }
        }
    }
}