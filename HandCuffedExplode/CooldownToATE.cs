using UnityEngine;

namespace HandCuffedExplode
{
    class CooldownToATE : MonoBehaviour
    {
        private float timer = 0f;
        private readonly float timeIsUp = 1.0f;
        public float cooldown = Global.cooldown_to_ate;

        public void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeIsUp)
            {
                timer = 0f;
                cooldown -= timeIsUp;
            }
            if (cooldown <= 0f)
            {
                Destroy(gameObject.GetComponent<CooldownToATE>());
            }
        }
    }
}