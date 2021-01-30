using System;
using System.Collections.Generic;
using UnityEngine;

namespace PUnity
{
    public class ToolBox<T>
    {
        private Dictionary<T, object> Tools { get; } = new Dictionary<T, object>();

        public K GetTool<K>(T key)
        {
            try
            {
                if (Tools.ContainsKey(key))
                    return (K)Tools[key];
                else
                    return default;
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e.Message + e.StackTrace);
                return default;
            }
        }

        public void SetTool(T key, object tool)
        {
            Tools[key] = tool;
        }

        public bool ContainsTool(T key)
        {
            return Tools.ContainsKey(key) && Tools[key] != null;
        }

        public void ClearTools()
        {
            Tools.Clear();
        }
    }
}