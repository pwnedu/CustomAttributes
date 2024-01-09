// NOTE: DON'T put in an editor folder. //

// Originally created by GucioDevs
// Modified by Kiltec

using UnityEngine;

namespace CustomAttributes
{
    /// <summary>
    /// <code>[MinMaxRange(0, 10)]</code>
    /// <list type="Examples">
    ///    <listheader>
    /// <para>Example Usage:</para> 
    ///    </listheader>
    ///     <item>
    /// <para>Limit a Vector to a specified range: </para>
    /// <code>[MinMaxRange(10f, 20f)] public Vector2 myVector2;</code>
    ///     </item>
    ///     <item>
    /// <para>Limit a Vector to a specified range using z for current value: </para>
    /// <code>[MinMaxRange(5f, 15f)] public Vector3 myVector3;</code>
    ///     </item>
    /// </list>
    /// </summary>
    public class MinMaxRangeAttribute : PropertyAttribute
    {
        public float min;
        public float max;

        public MinMaxRangeAttribute(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}