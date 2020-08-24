using _Scripts.Player.Projectiles;
using UnityEngine;

namespace _Scripts.Player.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected string projectileTag = "Blaster Projectile";
        [SerializeField] protected float fireRate;
        
        private float lastFired;

        public void TryShoot()
        {
            if (Time.time - lastFired > fireRate)
            {
                lastFired = Time.time;
                Shoot();
            }
        }

        protected void Shoot()
        {
            var proj = ProjectileManager.Instance.GetWithTag(projectileTag);
            if (!proj) return;
         
            var trans = transform;
            proj.transform.position = trans.position;
            proj.transform.rotation = trans.rotation;
            
            proj.SetActive(true);
        }
    }
}