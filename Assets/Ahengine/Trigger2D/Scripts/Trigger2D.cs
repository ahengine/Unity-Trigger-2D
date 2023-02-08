using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Triggers2D
{
    public abstract class Trigger2D : MonoBehaviour
    {
        private const string ON_TRIGGER_ENTER_FUNC = "OnTrigger2DEnter";
        private const string ON_TRIGGER_EXIT_FUNC = "OnTrigger2DExit";

        [SerializeField] private GameObject eventsOwner;
        [SerializeField] UnityEvent<Trigger2D> OnEnter;
        [SerializeField] UnityEvent<Trigger2D> OnExit;

        protected List<Trigger2D> triggerd = new List<Trigger2D>();

        protected virtual TriggerType TriggerType => TriggerType.Rect;

        public virtual float X => transform.position.x;
        public virtual float Y => transform.position.y;

        protected void OnEnable() => TriggerHandler.Add(this);

        protected void OnDisable() => TriggerHandler.Remove(this);

        protected void OnDestroy() => TriggerHandler.Remove(this);

        protected virtual void Reset() => eventsOwner = gameObject;

        public void TriggerEnter(Trigger2D trigger)
        {
            for (int i = 0; i < triggerd.Count; i++)
                if (triggerd[i] == trigger)
                    return;

            triggerd.Add(trigger);

            if(eventsOwner) eventsOwner.SendMessage(ON_TRIGGER_ENTER_FUNC, trigger, SendMessageOptions.DontRequireReceiver);
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
                    if (eventsOwner) eventsOwner.SendMessage(ON_TRIGGER_EXIT_FUNC, trigger, SendMessageOptions.DontRequireReceiver);
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