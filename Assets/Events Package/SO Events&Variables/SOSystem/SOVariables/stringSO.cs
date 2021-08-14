using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SO
{
    [CreateAssetMenu(fileName = "stringSO", menuName = "SO/Variables/String")]
    public class stringSO : VariableSO<string>
    {
        public override void SetValue(string value)
        {
            Value = value;
        }

        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return Value;
        }
    }
}