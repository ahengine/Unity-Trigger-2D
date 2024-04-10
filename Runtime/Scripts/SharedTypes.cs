namespace UTrigger2D
{
    public enum TriggerType { Rect, Circle, Line }

    [System.Serializable]
    public struct Circle
    {
        public float x;
        public float y;
        public float radius;

        public Circle(float x,float y, float radius)
        {
            this.x = x;
            this.y = y;
            this.radius = radius;
        }
    }

    public interface ITrigger2DEvents 
    {
        public void OnTrigger2DEnter(Trigger2D other);
        public void OnTrigger2DExit(Trigger2D other);
    }
}