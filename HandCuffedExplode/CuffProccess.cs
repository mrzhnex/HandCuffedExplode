using UnityEngine;
using RemoteAdmin;
using EXILED.Extensions;

namespace HandCuffedExplode
{
    class CuffProccess : MonoBehaviour
    {
        public float proccess = Global.time_to_ate;
        private float timer = 0f;
        private float timeIsUp = 1f;
        private ReferenceHub cuffer;
        public Vector3 targetstartpos;
        public ReferenceHub target;

        public void Start()
        {
            cuffer = Player.GetPlayer(gameObject);
        }

        public void Update()
        {
            timer += Time.deltaTime;
            if (timer >= timeIsUp)
            {
                timer = 0f;
                proccess -= timeIsUp;
                if (cuffer.GetCurrentItem().id != ItemType.Disarmer)
                {
                    cuffer.ClearBroadcasts();
                    cuffer.Broadcast(10, "<color=#228b22>" + Global._havenothandcuff + "</color>", true);
                    Destroy(gameObject.GetComponent<CuffProccess>());
                }
            }

            if (proccess <= 0)
            {
                if (cuffer.GetCurrentItem().id != ItemType.Disarmer)
                {
                    cuffer.ClearBroadcasts();
                    cuffer.Broadcast(10, "<color=#228b22>" + Global._havenothandcuff + "</color>", true);
                    Destroy(gameObject.GetComponent<CuffProccess>());
                }
                foreach (GameObject gameobj in PlayerManager.players)
                {
                    if (gameobj.GetComponent<CheckHandcuff>() != null)
                    {
                        if (gameobj.GetComponent<CheckHandcuff>().owner == gameObject)
                        {
                            cuffer.ClearBroadcasts();
                            cuffer.Broadcast(10, "<color=#228b22>" + Global._havenothandcuff + "</color>", true);
                            Destroy(gameObject.GetComponent<CuffProccess>());
                        }
                    }
                }
                if (target.gameObject.GetComponent<CheckHandcuff>() != null)
                {
                    cuffer.ClearBroadcasts();
                    cuffer.Broadcast(10, "<color=#228b22>" + Global._alreadyhandcuffed + "</color>", true);
                    Destroy(gameObject.GetComponent<CuffProccess>());
                }
                else
                {
                    target.gameObject.AddComponent<CheckHandcuff>();
                    target.gameObject.GetComponent<CheckHandcuff>().owner = gameObject;
                    cuffer.ClearBroadcasts();
                    cuffer.Broadcast(10, "<color=#ff0000>" + Global._successhandcuff + target.nicknameSync.Network_myNickSync + "</color>", true);
                }
                Destroy(gameObject.GetComponent<CuffProccess>());
            }

            if (Vector3.Distance(targetstartpos, target.GetPosition()) > Global.distance_to_stay_cuff)
            {
                cuffer.ClearBroadcasts();
                cuffer.Broadcast(10, "<color=#228b22>" + Global._targetismoving + "</color>", true);
                Destroy(gameObject.GetComponent<CuffProccess>());
            }
            if (Vector3.Distance(gameObject.transform.position, target.GetPosition()) > Global.distance_to_handcuff)
            {
                cuffer.ClearBroadcasts();
                cuffer.Broadcast(10, "<color=#228b22>" + Global._toolong + "</color>", true);
                Destroy(gameObject.GetComponent<CuffProccess>());
            }
        }
    }
}