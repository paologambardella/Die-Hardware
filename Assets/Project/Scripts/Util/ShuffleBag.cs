﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShuffleBag<T> : ICollection<T>, IList<T>
{
	private List<T> data = new List<T> ();
	private int cursor = 0;
	private T last;
	
	public T Next ()
	{
		if (cursor < 1) 
        {
			cursor = data.Count - 1;
			if (data.Count < 1)
            {
				return default(T);
            }

			return data[0];
		}

		int grab = Mathf.FloorToInt (Random.value * (cursor + 1));
		T temp = data[grab];
		data[grab] = this.data[this.cursor];
		data[cursor] = temp;
		cursor--;
		return temp;
	}
	
	
	#region IList[T] implementation
	public int IndexOf (T item)
	{
		return data.IndexOf (item);
	}
	
	public void Insert (int index, T item)
	{
		data.Insert (index, item);
	}
	
	public void RemoveAt (int index)
	{
		data.RemoveAt (index);
	}
	
	public T this[int index] 
    {
		get 
        {
			return data [index];
		}

		set 
        {
			data [index] = value;
		}
	}
	#endregion
	
	
	
	#region IEnumerable[T] implementation
	IEnumerator<T> IEnumerable<T>.GetEnumerator ()
	{
		return data.GetEnumerator ();
	}
	#endregion
	
	#region ICollection[T] implementation
	public void Add (T item)
	{
		data.Add (item);
		cursor = data.Count - 1;
	}

    public void Add (T item, int count) //add one item count number of time
    {
        for (int i = 0; i < count; ++i)
        {
            Add(item);
        }
    }

	public void AddRange(IEnumerable<T> items)
	{
		data.AddRange(items);
		cursor = data.Count - 1;
	}
	
	public int Count 
    {
		get 
        {
			return data.Count;
		}
	}
	
	public void Clear ()
	{
		data.Clear ();
	}
	
	public bool Contains (T item)
	{
		return data.Contains (item);
	}
	
	public void CopyTo (T[] array, int arrayIndex)
	{
		for (int i = 0; i < data.Count; ++i)
		{
			array.SetValue (data[i], arrayIndex);
			arrayIndex = arrayIndex + 1;
		}
	}
	
	public bool Remove (T item)
	{
		return data.Remove (item);
	}
	
	public bool IsReadOnly 
    {
		get 
        {
			return false;
		}
	}
	#endregion
	
	#region IEnumerable implementation
	IEnumerator IEnumerable.GetEnumerator ()
	{
		return data.GetEnumerator ();
	}
	#endregion
	
}