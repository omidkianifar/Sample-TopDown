using UnityEngine;

namespace _Project.Scripts
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector3 offset = new Vector3(0, 5, 0);
        public Vector3 rotation = new Vector3(40f, 0f, 0f);

        private void Start()
        {
            transform.rotation = Quaternion.Euler(rotation);
        }

        private void LateUpdate()
        {
            if (target != null)
            {
                transform.position = target.position + offset;
            }
        }
    }
}