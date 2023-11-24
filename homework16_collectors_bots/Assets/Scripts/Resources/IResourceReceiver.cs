using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Resources
{
    public interface IResourceReceiver
    {
        public void ReceiveResource(Resource resource);
    }
}
