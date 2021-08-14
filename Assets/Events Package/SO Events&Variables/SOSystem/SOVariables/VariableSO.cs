using SO.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    //  [CreateAssetMenu(fileName = "SOEvent", menuName = "SO/Event")]
    [Serializable]
    public abstract class VariableSO<T> : VariableSO, ISerializationCallbackReceiver
    {

        //Value

        public T Value { get { return GetValue(); } set { SetValue(value); } }
        [NonSerialized]
        private T _value;

        //When the game starts, the starting Value we use (so we can reset if need be)
        [SerializeField]
        private T startingValue = default(T);


        public virtual void SetValue(T newValue, bool log = false)
        {
            if (log) Debug.Log("SetValue: " + newValue + " on " + name);
            _value = newValue;
            if (OnChanged != null)
                OnChanged.Value = this;
            RaisEvents();
        }

        public void SetValue(VariableSO<T> numSO)
        {
            SetValue(numSO.Value);
        }

        public virtual T GetValue()
        {
            return _value;
        }

        /// <summary>
        /// Recieve callback after unity deseriallzes the object
        /// </summary>
        public void OnAfterDeserialize()
        {
            _value = startingValue;
        }

        public void OnBeforeSerialize() { }

        /// <summary>
        /// Reset the Value to it's inital Value if it's resettable
        /// </summary>
        public override void ResetValue()
        {
            Value = startingValue;
        }



    }
    public abstract class VariableSO : ScriptableObject, IFormattable
    {
        public EventSO OnChanged;
        protected event System.EventHandler valChanged;

        protected virtual void RaisEvents()
        {
            if (this.valChanged != null) valChanged(this, EventArgs.Empty);
            if (OnChanged != null) OnChanged.Raise();
        }

        public void Subscripe(System.EventHandler onValChanged)
        {
            valChanged += onValChanged;
        }
        public void UnSubscripe(System.EventHandler onValChanged)
        {
            valChanged -= onValChanged;
        }


        public abstract string ToString(string format, IFormatProvider formatProvider);

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format)
        {
            return ToString(format, null);
        }

        public abstract void SetValue(string value);

        public static VariableSO Parse(string inistanceID)
        {
            int id = 0;
            if (int.TryParse(inistanceID, out id))
            {
#if UNITY_EDITOR
                try
                {
                    return (VariableSO)EditorUtility.InstanceIDToObject(id);
                }
                catch (Exception)
#endif
                {
                    throw new Exception("cant find inistanceID: " + inistanceID);
                }
            }
            else
            {
                throw new Exception("string is not inistanceID: " + inistanceID);
            }
        }

        public static bool TryParse(string inistanceID, out VariableSO variableSO)
        {
            try
            {
                variableSO = VariableSO.Parse(inistanceID);
                return true;
            }
            catch (Exception)
            {

                variableSO = null;
                return false;
            }
        }

        public void CopyToText(Text textComponent)
        {
            textComponent.text = this.ToString();
        }

        public void CopyToTMP_Text(TMPro.TMP_Text TMP_textComponent)
        {
            TMP_textComponent.text = this.ToString();
        }

        public void CopyToInputField(InputField InputFieldComponent)
        {
            InputFieldComponent.text = this.ToString();
        }

        public void CopyToTMP_InputField(TMPro.TMP_InputField TMP_InputFieldComponent)
        {
            TMP_InputFieldComponent.text = this.ToString();
        }

        /// <summary>
        /// Reset the Value to it's inital Value if it's resettable
        /// </summary>
        public abstract void ResetValue();

    }

}