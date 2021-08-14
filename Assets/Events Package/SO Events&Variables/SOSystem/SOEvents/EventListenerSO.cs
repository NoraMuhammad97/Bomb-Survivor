using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace SO.Events
{

    [System.Serializable]
    public class SOListener
    {
        public EventSO Event;
        public bool listenWhenDisabled;
        public UnityEvent Response;
        [TextArea]
        [Tooltip("What does this object do when the attached event is raised")]
        public string WhatTheEventDO;
        public EventListenerSO source;
    }

    [ExecuteInEditMode]
    public class EventListenerSO : MonoBehaviour
    {

        public List<SOListener> listeners = new List<SOListener>();

        //public void OnEventRaised(SOEvent soEvent)
        //{
        //    for (int i = 0; i < listeners.Count; i++)
        //    {
        //        if (listeners[i].Event == soEvent)
        //            listeners[i].Response.Invoke();
        //    }
        //}

        private void OnEnable()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                listeners[i].source = this;
                if (listeners[i].Event != null) listeners[i].Event.RegisterListener(listeners[i]);

            }

        }
        private void OnDestroy()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i].Event != null) listeners[i].Event.UnregisterListener(listeners[i]);
            }
        }
        private void OnDisable()
        {
            for (int i = 0; i < listeners.Count; i++)
            {
                if (listeners[i].listenWhenDisabled == false)
                    if (listeners[i].Event != null) listeners[i].Event.UnregisterListener(listeners[i]);
            }
        }



    }
}
