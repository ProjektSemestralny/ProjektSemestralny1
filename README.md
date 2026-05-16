# System rezerwacji zajęć sportowych

## Opis projektu

Projekt jest aplikacją konsolową napisaną w języku C#.

Celem programu jest zarządzanie rezerwacjami zajęć sportowych odbywających się na obiekcie sportowym. Aplikacja pozwala użytkownikowi przeglądać dostępne zajęcia, rezerwować miejsca, modyfikować oraz anulować rezerwacje, a także generować raport zajętości wybranego zasobu w danym miesiącu.

Projekt został wykonany bez użycia frameworków GUI, web ani mobile. Interakcja z użytkownikiem odbywa się przez konsolę.

---

## Technologie

- C#
- .NET
- Aplikacja konsolowa
- Pliki CSV
- Git
- GitHub

---

## Główne funkcjonalności

Program umożliwia:

- przeglądanie grafiku zajęć sportowych,
- tworzenie rezerwacji,
- wyświetlanie listy rezerwacji,
- anulowanie rezerwacji,
- modyfikowanie rezerwacji,
- dodawanie nowych zajęć,
- usuwanie zajęć,
- kontrolę dostępności miejsc,
- generowanie raportu zajętości zasobu w wybranym miesiącu,
- zapisywanie i odczytywanie danych z plików CSV.

---

## Menu programu

Po uruchomieniu aplikacji użytkownik widzi menu:

1. Pokaz grafik
2. Zarezerwuj miejsce
3. Pokaz rezerwacje
4. Anuluj rezerwacje
5. Modyfikuj rezerwacje
6. Dodaj nowe zajecia
7. Usun zajecia
8. Raport zajetosci zasobu
9. Wyjscie

---

## Opis działania funkcji

### 1. Pokaz grafik

Opcja wyświetla grafik dostępnych zajęć sportowych.

Dla każdych zajęć pokazywane są:

- ID zajęć,
- nazwa zajęć,
- poziom,
- data i godzina,
- czas trwania,
- liczba zajętych i dostępnych miejsc,
- prowadzący,
- miejsce.

Grafik jest wyświetlany w formie kafelków, aby był bardziej czytelny w konsoli.

---

### 2. Zarezerwuj miejsce

Opcja pozwala użytkownikowi zarezerwować miejsce na wybranych zajęciach.

Program sprawdza:

- czy podane ID zajęć istnieje,
- czy na wybranych zajęciach są wolne miejsca,
- czy imię i nazwisko nie jest puste.

Po poprawnym zapisaniu rezerwacja zostaje dodana do pliku rezerwacje.csv.

---

### 3. Pokaz rezerwacje

Opcja wyświetla listę wszystkich dokonanych rezerwacji.

Dla każdej rezerwacji pokazywane są:

- numer rezerwacji,
- imię i nazwisko osoby,
- nazwa zajęć,
- data zajęć,
- miejsce zajęć,
- data utworzenia rezerwacji.

---

### 4. Anuluj rezerwacje

Opcja pozwala anulować istniejącą rezerwację.

Program wyświetla listę rezerwacji i prosi użytkownika o podanie numeru rezerwacji do usunięcia.

Po anulowaniu rezerwacji plik rezerwacje.csv zostaje zaktualizowany.

---

### 5. Modyfikuj rezerwacje

Opcja pozwala zmienić dane istniejącej rezerwacji.

Użytkownik może zmienić:

- imię i nazwisko osoby rezerwującej,
- zajęcia przypisane do rezerwacji.

Przy zmianie zajęć program sprawdza, czy nowe zajęcia istnieją oraz czy są na nich wolne miejsca.

---

### 6. Dodaj nowe zajecia

Opcja pozwala dodać nowe zajęcia do grafiku.

Użytkownik podaje:

- nazwę zajęć,
- poziom,
- datę i godzinę,
- czas trwania,
- limit miejsc,
- prowadzącego,
- miejsce.

