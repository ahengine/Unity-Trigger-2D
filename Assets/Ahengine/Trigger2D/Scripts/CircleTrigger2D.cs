using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Triggers2D
{
    public class CircleTrigger2D : Trigger2D
    {
        public float radius = 10;
        public Vector2 offset;
        private Transform tr;

        protected override TriggerType TriggerType => TriggerType.Circle;

        private void Awake() => tr = transform;

        public override float X => tr.position.x + offset.x;
        public override float Y => tr.position.y + offset.y;


        protected override bool CheckRectTrigger(RectTrigger2D trigger) 
        {
            var distX = Mathf.Abs(X - trigger.X - trigger.Width / 2);
            var distY = Mathf.Abs(Y - trigger.Y - trigger.Height / 2);

            if (distX > (trigger.Width / 2 + radius)) return false;
            if (distY > (trigger.Height / 2 + radius)) return false;

            if (distX <= (trigger.Width / 2)) return true;
            if (distY <= (trigger.Height / 2)) return true;

            var dx = distX - trigger.Width / 2;
            var dy = distY - trigger.Height / 2;
            return dx * dx + dy * dy <= (radius * radius);
        }

        protected override bool CheckCircleTrigger(CircleTrigger2D trigger)
        {
            var dx = X - trigger.X;
            var dy = Y - trigger.Y;
            var distance = Mathf.Sqrt(dx * dx + dy * dy);

            return distance < radius + trigger.radius;
        }

        protected override void Reset()
        {
            base.Reset();

            if (GetComponent<SpriteRenderer>())
                radius = 0.5f * Mathf.Min(GetComponent<SpriteRenderer>().bounds.size.x, GetComponent<SpriteRenderer>().bounds.size.y);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!TriggerIsSelected())
                return;

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + (Vector3)offset, radius);
        }
#endif
    }
}
