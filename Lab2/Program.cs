using System;
using System.Collections.Generic;

namespace Lab2
{
    public class BinaryHeap<T> where T : IComparable
    {
        private List<T> heap;

        public BinaryHeap()
        {
            heap = new List<T>();
        }

        public void Add(T value)
        {
            // Добавляем новый узел в конец списка
            heap.Add(value);

            int currentIndex = heap.Count - 1;
            int parentIndex = (currentIndex - 1) / 2;

            // Перемещаем узел вверх по куче, пока он не станет корректным
            while (currentIndex > 0 && heap[currentIndex].CompareTo(heap[parentIndex]) > 0)
            {
                // Меняем местами текущий узел и его родителя
                T temp = heap[currentIndex];
                heap[currentIndex] = heap[parentIndex];
                heap[parentIndex] = temp;

                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }
        }

        public override string ToString()
        {
            string str = "";
            heap.ForEach(o => str += $"{o} ");
            return str;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryHeap<int> heap = new BinaryHeap<int>();

            heap.Add(6);
            heap.Add(9);
            heap.Add(15);
            heap.Add(26);
            heap.Add(21);
            heap.Add(8);
            heap.Add(10);

            Console.WriteLine(heap);
            Console.ReadKey();
        }
    }
}
