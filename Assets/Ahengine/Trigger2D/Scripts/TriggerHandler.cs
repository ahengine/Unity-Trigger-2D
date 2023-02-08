using UnityEngine;
using System.Collections.Generic;

namespace Triggers2D
{
    public enum TriggerType { Rect, Circle, Line }

    public class TriggerHandler : MonoBehaviour
    {
        private static TriggerHandler instance;
        public static TriggerHandler Instance => instance ?? (instance = new TriggerHandler());

        private List<Trigger2D> triggers = new List<Trigger2D>();

        public static void Add(Trigger2D trigger) =>
            Instance.triggers.Add(trigger);
        public static void Remove(Trigger2D trigger) =>
            Instance.triggers.Remove(trigger);

        private void FixedUpdate()
        {
            for (int i = 0; i < triggers.Count; i++)
                for (int j = 0; j < triggers.Count; j++)
                {
                    if (i == j)
                        continue;

                    if (triggers[i].CheckTrigger(triggers[j]))
                        triggers[i].TriggerEnter(triggers[j]);
                    else
                        triggers[i].TriggerExit(triggers[j]);
                }

        }
    }
}