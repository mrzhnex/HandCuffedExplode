using EXILED.Extensions;
using UnityEngine;

namespace HandCuffedExplode
{
    public class TryToATE : MonoBehaviour
    {
        private float timer = 0f;
        private readonly float timeIsUp = 1.0f;
        private float time_to_ate = Global.time_to_ate;
        private ReferenceHub target;
        public void Start()
        {
            target = Player.GetPlayer(gameObject);
        }

        public void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeIsUp)
            {
                timer = 0f;
                time_to_ate = time_to_ate - timeIsUp;
            }
            if (time_to_ate <= 0f)
            {
                Destroy(gameObject.GetComponent<TryToATE>());
            }
        }

        public void OnDestroy()
        {
            gameObject.AddComponent<CooldownToATE>();
            if (Global.rand.Next(0, 10) == 3)
            {
                if (gameObject.GetComponent<CheckHandcuff>() != null)
                {
                    Destroy(gameObject.GetComponent<CheckHandcuff>());
                }
            }
            else
            {
                target.ClearBroadcasts();
                target.Broadcast(10, "<color=#228b22>У вас не вышло развязаться</color>", true);
            }
        }
    }
}