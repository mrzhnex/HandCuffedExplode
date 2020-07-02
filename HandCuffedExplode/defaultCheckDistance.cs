using UnityEngine;
using Smod2.API;
using RemoteAdmin;

namespace HandCuffedExplode
{
    class defaultCheckDistance : MonoBehaviour
    {
        private float timer = 0f;
        private Handcuffs Handcuffs;
        Player target;

        public void Start()
        {
            Handcuffs = gameObject.GetComponent<Handcuffs>();
            if (gameObject.GetComponent<Handcuffs>() == null)
            {
                global.plugin.Info("ebala");
                Destroy(gameObject.GetComponent<defaultCheckDistance>());
            }
            foreach (Player p in global.players)
            {
                if (p.PlayerId == Handcuffs.NetworkcuffTarget.GetComponent<QueryProcessor>().PlayerId)
                {
                    global.plugin.Info("a333");
                    target = p;
                    break;
                }
            }
        }

        public void Update()
        {
            timer = timer + Time.deltaTime;
            if (timer > 5f)
            {
                timer = 0f;
                global.plugin.Info("tar: " + Handcuffs.cuffTarget.transform.position.ToString() + " main: " + gameObject.transform.position.ToString());
                if (!target.IsHandcuffed())
                {
                    global.plugin.Info("escape");
                    Destroy(gameObject.GetComponent<defaultCheckDistance>());
                }
                if (Vector3.Distance(Handcuffs.NetworkcuffTarget.gameObject.transform.position, gameObject.transform.position) > global.distance)
                {
                    global.plugin.Info("explode");
                    CustomThrowG(ItemType.FRAG_GRENADE, true, Vector.Zero, true, new Vector(0f, 0f, 0f), true, 0, Handcuffs.NetworkcuffTarget.gameObject, false);
                    Destroy(gameObject.GetComponent<defaultCheckDistance>());
                }

            }
            
        }

        public void CustomThrowG(ItemType grenadeType, bool isCustomDirection, Vector direction, bool isEnvironmentallyTriggered, Vector position, bool isCustomForce, float throwForce, GameObject player, bool slowThrow = false)
        {
            int num = 0;
            if (grenadeType != ItemType.FRAG_GRENADE)
            {
                if (grenadeType == ItemType.FLASHBANG)
                {
                    num = 1;
                }
            }
            else
            {
                num = 0;
            }
            GrenadeManager component = player.GetComponent<GrenadeManager>();
            Vector3 forward = player.GetComponent<Scp049PlayerScript>().plyCam.transform.forward;
            if (isCustomDirection)
            {
                forward = new Vector3(direction.x, direction.y, direction.z);
            }
            if (!isCustomForce)
            {
                throwForce = ((!slowThrow) ? 1f : 0.5f) * component.availableGrenades[num].throwForce;
            }
            Grenade component2 = UnityEngine.Object.Instantiate<GameObject>(component.availableGrenades[num].grenadeInstance).GetComponent<Grenade>();
            component2.id = player.GetComponent<QueryProcessor>().PlayerId + ":" + (component.smThrowInteger + 4096);
            GrenadeManager.grenadesOnScene.Add(component2);
            component2.SyncMovement(component.availableGrenades[num].GetStartPos(player), (player.GetComponent<Scp049PlayerScript>().plyCam.transform.forward +
                Vector3.up / 4f).normalized * throwForce, Quaternion.Euler(component.availableGrenades[num].startRotation), component.availableGrenades[num].angularVelocity);
            GrenadeManager grenadeManager2 = component;
            GrenadeManager grenadeManager = component;

            int id = num;
            int playerId = player.GetComponent<QueryProcessor>().PlayerId;
            int smThrowInteger = grenadeManager2.smThrowInteger;
            grenadeManager2.smThrowInteger = smThrowInteger + 1;

            grenadeManager.availableGrenades[id].timeUnitilDetonation = 0f;
            grenadeManager.CallRpcThrowGrenade(id, playerId, smThrowInteger + 4096, forward, isEnvironmentallyTriggered, new Vector3(position.x, position.y, position.z), slowThrow, 0);

        }
    }
}
