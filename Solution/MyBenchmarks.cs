using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

public static class Generators
{
    public static int[] GenerateRandom(int size, int minVal, int maxVal)
    {
        int[] a = new int[size];
        Random rnd = new Random(42); 
        for (int i = 0; i < size; i++)
        {
            a[i] = rnd.Next(minVal, maxVal);
        }
        return a;
    }

    public static int[] GenerateSorted(int size, int minVal, int maxVal)
    {
        int[] a = GenerateRandom(size, minVal, maxVal);
        Array.Sort(a);
        return a;
    }

    public static int[] GenerateReversed(int size, int minVal, int maxVal)
    {
        int[] a = GenerateSorted(size, minVal, maxVal);
        Array.Reverse(a);
        return a;
    }

    public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal)
    {
        int[] a = GenerateSorted(size, minVal, maxVal);
        Random rnd = new Random(42);
        
        int swaps = Math.Max(1, (int)(size * 0.05)); 
        
        for (int i = 0; i < swaps; i++)
        {
            int idx1 = rnd.Next(size);
            int idx2 = rnd.Next(size);
            int temp = a[idx1];
            a[idx1] = a[idx2];
            a[idx2] = temp;
        }
        return a;
    }

    public static int[] GenerateFewUnique(int size)
    {
        return GenerateRandom(size, 1, 10);
    }
}

[MemoryDiagnoser]
[CsvExporter]  // Wymusza generowanie pliku CSV dla Excela
[HtmlExporter] // Wymusza generowanie pliku HTML dla ładnych screenów
public class SortingBenchmarks
{
    [Params(10, 1000, 100000)]
    public int N;

    [Params("Random", "Sorted", "Reversed", "AlmostSorted", "FewUnique")]
    public string DataType;

    private int[] _unsortedData;

    [GlobalSetup]
    public void Setup()
    {
        switch (DataType)
        {
            case "Random":
                _unsortedData = Generators.GenerateRandom(N, 0, 100000);
                break;
            case "Sorted":
                _unsortedData = Generators.GenerateSorted(N, 0, 100000);
                break;
            case "Reversed":
                _unsortedData = Generators.GenerateReversed(N, 0, 100000);
                break;
            case "AlmostSorted":
                _unsortedData = Generators.GenerateAlmostSorted(N, 0, 100000);
                break;
            case "FewUnique":
                _unsortedData = Generators.GenerateFewUnique(N);
                break;
        }
    }

    [Benchmark(Baseline = true)] 
    //QuickSort
    public void DotNetArraySort()
    {
        int[] kopia = (int[])_unsortedData.Clone(); 
        Array.Sort(kopia);
    }

    //InsertionSort
    [Benchmark]
    public void InsertionSort()
    {
        int[] kopia = (int[])_unsortedData.Clone();
        
        int n = kopia.Length;
        for (int i = 1; i < n; ++i) 
        {
            int key = kopia[i];
            int j = i - 1;

            while (j >= 0 && kopia[j] > key) 
            {
                kopia[j + 1] = kopia[j];
                j = j - 1;
            }
            kopia[j + 1] = key;
        }
    }

    //MergeSort
    [Benchmark]
    public void MergeSortTest()
    {
        int[] kopia = (int[])_unsortedData.Clone();
        MergeSort(kopia, 0, kopia.Length - 1);
    }

    private void MergeSort(int[] brudne, int left, int right)
    {
        if (left < right)
        {
            int middle = left + (right - left) / 2;
            MergeSort(brudne, left, middle);
            MergeSort(brudne, middle + 1, right);
            Merge(brudne, left, middle, right);
        }
    }

    private void Merge(int[] brudne, int left, int middle, int right)
    {
        int[] leftArray = new int[middle - left + 1];
        int[] rightArray = new int[right - middle];

        Array.Copy(brudne, left, leftArray, 0, middle - left + 1);
        Array.Copy(brudne, middle + 1, rightArray, 0, right - middle);

        int indexLewej = 0; 
        int indexPrawej = 0;
        int indexGlownej = left;

        while (indexLewej < leftArray.Length && indexPrawej < rightArray.Length)
        {
            if (leftArray[indexLewej] <= rightArray[indexPrawej])
            {
                brudne[indexGlownej++] = leftArray[indexLewej++];
            }
            else
            {
                brudne[indexGlownej++] = rightArray[indexPrawej++];
            }
        }
        
        while (indexLewej < leftArray.Length)
        {
            brudne[indexGlownej++] = leftArray[indexLewej++];
        }
        
        while (indexPrawej < rightArray.Length)
        {
            brudne[indexGlownej++] = rightArray[indexPrawej++];
        }
    }

    //QuickSortClassical
    [Benchmark]
    public void QuickSortClassical()
    {
        int[] kopia = (int[])_unsortedData.Clone();
        QuickSort(kopia, 0, kopia.Length - 1);
    }

    private void QuickSort(int[] arr, int low, int high)
    {
        while (low < high)
        {
            int pi = Partition(arr, low, high);
            
            // Rekurencyjnie sortujemy tylko MNIEJSZĄ połówkę
            if (pi - low < high - pi)
            {
                QuickSort(arr, low, pi - 1);
                low = pi + 1;
            }
            else
            {
                QuickSort(arr, pi + 1, high);
                high = pi - 1;
            }
        }
    }

    private int Partition(int[] arr, int low, int high)
    {
        int pivot = arr[high]; 
        int i = (low - 1);

        for (int j = low; j <= high - 1; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                int temp = arr[i];
                arr[i] = arr[j];
                arr[j] = temp;
            }
        }
        int temp1 = arr[i + 1];
        arr[i + 1] = arr[high];
        arr[high] = temp1;
        
        return i + 1;
    }
}