namespace Triggers2D
{
    public interface ITrigger2DEvents 
    {
        public void OnTrigger2DEnter(Trigger2D other);
        public void OnTrigger2DExit(Trigger2D other);
    }
}