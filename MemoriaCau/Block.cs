using System;

namespace MemoriaCau
{
    public class Block<T>
    {
        public readonly UInt32 Tag;
        public T Val { get; set; }

        public DateTime Added { get; private set; }
        public DateTime LastUsed { get; private set; }
        public Block(UInt32 key, T val)
        {
            this.Tag = key;
            this.Val = val;
            DateTime ts = DateTime.Now;
            Added = ts;
            LastUsed = ts;
        }

        public void use()
        {
            LastUsed = DateTime.Now;
        }
    }
}