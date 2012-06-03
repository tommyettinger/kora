﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UAM.Kora;
using System.Diagnostics;

namespace Kora.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("RBTree:");
            MeasureFor(100000, 131072, new SortedDictionary<uint, uint>());
            Console.WriteLine("RBTree:");
            MeasureFor(100000, 131072, new VEBTree<uint>(17));
            Console.WriteLine("DPH:");
            MeasureFor(100000, 131072, new HashTable<uint>());
            Console.WriteLine("XFast:");
            MeasureFor(100000, 131072, new XFastTrie<uint>());
            Console.WriteLine("YFast:");
            MeasureFor(100000, 131072, new YFastTrie<uint>());
            Console.ReadLine();
        }

        static void MeasureFor(int size, int range, IDictionary<uint, uint> emptyDict)
        {
            uint[] additionSet = GenerateRandomSet(size, range);
            Shuffle(additionSet);
            uint[] removalSet = (uint[])additionSet.Clone();
            Shuffle(removalSet);
            var timer = new Stopwatch();
            //var vebTree = new VEBTree<uint>(16);
            timer.Restart();
            for (int i = 0; i < size; i++)
            {
                emptyDict.Add(additionSet[i], additionSet[i]);
            }
            timer.Stop();
            Console.WriteLine("INSERT: {0}", timer.ElapsedMilliseconds);
            timer.Restart();
            for (int i = 0; i < size; i++)
            {
                bool result = emptyDict.Remove(removalSet[i]);
            }
            timer.Stop();
            Console.WriteLine("DELETE: {0}", timer.ElapsedMilliseconds);
        }

        static private uint[] GenerateRandomSet(int size, int range)
        {
            var rand = new Random();
            var set = new HashSet<uint>();
            while (set.Count < size)
            {
                int next = rand.Next(range);
                set.Add((uint)next);
            }
            return set.ToArray();
        }

        public static void Shuffle<T>(T[] array)
        {
            var random = new Random();
            for (int i = array.Length; i > 1; i--)
            {
                // Pick random element to swap.
             int j = random.Next(i); // 0 <= j <= i-1
                // Swap.
                T tmp = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tmp;
            }
        }
    }
}