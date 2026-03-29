# Zadanie: Efektywno-algorytm-w-eksperyment

Zbadaj empirycznie (doświadczalnie) efektywność 3 klasycznych algorytmów sortowania:

 - InsertionSort (zaimplementuj: https://www.geeksforgeeks.org/insertion-sort/)
 - MergeSort (zaimplementuj: https://www.geeksforgeeks.org/merge-sort/)
 - QuickSortClassical klasyczny (zaimplementuj: https://www.geeksforgeeks.org/quick-sort)
 - QuickSort (wariant heurystyczny, zaimplementowany w bibliotekach C# - `Array.Sort()`)

Przyjmij, że sortowane rosnąco będą tablice liczb całkowitych (typ `int`) spełniające następujące kryteria:
 - różny rozmiar danych, przyjmij: tablica mała (np. **10-elementowa**), średnia (np. **1000-elementowa**), duża (np. **100000-elementowa**)
 - liczby w tablicy będą losowe oraz:
    1. **(random)** rozmieszczone losowo lub
    2. **(sorted)** już posortowane rosnąco lub
    3. **(reversed)** posortowane w odwrotnym porządku (malejąco) lub
    4. **(almost sorted)** prawie posortowane rosnąco, tzn. tylko kilka (niewielki procent) wartości będzie zaburzonych/zamienionych
    5. **(few unique)** w tablicy będzie niewiele wartości unikalnych (np. tablica 100-elementowa liczb z zakresu od 1 do 10, wartości powtarzają się wielokrotnie)

> [!WARNING]
> w zależności od możliwości Twojego komputera możesz zmienić wielkości tablic na większe/mniejsze, aby zobaczyć efekty.

Uwzględniając podane kryteria będziesz musiał wykonać 15 różnych pomiarów - określić czasy wykonania algorytmów w 15 różnych sytuacjach.

Utwórz projekt, który wykorzysta framework BenchmarkDotNet do przeprowadzenia eksperymentu. Zobacz: https://benchmarkdotnet.org/articles/overview.html

## **Podpowiedzi**
  - Losowanie liczb: https://docs.microsoft.com/pl-pl/dotnet/api/system.random
  - Sortowanie tablic: https://docs.microsoft.com/pl-pl/dotnet/api/system.array.sort
  - Klonowanie tablic: https://docs.microsoft.com/pl-pl/dotnet/api/system.array.clone
  - BenchmarkDotNet: https://benchmarkdotnet.org/articles/overview.html
  - BenchmarkDotNet parametrization: https://benchmarkdotnet.org/articles/features/parameterization.html
  - BenchmarkDotNet prezentuje wyniki eksperymentu na konsoli oraz zapisuje je w formie plików CSV, markdown i html w podfolderze .\BenchmarkDotNet.Artifacts\results.

## **Proponowane kroki wykonania zadania**
Utwórz w projekcie:

  - plik z klasą public static class Generators, w której umieścisz kod metod generujących dane testowe
  - plik z klasą public class SortingAlgorithms, w której umieścisz implementacje algorytmów sortujących. Algorytmy te adnotujesz atrybutem [Benchmark] z BenchmarkDotNet
W klasie Program zaimplementujesz sam program wykonujący obliczenia i generujący raport.

## **Generatory tablic dla różnych wariantów**
Musisz napisać metody, które wygenerują dane dla eksperymentu. Zacznij od tablicy liczb losowych.

``` Csharp
public static int[] GenerateRandom(int size, int minVal, int maxVal)
{
    int[] a = new int[size];
    for( ... )
    ...
    return a;
}
```

Mając tablicę liczb losowych możesz ją wykorzystać w pozostałych sytuacjach, np.
``` Csharp
public static int[] GenerateSorted(int size, int minVal, int maxVal)
{
    int[] a = GenerateRandom(size, minVal, maxVal);
    Array.Sort(a);
    return a;
}
```

lub

```Csharp
public static int[] GenerateReversed(int size, int minVal, int maxVal)
{
    int[] a = GenerateSorted(size, minVal, maxVal);
    Array.Reverse(a);
    return a;
}
```

Przetestuj utworzone generatory.

Do oceny przesyłasz:
  1. Skompresowane solution w formacie .zip bez niepotrzebnych kodów binarnych. Jeśli zadanie to realizujesz jako projekt na GitHub, pobierz ten .zip z witryny GitHub.
  2. Plik .pdf ze sprawozdaniem, w którym znajdzie się:
    - Twoje imię, nazwisko, numer albumu, nazwa grupy laboratoryjnej
    - opis eksperymentu
    - opis implementacji (konfiguracji środowiska do benchmarku, implementacji algorytmów, generowania danych testowych,           wykorzystania BenchmarkDotNet)
    - wyniki pomiarów (uruchomionych testów wydajnościowych) - mogą być screenshoty
    - wnioski
  3. Link do repozytorium na GitHubie (jeśli realizujesz zadanie w ten sposób)

https://www.toptal.com/developers/sorting-algorithms
