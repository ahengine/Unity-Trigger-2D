using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UTrigger2D
{
    public abstract class Trigger2D : MonoBehaviour
    {
        protected Transform tr;

        [SerializeField] private GameObject eventsOwner;
        [SerializeField] private UnityEvent<Trigger2D> OnEnter;
        [SerializeField] private UnityEvent<Trigger2D> OnExit;

        [SerializeField] protected List<Trigger2D> triggerd = new List<Trigger2D>();

        protected virtual TriggerType TriggerType => TriggerType.Rect;

        public Vector2 offset;
        public virtual float X => transform.position.x + offset.x;
        public virtual float Y => transform.position.y + offset.y;

        protected void OnEnable() => Trigger2DHandler.Add(this);

        protected void OnDisable() => Trigger2DHandler.Remove(this);

        protected void OnDestroy() => Trigger2DHandler.Remove(this);

        protected virtual void Reset() => eventsOwner = gameObject;

        public void TriggerEnter(Trigger2D trigger)
        {
            for (int i = 0; i < triggerd.Count; i++)
                if (triggerd[i] == trigger)
                    return;

            triggerd.Add(trigger);

            if (eventsOwner)
            {
                var eventCompOwners = eventsOwner.GetComponents<ITrigger2DEvents>();

                for (int i = 0; i < eventCompOwners.Length; i++)
                    eventCompOwners[i].OnTrigger2DEnter(trigger);
            }
            OnEnter.Invoke(trigger);
        }

        public void TriggerExit(Trigger2D trigger)
        {
            if (triggerd.Count == 0)
                return;

            for (int i = 0; i < triggerd.Count; i++)
                if (triggerd[i] == trigger)
                {
                    triggerd.Remove(trigger);

                    if (eventsOwner)
                    {
                        var eventCompOwners = eventsOwner.GetComponents<ITrigger2DEvents>();

                        for (int ownersIndex = 0; ownersIndex < eventCompOwners.Length; ownersIndex++)
                            eventCompOwners[ownersIndex].OnTrigger2DExit(trigger);
                    }

                    OnExit.Invoke(trigger);
                    return;
                }
        }

        public bool CheckTrigger(Trigger2D trigger)
        {
            switch (trigger.TriggerType)
            {
                case TriggerType.Rect:
                    return CheckRectTrigger(trigger.GetComponent<RectTrigger2D>());

                case TriggerType.Circle:
                    return CheckCircleTrigger(trigger.GetComponent<CircleTrigger2D>());
            }

            return false;
        }

        protected virtual bool CheckRectTrigger(RectTrigger2D trigger) => false;
        protected virtual bool CheckCircleTrigger(CircleTrigger2D trigger) => false;

#if UNITY_EDITOR
        protected bool TriggerIsSelected()
        {
            for (int i = 0; i < Selection.objects.Length; i++)
                if (Selection.objects[i] == gameObject)
                    return true;
            return false;
        }
#endif

    }
}