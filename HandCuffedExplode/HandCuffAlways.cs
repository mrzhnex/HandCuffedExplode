using System;
using System.Collections.Generic;
using UnityEngine;
using Smod2.API;
using Smod2;
using RemoteAdmin;

namespace HandCuffedExplode
{
    class HandCuffAlways : MonoBehaviour
    {
        private float timer = 0f;

        public void Start()
        {

        }

        public void Update()
        {
            timer = timer + Time.deltaTime;
            if (timer > 3f)
            {
                timer = 0f;
                foreach (Player p in global.players)
                {
                    if (p.IsHandcuffed()) //добавить провеку на Contains(previos)
                    {
                        GameObject gameobj = p.GetGameObject() as GameObject;
                        if (!global.defaultOwners.Contains(gameobj))
                        {
                            if (gameobj.GetComponent<Handcuffs>() != null)
                            {
                                global.plugin.Info("get component");
                                global.defaultOwners.Add(gameobj);
                                global.plugin.Info("none");
                                gameobj.AddComponent<defaultCheckDistance>();
                            }
                        }

                    }
                }
            }
        }

    }
}
