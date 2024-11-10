using System.Collections.Generic;
using UnityEngine;

namespace GameFramework
{
    public class ItemListPopup<T> : MonoBehaviour where T : Item
    {
        public T createdPrefab;
        
        protected List<T> _list;
    }
}