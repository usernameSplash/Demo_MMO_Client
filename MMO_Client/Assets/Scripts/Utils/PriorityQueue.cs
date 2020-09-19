using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable<T>
{
    List<T> _heap = new List<T>();

    public void Push(T data)
    {
        //힙의 맨 끝에 새로운 데이터를 삽입한다.
        _heap.Add(data);

        int now = _heap.Count - 1;

        while (now > 0)
        {
            int next = (now - 1) / 2;
            if (_heap[now].CompareTo(_heap[next]) < 0)
                break;

            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;

            now = next;
        }

    }
    public T Pop()
    {
        // 반환할 데이터를 따로 저장
        T ret = _heap[0];

        // 마지막 데이터를 루트로 이동
        int lastIndex = _heap.Count - 1;
        _heap[0] = _heap[lastIndex];
        _heap.RemoveAt(lastIndex);
        lastIndex--;

        int now = 0;
        while (true)
        {
            int left = now * 2 + 1;
            int right = now * 2 + 2;

            int next = now;

            if (left <= lastIndex && _heap[next].CompareTo(_heap[left]) > 0)
                next = left;
            if (right <= lastIndex && _heap[next].CompareTo(_heap[right]) > 0)
                next = right;

            if (next == now)
                break;

            T temp = _heap[now];
            _heap[now] = _heap[next];
            _heap[next] = temp;

            now = next;

        }

        return ret;

    }

    public int Count { get { return _heap.Count; } }
}