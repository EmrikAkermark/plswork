using UnityEngine;

namespace _Scripts.Player.Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        protected float age;
        [SerializeField] private float lifetime = 2.0f;

        private void OnEnable()
        {
            age = 0;
        }

        protected virtual void Update()
        {
            age += Time.deltaTime;
            if (age > lifetime)
            {
                gameObject.SetActive(false);
            }
        }
    }
}