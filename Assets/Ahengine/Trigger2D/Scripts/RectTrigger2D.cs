using UnityEngine;

namespace Triggers2D
{
    public class RectTrigger2D : Trigger2D
    {
        public Vector2 size;
        public Vector2 offset;

        protected override TriggerType TriggerType => TriggerType.Rect;

        private Transform tr;

        private void Awake() => tr = transform;

        public override float X => tr.position.x - size.x / 2;
        public float Width => size.x;

        public override float Y => tr.position.y - size.y / 2;
        public float Height => size.y;

        protected override bool CheckRectTrigger(RectTrigger2D trigger) =>
            X < trigger.X + trigger.Width &&
            Y < trigger.Y + trigger.Height &&
            X + Width > trigger.X &&
            Height + Y > trigger.Y;

        protected override bool CheckCircleTrigger(CircleTrigger2D trigger)
        {
            var distX = Mathf.Abs(trigger.X - X - Width / 2);
            var distY = Mathf.Abs(trigger.Y - Y - Height / 2);

            if (distX > (Width / 2 + trigger.radius) ||
                distY > (Height / 2 + trigger.radius)) return false;

            if (distX <= (Width / 2) || distY <= (Height / 2)) return true;

            var dx = distX - Width / 2;
            var dy = distY - Height / 2;
            return dx * dx + dy * dy <= (trigger.radius * trigger.radius);
        }

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
            Gizmos.DrawWireCube(transform.position, size);
        }
#endif
    }
}
