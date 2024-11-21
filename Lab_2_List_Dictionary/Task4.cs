using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Character
{
    public int hp;
    public int xp;

    public List<Action> skills;

    public Character(int hp, int xp)
    {
        this.hp = hp;
        this.xp = xp;
        skills = new List<Action>();
    }

    override public string ToString()
    {
        return $"HP: {hp}, XP: {xp}";
    }
}

public class MyDictionary<T1,T2> : IEnumerable<KeyValuePair<T1, T2>>
{
    private const int InitialBucketCount = 4;
    private List<KeyValuePair<T1, T2>>[] buckets;
    private int count;
    private int bucketCount;

    public MyDictionary()
    {
        buckets = new List<KeyValuePair<T1, T2>>[InitialBucketCount];
        bucketCount = InitialBucketCount;
        count = 0;
    }
    private void Resize()
    {
        List<KeyValuePair<T1, T2>>[] newBuckets = new List<KeyValuePair<T1, T2>>[bucketCount * 2];
        int newBucketCount = bucketCount * 2;

        // Rehash existing entries into new buckets
        for (int i = 0; i < bucketCount; i++)
        {
            if (buckets[i] != null)
            {
                foreach (var pair in buckets[i])
                {
                    int newBucket = Math.Abs(pair.Key.GetHashCode()) % newBucketCount;

                    if (newBuckets[newBucket] == null)
                    {
                        newBuckets[newBucket] = new List<KeyValuePair<T1, T2>>();
                    }

                    newBuckets[newBucket].Add(pair);
                }
            }
        }

        buckets = newBuckets;
        bucketCount = newBucketCount; // Update the bucketCount to the new size
    }
    public void Add(T1 key, T2 value)
    {
        if (count == bucketCount)
        {
            Resize();
        }

        int bucket = Math.Abs(key.GetHashCode()) % bucketCount;

        if (buckets[bucket] == null)
        {
            buckets[bucket] = new List<KeyValuePair<T1, T2>>();
        }

        buckets[bucket].Add(new KeyValuePair<T1, T2>(key, value));
        count++;
    }

    public T2 this[T1 idx]
    {
        get
        {
            int bucket = Math.Abs(idx.GetHashCode()) % bucketCount;

            if (buckets[bucket] == null)
            {
                throw new KeyNotFoundException();
            }

            foreach (var pair in buckets[bucket])
            {
                if (pair.Key.Equals(idx))
                {
                    return pair.Value;
                }
            }

            throw new KeyNotFoundException();
        }

        set
        {
            int bucket = Math.Abs(idx.GetHashCode()) % bucketCount;

            if (buckets[bucket] == null)
            {
                throw new KeyNotFoundException();
            }

            for (int i = 0; i < buckets[bucket].Count; i++)
            {
                if (buckets[bucket][i].Key.Equals(idx))
                {
                    buckets[bucket][i] = new KeyValuePair<T1, T2>(idx, value);
                    return;
                }
            }

            throw new KeyNotFoundException();
        }
    }

    // IEnumerable implementation
    public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
    {
        foreach (var bucket in buckets)
        {
            if (bucket != null)
            {
                foreach (var pair in bucket)
                {
                    yield return pair;
                }
            }
        }
    }

    // Explicit implementation of the non-generic IEnumerable interface
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}