Program sprawdza poprawność danych oraz to, czy nie istnieją już zajęcia w tym samym miejscu o tej samej godzinie.

Po dodaniu nowych zajęć plik grafik.csv zostaje zaktualizowany.

---

### 7. Usun zajecia

Opcja pozwala usunąć zajęcia z grafiku.

Program nie pozwala usunąć zajęć, jeżeli istnieją już na nie rezerwacje. W takim przypadku trzeba najpierw anulować rezerwacje przypisane do tych zajęć.

Po usunięciu zajęć plik grafik.csv zostaje zaktualizowany.

---

### 8. Raport zajetosci zasobu

Opcja generuje raport zajętości wybranego zasobu w danym miesiącu.

Zasobem może być na przykład:

- Kort 1,
- Kort 2,
- Sala fitness,
- Sala walk,
- Boisko,
- Hala glowna.

Użytkownik podaje:

- nazwę zasobu,
- rok,
- miesiąc.

Program wyświetla:

- zajęcia odbywające się w danym zasobie,
- liczbę rezerwacji dla każdych zajęć,
- liczbę wszystkich miejsc,
- liczbę zajętych miejsc,
- procentową zajętość zasobu.

---

## Persystencja danych

Program zapisuje dane w plikach CSV.

W projekcie wykorzystywane są dwa pliki:

- grafik.csv
- rezerwacje.csv

---

## Plik grafik.csv

Plik grafik.csv przechowuje informacje o dostępnych zajęciach sportowych.

Format pliku:

Id;Nazwa;Poziom;Data;CzasTrwaniaMinuty;LimitMiejsc;Prowadzacy;Miejsce

Przykładowy rekord:

1;Tenis;Poczatkujacy;2026-05-20 10:00;60;2;Jan Kowalski;Kort 1

Znaczenie kolumn:

- Id - unikalny identyfikator zajęć,
- Nazwa - nazwa zajęć,
- Poziom - poziom zaawansowania,
- Data - data i godzina zajęć,
- CzasTrwaniaMinuty - czas trwania zajęć w minutach,
- LimitMiejsc - maksymalna liczba uczestników,
- Prowadzacy - osoba prowadząca zajęcia,
- Miejsce - miejsce odbywania się zajęć.

---

## Plik rezerwacje.csv

Plik rezerwacje.csv przechowuje informacje o dokonanych rezerwacjach.

Format pliku:

IdZajec;ImieNazwisko;DataRezerwacji

Przykładowy rekord:

1;Jan Nowak;2026-05-20 09:30

Znaczenie kolumn:

- IdZajec - ID zajęć, na które dokonano rezerwacji,
- ImieNazwisko - imię i nazwisko osoby rezerwującej,
- DataRezerwacji - data utworzenia rezerwacji.

---

## Automatyczne tworzenie plików

Jeżeli pliki CSV nie istnieją przy pierwszym uruchomieniu programu, aplikacja automatycznie tworzy:

- przykładowy plik grafik.csv,
- pusty plik rezerwacje.csv z nagłówkiem.

Dzięki temu projekt może zostać uruchomiony na nowym komputerze bez ręcznego tworzenia plików wejściowych.

---

## Przykładowy grafik

Przykładowy grafik zawiera między innymi zajęcia:

- Tenis,
- Badminton,
- Squash,
- Tenis stołowy,
- Zdrowe plecy,
- Yoga,
- Boks,
- Judo,
- Gimnastyka,
- Piłka nożna,
- Koszykówka.

Przykładowe miejsca:

- Kort 1,
- Kort 2,
- Kort squash 1,
- Hala A,
- Sala fitness,
- Sala walk,
- Sala gimnastyczna,
- Boisko,
- Hala glowna.

---

## Walidacja danych

Program zawiera walidację danych wejściowych.

Sprawdzane są między innymi:

