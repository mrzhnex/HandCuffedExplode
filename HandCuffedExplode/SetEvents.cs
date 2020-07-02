using EXILED;
using EXILED.Extensions;
using RemoteAdmin;
using UnityEngine;

namespace HandCuffedExplode
{
    public class SetEvents
    {
        public void OnCallCommand(ConsoleCommandEvent ev)
        {
            if (!Global.can_use_commands)
            {
                ev.ReturnMessage = "Дождитесь начала раунда!";
                return;
            }
            string command = ev.Command.Split(new char[]
            {
                ' '
            })[0].ToLower();
            if (command == "cuff")
            {
                if (Global.plugin_enable)
                {
                    if (ev.Player.GetCurrentItem().id != ItemType.Disarmer)
                    {
                        ev.ReturnMessage = Global._havenothandcuff;
                        return;
                    }
                    foreach (GameObject gameobj in PlayerManager.players)
                    {
                        if (gameobj.GetComponent<CheckHandcuff>() != null)
                        {
                            if (gameobj.GetComponent<CheckHandcuff>().owner == ev.Player.gameObject)
                            {
                                ev.ReturnMessage = Global._havenothandcuff;
                                return;
                            }
                        }
                    }
                    ReferenceHub target = null;
                    if (Physics.Raycast((ev.Player.gameObject.GetComponent<Scp049PlayerScript>().plyCam.transform.forward * 1.01f) + ev.Player.gameObject.transform.position, ev.Player.gameObject.GetComponent<Scp049PlayerScript>().plyCam.transform.forward, out RaycastHit hit, Global.distance_to_handcuff))
                    {
                        if (hit.transform.GetComponent<QueryProcessor>() == null)
                        {
                            ev.ReturnMessage = Global._notlook;
                            return;
                        }
                        else
                        {
                            target = Player.GetPlayer(hit.transform.gameObject);
                            if (target.GetTeam() == Team.SCP)
                            {
                                ev.ReturnMessage = Global._isscp;
                                return;
                            }
                            if (target.gameObject.GetComponent<CheckHandcuff>() != null)
                            {
                                ev.ReturnMessage = Global._alreadyhandcuffed;
                                return;
                            }
                            else
                            {
                                ev.Player.gameObject.AddComponent<CuffProccess>();
                                ev.Player.gameObject.GetComponent<CuffProccess>().target = target;
                                ev.Player.gameObject.GetComponent<CuffProccess>().targetstartpos = target.GetPosition();
                                target.ClearBroadcasts();
                                target.Broadcast(5, "<color=#228b22>" + Global._trycufftarget + "</color>", true);
                                ev.ReturnMessage = Global._startcuffproccess + target.nicknameSync.Network_myNickSync;
                                return;
                            }
                        }
                    }
                    else
                    {
                        ev.ReturnMessage = Global._notlook;
                        return;
                    }
                }
            }
            else if (command == "uncuff")
            {
                if (Global.plugin_enable)
                {
                    ReferenceHub target = null;
                    if (Physics.Raycast((ev.Player.gameObject.GetComponent<Scp049PlayerScript>().plyCam.transform.forward * 1.01f) + ev.Player.gameObject.transform.position, ev.Player.gameObject.GetComponent<Scp049PlayerScript>().plyCam.transform.forward, out RaycastHit hit, Global.distance_to_handcuff))
                    {
                        if (hit.transform.GetComponent<QueryProcessor>() == null)
                        {
                            ev.ReturnMessage = Global._notlookremove;
                            return;
                        }
                        else
                        {
                            target = Player.GetPlayer(hit.transform.gameObject);

                            if (target.gameObject.GetComponent<CheckHandcuff>() == null)
                            {
                                ev.ReturnMessage = Global._nothandcuffed;
                                return;
                            }
                            else
                            {
                                if (ev.Player.GetCurrentItem().id == ItemType.Disarmer)
                                {
                                    if (ev.Player.gameObject.GetComponent<CooldownToUncuff>() != null)
                                    {
                                        Object.Destroy(ev.Player.gameObject.GetComponent<CooldownToUncuff>());
                                    }
                                    Object.Destroy(ev.Player.gameObject.GetComponent<CheckHandcuff>());
                                    ev.ReturnMessage = Global._successremove + target.nicknameSync.Network_myNickSync;
                                    return;
                                }
                                else
                                {
                                    if (ev.Player.gameObject.GetComponent<CooldownToUncuff>() != null)
                                    {
                                        ev.ReturnMessage = Global._iscooldown + ev.Player.gameObject.GetComponent<CooldownToUncuff>().cooldown.ToString();
                                        return;
                                    }
                                    ev.Player.gameObject.AddComponent<CooldownToUncuff>();
                                    if (Global.rand.Next(0, 10) > 3)
                                    {
                                        ev.ReturnMessage = Global._badtryuncuff;
                                        return;
                                    }
                                    Object.Destroy(ev.Player.gameObject.GetComponent<CheckHandcuff>());
                                    ev.ReturnMessage = Global._successremove + target.nicknameSync.Network_myNickSync;
                                    return;
                                }
                            }

                        }
                    }
                    else
                    {
                        ev.ReturnMessage = Global._notlookremove;
                        return;
                    }
                }
            }
            else if (command == "ate")
            {
                if (ev.Player.gameObject.GetComponent<CheckHandcuff>() == null)
                {
                    ev.ReturnMessage = Global._nothandcuffed;
                    return;
                }
                if (ev.Player.gameObject.GetComponent<CheckHandcuff>().adminUsage)
                {
                    ev.ReturnMessage = Global._adminUsage;
                    return;
                }

                if (ev.Player.gameObject.GetComponent<CooldownToATE>() != null)
                {
                    ev.ReturnMessage = Global._iscooldown + ev.Player.gameObject.GetComponent<CooldownToATE>().cooldown.ToString();
                    return;
                }
                if (ev.Player.gameObject.GetComponent<TryToATE>() != null)
                {
                    ev.ReturnMessage = Global._already_try_ate;
                    return;
                }

                ev.Player.gameObject.AddComponent<TryToATE>();
                ev.ReturnMessage = Global._startate;
                return;
            }
        }

