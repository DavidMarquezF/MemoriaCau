using System;
using System.Collections;
using System.Collections.Specialized;

namespace MemoriaCau
{
    public class AccessRow<T>
    {

        private OrderedDictionary _row;

        public AccessRow(CacheEvent<T> ev, UInt32 key, OrderedDictionary extraCols)
        {
            _row = new OrderedDictionary();
            _row.Add("Adreça", string.Format("0x{0:X4}", key));
            _row.Add("Adreça bits", "0b" + Convert.ToString(key, 2));
            _row.Add("Acció", ev.type == AccessType.Read ? "Lec" : "Esc");
            _row.Add("Etiq", string.Format("0x{0:X}",ev.block.Tag));
            foreach (DictionaryEntry d in extraCols)
                _row.Add(d.Key, d.Value.ToString());

            var type = ev.success ? "Encert" : "Fallada";
            var replace = !ev.replacedNode ? null : string.Format(". S'elimina 0x{0:X}", ev.replacedNodeTag);
            _row.Add("Esdeveniment", string.Format("{0}{1}", type, replace));
        }

        public string getHeader()
        {
            string[] keys = new String[_row.Count];
            _row.Keys.CopyTo(keys, 0);
            return string.Join(',', keys);
        }

        public AccessRow<T> AddElement(string value)
        {
            _row.Insert(0, "Element", value);
            return this;
        }

        public override string ToString()
        {
            string[] values = new String[_row.Count];
            _row.Values.CopyTo(values, 0);
            return string.Join(',', values);
        }


    }
}