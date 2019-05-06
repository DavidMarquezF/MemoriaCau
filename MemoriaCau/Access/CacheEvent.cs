using System;

namespace MemoriaCau
{
    public struct CacheEvent<T>
    {
        public bool success { get; private set; }
        public AccessType type {get; private set;}
        public bool replacedNode { get; private set; }
        public UInt32 replacedNodeTag { get; private set; }

        public Block<T> block { get; private set; }

        public CacheEvent(bool success, AccessType type)
        {
            this.type = type;
            this.replacedNode = false;
            this.replacedNodeTag = default(UInt32);
            this.block = null;
            this.success = success;
        }

        public CacheEvent(bool success, AccessType type, Block<T> b)
        {
            this.type = type;
            this.success = success;
            this.replacedNodeTag = default(UInt32);
            block = b;
            replacedNode = false;
        }
        public CacheEvent(bool success, AccessType type, Block<T> n, UInt32 replacedNode)
        {
            this.success = success;
            this.type = type;
            this.replacedNodeTag = replacedNode;
            block = n;
            this.replacedNode = true;
        }
    }
}