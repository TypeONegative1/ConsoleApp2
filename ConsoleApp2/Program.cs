using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class Program
    {
        public class HashTable
        {
            private KeyValuePair[] array;

            public HashTable(int size)
            {
                array = new KeyValuePair[size];
            }

            public void PutPair(object key, object value)
            {
                var hashCode = key.GetHashCode();
                var Number = GetNumber(key);
                var stack = array[Number];
                while (stack != null)
                {
                    if (Equals(stack.Key, key))
                    {
                        stack.Value = value;
                        return;
                    }
                    stack = stack.Next;
                }
                var writeStack = array[Number];
                if (writeStack == null)
                {
                    array[Number] = new KeyValuePair { Value = value, Key = key };
                }
                else
                {
                    while (writeStack.Next != null)
                    {
                        writeStack = writeStack.Next;
                    }
                    writeStack.Next = new KeyValuePair { Value = value, Key = key };
                }
            }
            private int GetNumber(object key)
            {
                var hashCode = key.GetHashCode();
                var Number = Math.Abs(hashCode) % array.Length;
                return Number;
            }
            public object GetValueByKey(object key)
            {
                var Number = GetNumber(key);
                var stack = array[Number];
                while (stack != null)
                {
                    if (Equals(stack.Key, key))
                    {
                        return stack.Value;
                    }

                    stack = stack.Next;
                }
                return null;
            }
            class KeyValuePair
            {
                public Object Key { get; set; }
                public Object Value { get; set; }
                public KeyValuePair Next { get; set; }
            }
        }
        static void Main(string[] args)
        {
            ThreeElemTest();
            RepeatKeyTest();
            AddAndSearchElemTest();
            SearchAndAddElemAndKeysTest();
        }
        private static void ThreeElemTest() //Добавление трёх элементов, поиск трёх элементов
        {
            HashTable hashTable = new HashTable(3);
            int j = 0;
            for (int i = 0; i < 3; i++)
                hashTable.PutPair("key" + i, "value" + i);
            for (int i = 0; i < 3; i++)
                if (hashTable.GetValueByKey("key" + i).Equals("value" + i))
                {
                    j++;
                }
            if (j == 3)
                Console.WriteLine("Программа на добавление и поиск трёх элементов работает норм.");
            else
                Console.WriteLine("Программа на добавление и поиск трёх элементов не работает.");
        }


        private static void RepeatKeyTest() //Добавление одного и того же ключа дважды с разными значениями сохраняет последнее добавленное значение
        {
            HashTable hashTable = new HashTable(2);
            hashTable.PutPair("key14", "value88");
            hashTable.PutPair("key14", "value89");
            if (hashTable.GetValueByKey("key14").Equals("value89"))
                Console.WriteLine("Программа на добавление одного и того же ключа дважды работает норм.");
            else
                Console.WriteLine("Программа на добавление одного и того же ключа не работает");
        }

        private static void AddAndSearchElemTest()//Добавление 10000 элементов в структуру и поиск одного из них
        {
            HashTable hashTable = new HashTable(10000);
            for (int i = 0; i < 10000; i++)
            {
                hashTable.PutPair("key" + (i + 3), "value" + i);
            }
            if (hashTable.GetValueByKey("key1488").Equals("value1485"))
                Console.WriteLine("Программа на добавление 10000 элементов в структуру и поиск одного из них работает норм.");
            else
                Console.WriteLine("3Программа на добавление 10000 элементов в структуру и поиск одного из них не работает");
        }

        private static void SearchAndAddElemAndKeysTest()//Добавление 10000 элементов в структуру и поиск 1000 недобавленных ключей, поиск которых должен вернуть null
        {
            HashTable hashTable = new HashTable(10000);
            int j = 0;
            for (int i = 0; i < 10000; i++)
                hashTable.PutPair(i, i);
            for (int i = 10000; i < 11000; i++)
                if (hashTable.GetValueByKey(i) == null)
                {
                    j++;
                }
            if (j == 1000)
                Console.WriteLine("программа на добавление 10000 элементов в структуру и поиск 1000 недобав. ключей работает норм.");
            else
                Console.WriteLine("программа на добавление 10000 элементов в структуру и поиск 1000 недобав. ключей не работает");
        }
    }
}