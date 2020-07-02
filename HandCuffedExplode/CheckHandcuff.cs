using UnityEngine;
using EXILED.Extensions;

namespace HandCuffedExplode
{
    public class CheckHandcuff : MonoBehaviour
    {
        private float timer = 0f;
        private readonly float timeIsUp = 0.25f;
        public Vector3 handcufflocation;
        public Vector3 handcufflocation_teleport;
        private ReferenceHub target;
        public GameObject owner;
        public string badge;
        public string color;
        public bool adminUsage = false;

        public void Start()
        {
            target = Player.GetPlayer(gameObject);
            handcufflocation_teleport = target.GetPosition();
            handcufflocation = target.GetPosition();
            target.ClearBroadcasts();
            target.Broadcast(10, "<color=#ff0000>Вы чувствуете, что вас привязали к поверхности/объекту</color>", true);
        }

        public void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeIsUp)
            {
                timer = 0f;

            }
            if (Vector3.Distance(transform.position, handcufflocation) > Global.distance_to_stay_cuff)
            {
                target.SetPosition(handcufflocation_teleport);
            }
        }

        public void OnDestroy()
        {
            target.ClearBroadcasts();
            target.Broadcast(10, "<color=#42aaff>Вы чувствуете, что можете свободно двигаться</color>", true);
        }
    }
}