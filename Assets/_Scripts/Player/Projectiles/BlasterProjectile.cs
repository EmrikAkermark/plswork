using UnityEngine;

namespace _Scripts.Player.Projectiles
{
    public class BlasterProjectile : Projectile
    {
        public float speed = 5.0f;

        protected override void Update()
        {
            base.Update();
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
        }
    }
}