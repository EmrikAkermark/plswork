using UnityEngine;

namespace _Scripts.Player.Projectiles
{
    public class WavegunProjectile : Projectile
    {
        public float speed = 4.0f;
        public float amplitude = 8.0f;
        public float frequency = 8.0f;
        
        protected override void Update()
        {
            base.Update();
            var test = Mathf.Cos(age * frequency) * (amplitude * Time.deltaTime);

            transform.Translate(test, speed * Time.deltaTime, 0.0f);
        }
    }
}