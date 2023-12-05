namespace AdventOfCodePreset;

public class MinHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public int Count => heap.Count;

    public MinHeap()
    {
        heap = new List<T>();
    }

    public void Insert(T item)
    {
        heap.Add(item);
        int i = heap.Count - 1;
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (heap[parent].CompareTo(heap[i]) <= 0)
                break;
            Swap(parent, i);
            i = parent;
        }
    }

    public T ExtractMin()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Heap is empty.");
        T min = heap[0];
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        Heapify(0);
        return min;
    }

    private void Heapify(int i)
    {
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        int smallest = i;
        if (left < heap.Count && heap[left].CompareTo(heap[smallest]) < 0)
            smallest = left;
        if (right < heap.Count && heap[right].CompareTo(heap[smallest]) < 0)
            smallest = right;
        if (smallest != i)
        {
            Swap(i, smallest);
            Heapify(smallest);
        }
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}