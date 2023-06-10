using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Systems.ObjectPlacement
{
    internal class ObjectPlacer
    {
        private Vector3 _prevOffsetDirection = Vector3.zero;

        public bool TryPlace(GameObject objectWithCollider, Vector2 desiredPoint, float maxOffset = 4f)
        {
            var inst = GameObject.Instantiate(
                objectWithCollider,
                desiredPoint,
                Quaternion.identity);

            Collider2D collider;

            if (inst.TryGetComponent(out collider) == false)
                return false;

            _prevOffsetDirection = Vector2.zero;

            var bounds = collider.bounds;
            inst.SetActive(false);

            var extents = bounds.extents;
            bounds.min -= extents;
            bounds.max -= extents;

            var initLeftCorner = bounds.min;

            for (int i = 0; i < 8; i++)
            {
                var hits = Physics2D.BoxCastAll(bounds.min + bounds.extents, bounds.size, 0f, Vector2.zero);
                if (hits.Length == 0)
                {
                    inst.transform.position = bounds.min;
                    inst.SetActive(true);
                    return true;
                }

                var offset = CalcOffsetVector(hits, bounds);

                bounds.max += offset;
                bounds.min += offset;

                if (Vector2.Distance(bounds.min, initLeftCorner) > maxOffset)
                    break;
            }

            GameObject.Destroy(inst);
            return false;
        }

        private Vector3 CalcOffsetVector(RaycastHit2D[] hits, Bounds bounds)
        {
            var offsets = new List<Vector3>(4)
            {
                // Up
                new Vector3(0f, CalcOffset(hits, bounds, (h, b) => h.collider.bounds.max.y - b.min.y), 0f),
                // Down
                new Vector3(0f, CalcOffset(hits, bounds, (h, b) => h.collider.bounds.min.y - b.max.y) * -1, 0f),
                // Left
                new Vector3(CalcOffset(hits, bounds, (h, b) => h.collider.bounds.min.x - b.max.x) * -1, 0f, 0f),
                // Right
                new Vector3(CalcOffset(hits, bounds, (h, b) => h.collider.bounds.max.x - b.min.x), 0f, 0f)
            };

            offsets.Sort((a, b) => (int)((a.magnitude - b.magnitude) * 1000));
            var offset = offsets[0];

            if (offsets[0].y > 0 && _prevOffsetDirection.y < 0
                || offsets[0].y < 0 && _prevOffsetDirection.y > 0
                || offsets[0].x > 0 && _prevOffsetDirection.x < 0
                || offsets[0].x < 0 && _prevOffsetDirection.x > 0) 
                offset = offsets[1];

            _prevOffsetDirection = offset;
            return offset;
        }

        private float CalcOffset(RaycastHit2D[] hits,
                               Bounds bounds,
                               Func<RaycastHit2D, Bounds, float> func)
        {
            var maxOffset = float.MinValue;

            foreach (var hit in hits)
            {
                var offset = Mathf.Abs(func(hit, bounds));

                if (offset > maxOffset)
                    maxOffset = offset;
            }
            return maxOffset + UnityEngine.Random.Range(0.005f, 0.05f);
        }
    }
}
