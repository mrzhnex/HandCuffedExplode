using UnityEngine;
using EXILED.Extensions;

namespace HandCuffedExplode
{
    public class CheckDistance : MonoBehaviour
    {
        public GameObject owner;
        private float distance = 298;
        private float timer = 0;
        private const float setter = 1.56f;
        public int stage = 0;
        private float distance50 = 0;
        private float distance75 = 0;
        private float distance90 = 0;
        private ReferenceHub target;
        private float saveTimerCheck = 0f;
        private bool saveLock = false;

        public void Start()
        {
            distance = Global.distance;
            distance50 = distance / 100 * 50;
            distance75 = distance / 100 * 75;
            distance90 = distance / 100 * 90;
            target = Player.GetPlayer(gameObject);
            target.ClearBroadcasts();
            target.Broadcast(10, "<color=#ff0000>*вы чувствуете, как вам на руку прикрепили странный браслет*</color>", true);
        }

        public void Update()
        {
            timer += Time.deltaTime;
            if (timer > 1.0f)
            {
                if (saveLock)
                {
                    saveTimerCheck = saveTimerCheck + timer;
                }
                timer = 0f;
                if (Vector3.Distance(transform.position, owner.transform.position) > (distance50 / setter)) //50%
                {
                    if (Vector3.Distance(transform.position, owner.transform.position) > (distance75 / setter)) //25%
                    {
                        if (Vector3.Distance(transform.position, owner.transform.position) > (distance90 / setter)) //10%
                        {
                            if (Vector3.Distance(transform.position, owner.transform.position) > (distance / setter)) //0%
                            {
                                stage = 4;
                                if (Vector3.Distance(transform.position, new Vector3(0f, -2000f, 0f) + Vector3.up * 1.5f) < 1f)
                                {
                                    Destroy(gameObject.GetComponent<CheckDistance>());
                                }
                                else if (Vector3.Distance(owner.transform.position, new Vector3(0f, -2000f, 0f) + Vector3.up * 1.5f) < 1f)
                                {
                                    Destroy(gameObject.GetComponent<CheckDistance>());
                                }
                                else
                                {
                                    if (Global.saveTime == 0)
                                    {
                                        CustomThrowG(gameObject.transform.position, gameObject);
                                        Destroy(gameObject.GetComponent<CheckDistance>());
                                    }
                                    else
                                    {
                                        if (!saveLock)
                                        {
                                            target.ClearBroadcasts();
                                            target.Broadcast(10, "<color=#ff0000>*вы слышите, как пиканье пищит пронзительно и безостановочно*</color>", true);
                                            saveLock = true;
                                            return;
                                        }
                                        else
                                        {
                                            if (saveTimerCheck >= Global.saveTime)
                                            {
                                                CustomThrowG(gameObject.transform.position, gameObject);
                                                Destroy(gameObject.GetComponent<CheckDistance>());
                                            }
                                            else
                                            {
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                saveLock = false;
                                saveTimerCheck = 0f;
                                if (stage != 3)
                                {
                                    stage = 3;
                                    target.ClearBroadcasts();
                                    target.Broadcast(10, "<color=#ff0000>*вы слышите, как пиканье пищит безотрывисто*</color>", true);
                                }
                            }
                        }
                        else
                        {
                            saveLock = false;
                            saveTimerCheck = 0f;
                            if (stage != 2)
                            {
                                stage = 2;
                                target.ClearBroadcasts();
                                target.Broadcast(10, "<color=#228b22>*вы слышите, как пиканье учащается ещё сильнее*</color>", true);
                            }
                        }
                    }
                    else
                    {
                        saveLock = false;
                        saveTimerCheck = 0f;
                        if (stage != 1)
                        {
                            stage = 1;
                            target.ClearBroadcasts();
                            target.Broadcast(10, "<color=#228b22>*вы слышите, как пиканье учащается*</color>", true);
                        }
                    }
                }
                else
                {
                    saveLock = false;
                    saveTimerCheck = 0f;
                    if (stage != 0)
                    {
                        stage = 0;
                        target.ClearBroadcasts();
                    }
                }

            }
        }

        public void CustomThrowG(Vector3 position, GameObject gameObject)
        {
            ExplodeBullets.SetEvent setEvent = new ExplodeBullets.SetEvent();
            setEvent.CustomThrowG(position, gameObject);
        }
    }
}