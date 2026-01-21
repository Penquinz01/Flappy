using UnityEngine;
using System;

namespace States
{
    public abstract class States
    {
        public abstract void Initilize();
        public abstract void Update();
        public abstract void Deinitialize();
    }
}