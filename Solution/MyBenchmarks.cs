using System;
using BenchmarkDotNet.Attributes;

[MemoryDiagnoser]
public class SortingBenchmarks
{
    // Testujemy dla 100, 1000 i 5000 elementów
    [Params(100, 1000, 5000)]
    public int N;

    private int[] _unsortedData;

    // Ta metoda odpala się raz przed testami. Tworzy nam naszą "brudną" tablicę.
    [GlobalSetup]
    public void Setup()
    {
        _unsortedData = new int[N];
        Random rnd = new Random(42); // Stałe ziarno = zawsze te same "losowe" liczby
        for (int i = 0; i < N; i++)
        {
            _unsortedData[i] = rnd.Next(0, 10000);
        }
    }

    [Benchmark(Baseline = true)] // Baseline znaczy, że do tego wbudowanego algorytmu będziemy porównywać resztę
    public void DotNetArraySort()
    {
        int[] kopia = (int[])_unsortedData.Clone(); // Klonujemy, żeby nie zepsuć oryginału
        Array.Sort(kopia);
    }

    // Insertion Sort
    [Benchmark]
    public void InsertionSort()
    {
        int[] kopia = (int[])_unsortedData.Clone();
        
        int n = kopia.Length;
        for (int i = 1; i < n; ++i) 
        {
            int key = kopia[i];
            int j = i - 1;

            /* Move elements of kopia[0..i-1], that are
               greater than key, to one position ahead
               of their current position */
            while (j >= 0 && kopia[j] > key) 
            {
                kopia[j + 1] = kopia[j];
                j = j - 1;
            }
            kopia[j + 1] = key;
        }
    }

    // Merge Sort
    [Benchmark]
    
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

    // Metody pomocnicze dla Merge Sort
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

    public void MergeSortTest()
    {
        int[] kopia = (int[])_unsortedData.Clone();
        MergeSort(kopia, 0, kopia.Length - 1);
    }
}