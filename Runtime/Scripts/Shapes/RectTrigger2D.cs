using UnityEngine;
using static UTrigger2D.Trgger2DAlgorithms;

namespace UTrigger2D
{
    public class RectTrigger2D : Trigger2D
    {
        public Vector2 size;

        protected override TriggerType TriggerType => TriggerType.Rect;

        private void Awake() => tr = transform;

        public Rect property => new Rect(X - size.x / 2, Y - size.y / 2, size.x, size.y);

        protected override bool CheckRectTrigger(RectTrigger2D other) =>
            OnTrigger(property, other.property);

        protected override bool CheckCircleTrigger(CircleTrigger2D other) =>
            OnTrigger(property, other.property);

        protected override void Reset()
        {
            base.Reset();

            if (GetComponent<SpriteRenderer>())
                size = GetComponent<SpriteRenderer>().bounds.size;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!TriggerIsSelected())
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(new Vector3(X,Y,transform.position.z), size);
        }
#endif
    }
}
