using NUnit.Framework;

using Unity.Collections;
using UnityEngine;

using Orazum.Collections;

public class TestCollectionsUtils
{
    [Test]
    public void Array2DTests()
    {
        Array2D<Vector2Int> arr = new Array2D<Vector2Int>(3, 6);
        for (int row = 0; row < arr.RowCount; row++)
        {
            for (int col = 0; col < arr.ColCount; col++)
            {
                arr[col, row] = new Vector2Int(col, row);
                Assert.AreEqual(arr[col, row], new Vector2Int(col, row));
            }
        }

        for (int col = 0; col < arr.ColCount; col++)
        {
            for (int row = 0; row < arr.RowCount; row++)
            {
                Assert.AreEqual(arr[col, row], new Vector2Int(col, row));
            }
        }

        for (int col = 0; col < arr.ColCount; col++)
        {
            for (int row = 0; row < arr.RowCount; row++)
            {
                arr[col, row] += Vector2Int.down + Vector2Int.right;
            }
        }


        for (int row = 0; row < arr.RowCount; row++)
        {
            for (int col = 0; col < arr.ColCount; col++)
            {
                Assert.AreEqual(arr[col, row], new Vector2Int(col + 1, row - 1));
            }
        }
    }

    [Test]
    public void Reverse()
    {
        int evenCount = 10;
        using (NativeArray<int> toReverseEven = InitializeNativeArray(evenCount))
        { 
            CollectionUtilities.ReverseNativeArray(toReverseEven);
            for (int i = 0; i < evenCount; i++)
            {
                Assert.AreEqual(evenCount - 1 - i, toReverseEven[i]);
            }
        }

        int oddCount = 7;
        using (NativeArray<int> toReverseOdd = InitializeNativeArray(oddCount))
        { 
            CollectionUtilities.ReverseNativeArray(toReverseOdd);
            for (int i = 0; i < oddCount; i++)
            {
                Assert.AreEqual(oddCount - 1 - i, toReverseOdd[i]);
            }
        }
    }

    [Test]
    public void GetSlice()
    {
        int totalCount = 10;
        using (NativeArray<int> total = InitializeNativeArray(totalCount))
        {
            using (NativeArray<int> slice_1 = CollectionUtilities.GetSlice(total, 0, 3))
            {
                for (int i = 0; i < 3; i++)
                {
                    Assert.AreEqual(i, slice_1[i]);
                }
            }

            using (NativeArray<int> slice_2 = CollectionUtilities.GetSlice(total, 2, 7))
            {
                for (int i = 0; i < 7; i++)
                {
                    Assert.AreEqual(i + 2, slice_2[i]);
                }
            }
        }

        totalCount = 5;
        using (NativeArray<int> total = InitializeNativeArray(totalCount))
        { 
            using (NativeArray<int> slice_1 = CollectionUtilities.GetSlice(total, 3, 2))
            {
                for (int i = 0; i < 2; i++)
                {
                    Assert.AreEqual(i + 3, slice_1[i]);
                }
            }

            using (NativeArray<int> slice_2 = CollectionUtilities.GetSlice(total, 0, 5))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.AreEqual(i, slice_2[i]);
                }
            }
        }
    }

    private NativeArray<int> InitializeNativeArray(int itemCount)
    { 
        NativeArray<int> toReturn = new NativeArray<int>(itemCount, Allocator.Temp);
        for (int i = 0; i < itemCount; i++)
        {
            toReturn[i] = i;
        }
        return toReturn;
    }
}