- czy pola tekstowe nie są puste,
- czy podana data ma poprawny format,
- czy liczby są poprawne,
- czy liczby są większe od zera,
- czy wybór z menu mieści się w dozwolonym zakresie,
- czy istnieją zajęcia o podanym ID,
- czy istnieje rezerwacja o podanym numerze,
- czy na wybranych zajęciach są wolne miejsca,
- czy można usunąć dane zajęcia,
- czy istnieją zajęcia dla danego zasobu w wybranym miesiącu.

W przypadku błędnych danych program prosi użytkownika o ponowne wpisanie wartości, zamiast od razu przerywać operację.

---

## Obsługa błędów

Program obsługuje typowe błędy użytkownika, takie jak:

- wpisanie tekstu zamiast liczby,
- podanie pustego pola,
- podanie nieistniejącego ID zajęć,
- próba rezerwacji zajęć bez wolnych miejsc,
- próba usunięcia zajęć, na które istnieją rezerwacje,
- podanie błędnej daty,
- podanie nieistniejącego zasobu w raporcie.

---

## Kontrola dostępności

Dostępność miejsc jest kontrolowana na podstawie liczby rezerwacji przypisanych do danych zajęć.

Program porównuje liczbę rezerwacji z limitem miejsc zapisanym w grafiku.

Przykład:

Miejsca: 1/2

Oznacza to, że jedna osoba jest już zapisana, a limit miejsc wynosi dwa.

Jeżeli liczba rezerwacji osiągnie limit miejsc, program nie pozwoli dodać kolejnej rezerwacji.

---

## Jak uruchomić projekt

1. Sklonuj repozytorium z GitHuba.
2. Otwórz projekt w Visual Studio.
3. Upewnij się, że otwierasz plik projektu .csproj lub rozwiązanie .sln, a nie sam folder.
4. Uruchom projekt jako aplikację konsolową.
5. Korzystaj z menu w konsoli.

Program sam utworzy potrzebne pliki CSV, jeśli nie będą jeszcze istnieć.

---

## Przykładowe użycie programu

### Rezerwacja miejsca

1. Wybierz opcję 2. Zarezerwuj miejsce.
2. Program pokaże listę dostępnych zajęć.
3. Podaj ID zajęć.
4. Podaj imię i nazwisko.
5. Program zapisze rezerwację do pliku rezerwacje.csv.

### Anulowanie rezerwacji

1. Wybierz opcję 4. Anuluj rezerwacje.
2. Program pokaże listę rezerwacji.
3. Podaj numer rezerwacji.
4. Program usunie rezerwację i zapisze zmiany w pliku CSV.

### Raport zajętości

1. Wybierz opcję 8. Raport zajetosci zasobu.
2. Podaj nazwę miejsca, np. Sala fitness.
3. Podaj rok, np. 2026.
4. Podaj miesiąc, np. 5.
5. Program wyświetli raport zajętości.

---

## Zgodność z wymaganiami projektu

Projekt spełnia wymagania:

- persystencja danych - dane zapisywane są do plików CSV,
- obsługa błędów - program reaguje na błędne dane wejściowe,
- walidacja inputu - program sprawdza poprawność danych użytkownika,
- system kontroli wersji - projekt jest prowadzony w GitHubie,
- aplikacja konsolowa - projekt nie używa GUI, web ani mobile,
- przeglądanie dostępnych zasobów - użytkownik może przeglądać grafik,
- tworzenie rezerwacji - użytkownik może zapisać się na zajęcia,
- modyfikowanie rezerwacji - użytkownik może zmienić dane rezerwacji,
- anulowanie rezerwacji - użytkownik może usunąć rezerwację,
- kontrola dostępności zasobów - program sprawdza liczbę wolnych miejsc,
- generowanie raportu zajętości zasobu w danym miesiącu - program generuje raport dla wybranego miejsca.

---

## Autorzy

Projekt wykonany w ramach zajęć jako aplikacja konsolowa do zarządzania rezerwacjami zasobów sportowych.

Autorzy:

- Jakub Wójcik 
- Eryk Kośla
