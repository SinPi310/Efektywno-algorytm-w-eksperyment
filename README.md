# Zadanie: Efektywność algorytów - eksperyment

Projekt to badanie empiryczne (doświadczalne) efektywności 3 klasycznych algorytmów sortowania:
## Badane algorytmy
 - **InsertionSort** (zaimplementuj: https://www.geeksforgeeks.org/insertion-sort/)
 - **MergeSort** (zaimplementuj: https://www.geeksforgeeks.org/merge-sort/)
 - **QuickSortClassical** klasyczny (zaimplementuj: https://www.geeksforgeeks.org/quick-sort)
 - **QuickSort** (wariant heurystyczny, zaimplementowany w bibliotekach C# - `Array.Sort()`)

## Kryteria testowe
Przyjmij, że sortowane rosnąco będą tablice liczb całkowitych (typ `int`) spełniające następujące kryteria:

**Różny rozmiar danych, przyjmij:**
 - tablica mała (np. **10-elementowa**),
 - średnia (np. **1000-elementowa**),
 - duża (np. **100000-elementowa**)

**Liczby w tablicy będą losowe oraz:**
1. **(random)** rozmieszczone losowo lub
2. **(sorted)** już posortowane rosnąco lub
3. **(reversed)** posortowane w odwrotnym porządku (malejąco) lub
4. **(almost sorted)** prawie posortowane rosnąco, tzn. tylko kilka (niewielki procent) wartości będzie
5. **(few unique)** w tablicy będzie niewiele wartości unikalnych (np. tablica 100-elementowa liczb z zakresu od 1 do 10, wartości powtarzają się wielokrotnie)


## Wyniki pomiarów

## Wnioski