        public void OnWaitingForPlayers()
        {
            Global.can_use_commands = false;
            Global.distance = 298f;
            Global.plugin_enable = true;
            Global.saveTime = 5.0f;
        }

        public void OnSpawnPlayer(PlayerSpawnEvent ev)
        {
            if (ev.Player.gameObject.GetComponent<CheckHandcuff>() != null)
            {
                Object.Destroy(ev.Player.gameObject.GetComponent<CheckHandcuff>());
            }
            if (ev.Player.gameObject.GetComponent<CheckDistance>() != null)
            {
                Object.Destroy(ev.Player.gameObject.GetComponent<CheckDistance>());
                ev.Player.ClearBroadcasts();
                ev.Player.Broadcast(10u, "<color=#42aaff>*вы слышите, что браслет перестал пикать. Видимо, он больше не активен*</color>", true);
            }
            foreach (GameObject gameobj in PlayerManager.players)
            {
                if (gameobj.GetComponent<CheckDistance>() != null)
                {
                    if (gameobj.GetComponent<CheckDistance>().owner == ev.Player.gameObject)
                    {
                        Object.Destroy(gameobj.GetComponent<CheckDistance>());
                        Player.GetPlayer(gameobj).ClearBroadcasts();
                        Player.GetPlayer(gameobj).Broadcast(10u, "<color=#42aaff>*вы слышите, что браслет перестал пикать. Видимо, он больше не активен*</color>", true);
                    }
                }
            }
        }

        public void OnPocketDimensionExit(PocketDimEscapedEvent ev)
        {
            GameObject gameObject = ev.Player.gameObject;
            foreach (GameObject gameobj in PlayerManager.players)
            {
                if (gameobj.GetComponent<CheckHandcuff>() != null)
                {
                    if (gameobj.GetComponent<CheckHandcuff>().owner == gameObject)
                    {
                        Object.Destroy(gameobj.GetComponent<CheckHandcuff>());
                    }
                }
            }
            if (gameObject.GetComponent<CheckHandcuff>() != null)
            {
                Object.Destroy(gameObject.GetComponent<CheckHandcuff>());
            }

            if (ev.Player.gameObject.GetComponent<CheckDistance>() != null)
            {
                Object.Destroy(ev.Player.gameObject.GetComponent<CheckDistance>());
                ev.Player.ClearBroadcasts();
                ev.Player.Broadcast(10u, "<color=#42aaff>*вы слышите, что браслет перестал пикать. Видимо, он больше не активен*</color>", true);
            }
            foreach (GameObject gameobj in PlayerManager.players)
            {
                if (gameobj.GetComponent<CheckDistance>() != null)
                {
                    if (gameobj.GetComponent<CheckDistance>().owner == ev.Player.gameObject)
                    {
                        Object.Destroy(gameobj.GetComponent<CheckDistance>());
                        Player.GetPlayer(gameobj).ClearBroadcasts();
                        Player.GetPlayer(gameobj).Broadcast(10u, "<color=#42aaff>*вы слышите, что браслет перестал пикать. Видимо, он больше не активен*</color>", true);
                    }
                }
            }
        }

        public void OnHandcuffed(ref HandcuffEvent ev)
        {
            if (!ev.Target.IsHandCuffed())
            {
                if (ev.Target.gameObject.GetComponent<CheckDistance>() == null)
                {
                    ev.Target.ClearBroadcasts();
                    ev.Target.Broadcast(10u, "<color=#ff0000>*вы чувствуете, как вам на руку прикрепили странный браслет*</color>", true);
                    ev.Target.gameObject.AddComponent<CheckDistance>();
                    ev.Target.gameObject.GetComponent<CheckDistance>().owner = ev.Player.gameObject;
                }
            }
            else
            {
                if (ev.Target.gameObject.GetComponent<CheckDistance>() != null)
                {
                    Object.Destroy(ev.Target.gameObject.GetComponent<CheckDistance>());
                    ev.Target.ClearBroadcasts();
                    ev.Target.Broadcast(10u, "<color=#42aaff>*вы чувствуете, как с вас сняли браслет*</color>", true);
                }
            }
        }

        public void OnRoundStart()
        {
            Global.can_use_commands = true;
        }
    }
}