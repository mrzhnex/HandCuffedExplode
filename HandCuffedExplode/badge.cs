using UnityEngine;

namespace HandCuffedExplode
{
    class badge : MonoBehaviour
    {
        private string pbadge;
        private string color;

        public void Start()
        {
            pbadge = gameObject.GetComponent<ServerRoles>().NetworkMyText;
            color = gameObject.GetComponent<ServerRoles>().NetworkMyColor;
            gameObject.GetComponent<ServerRoles>().NetworkMyText = "*издаёт странное пиканье*";
            gameObject.GetComponent<ServerRoles>().NetworkMyColor = "army_green";
        }

        public void OnDestroy()
        {
            gameObject.GetComponent<ServerRoles>().NetworkMyText = pbadge;
            gameObject.GetComponent<ServerRoles>().NetworkMyColor = color;
        }
    }
}
