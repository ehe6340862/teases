using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallCrab
{
    /// <summary>
    /// Entry<K,V>
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class Entry<K,V>
    {
        public K key { get; set; }
        public V value { get; set; }
        public Entry(K k, V v) {
            key = k;
            value = v;
        }
        public Entry()
        {
          
        }
    }
    /// <summary>
    /// Entry<K,F,V>
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class Entry<K,F, V>
    {
        public K key { get; set; }
        public F field { get; set; }
        public V value { get; set; }
        public Entry(K k,F f, V v)
        {
            key = k;
            field = f;
            value = v;
        }
        public Entry()
        {

        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V1"></typeparam>
    /// <typeparam name="V2"></typeparam>
    /// <typeparam name="V3"></typeparam>
    public class Entry<K, V1, V2,V3>
    {
        public K key { get; set; }
        public V1 value1 { get; set; }
        public V2 value2 { get; set; }
        public V3 value3 { get; set; }
        public Entry(K k, V1 v1,V2 v2 ,V3 v3)
        {
            key = k;
            value1 = v1;
            value2 = v2;
            value3 = v3;
        }
        public Entry()
        {

        }
    }

}
