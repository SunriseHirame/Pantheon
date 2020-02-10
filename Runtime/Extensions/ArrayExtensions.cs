using UnityEngine;
using UnityEngine.Assertions;

namespace Hirame.Pantheon
{
    public static class ArrayExtensions
    {
        public static T GetRandom<T> (this T[] array)
        {
            Assert.IsTrue (array != null);
            Assert.IsTrue (array.Length > 0);
            
            return array[Random.Range (0, array.Length - 1)];
        }
    }

}