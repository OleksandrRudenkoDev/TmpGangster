#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
namespace Utils
{
    public class GizmosDrawer : MonoBehaviour
    {
        private static GizmosDrawer _instance;

        public static GizmosDrawer Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject("[GizmosDrawer]");
                    _instance = go.AddComponent<GizmosDrawer>();

                }

                return _instance;
            }
        }

        private readonly List<RayData> _rays = new List<RayData>();

        public void DrawRay(Vector3 origin, Vector3 direction, Color color, float length = 10f)
        {
            _rays.Add(new RayData
            {
                origin = origin,
                direction = direction,
                color = color,
                length = length
            });
        }

        public void DrawRay(Ray ray, Color color, float length = 10f)
        {
            DrawRay(ray.origin, ray.direction, color, length);
        }

        private void OnDrawGizmos()
        {
            foreach (RayData ray in _rays)
            {
                Gizmos.color = ray.color;
                Gizmos.DrawRay(ray.origin, ray.direction.normalized * ray.length);
            }

            _rays.Clear();
        }

        private struct RayData
        {
            public Vector3 origin;
            public Vector3 direction;
            public Color color;
            public float length;
        }
    }
}
#endif