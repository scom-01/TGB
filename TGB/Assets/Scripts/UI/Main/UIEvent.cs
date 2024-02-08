using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB
{
    public class UIEvent : MonoBehaviour
    {
        protected UIManager manager;
        protected virtual void Awake()
        {
            manager = this.GetComponentInParent<UIManager>();
        }
    }
}
