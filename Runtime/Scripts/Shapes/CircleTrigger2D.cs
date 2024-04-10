using UnityEngine;
using static UTrigger2D.Trgger2DAlgorithms;

namespace UTrigger2D
{
    public class CircleTrigger2D : Trigger2D
    {
        public float radius = 10;

        protected override TriggerType TriggerType => TriggerType.Circle;

        public Circle property => new Circle(X, Y, radius);

        private void Awake() => tr = transform;

        protected override bool CheckRectTrigger(RectTrigger2D other) => 
            OnTrigger(circle:property,rect: other.property);

        protected override bool CheckCircleTrigger(CircleTrigger2D other) => 
            OnTrigger(property, other.property);

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
            Gizmos.DrawWireSphere(new Vector3(X,Y,transform.position.z), radius);
        }
#endif
    }
}
