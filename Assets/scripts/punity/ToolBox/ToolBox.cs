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
                    return default(K);
            }
            catch (InvalidCastException e)
            {
                Debug.Log(e.Message + e.StackTrace);
                return default;
            }
        }

        public void SetTool(T key, object tool)
        {
            if (!Tools.ContainsKey(key))
                Tools[key] = tool;
            else
                Tools.Add(key, tool);
        }

        public bool ContainsTool(T key)
        {
            if (Tools.ContainsKey(key) && Tools[key] != null)
                return true;
            else
                return false;
        }

        public void ClearTools()
        {
            Tools.Clear();
        }
    }
}