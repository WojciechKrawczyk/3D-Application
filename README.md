# 3D-Application
Animation of objects in 3D scene. Different light and shading models are used.

Polish description:
1.	Tematyka projektu
Projekt jest realizacją prostego silnika graficznego dla aplikacji 3D na CPU.

2.	Funkcjonalności aplikacji
•	Aplikacja wspiera importowanie modeli z programu Blender.
Dla każdego modelu zapisywane są współrzędne jego wierzchołków razem z wektorami normalnymi oraz podział wierzchołków na trójkątne ściany będące podstawą w procesie cieniowania.  
•	Dla każdego modelu można zdefiniować szereg unikalnych właściwości:
  - Skala
  - Pozycja startowa
  -	Przesunięcie (niezależne) wzdłuż każdej osi układu podczas renderowania kolejnych klatek
  -	Obrót (niezależnie) wokół każdej osi układu podczas renderowania kolejnych klatek
  -	Parametry cieniowania (kolor, współczynniki ka, kd, ks oraz parametr m).
•	Tworzenie źródeł światła (punktowe, reflektor).
•	Tworzenie statycznych, dynamicznych lub śledzących ruchomy obiekt kamer obserwujących scenę.
•	Mgła.

3.	Zastosowane algorytmy 
Przekształcenia obiektów:
Aplikacja wykorzystuje standardową technikę do prezentacji obiektów 3D. Odbywa się to za pomocą przekształceń wykorzystujących macierze: modelu, widoku oraz projekcji. 

Cieniowanie:
Podstawowymi algorytmami wykorzystywanymi w projekcie są algorytmy cieniowania: 
•	cieniowanie stałe
•	cieniowanie Gourauda
•	cieniowanie Phonga
W każdym algorytmie wykorzystywany jest model oświetlenia Phonga. 
https://en.wikipedia.org/wiki/Phong_reflection_model
W przypadku cieniowania stałego model oświetlenia Phonga jest wykorzystywany tylko dla jednego wierzchołka dla każdego trójkąta. Współrzędne i wektory normalne tego wierzchołka są obliczane za pomocą średniej arytmetycznej z trzech wierzchołków danego trójkąta. Cały trójkąt jest wypełniony jednolitym kolorem.

W przypadku cieniowania Gourauda właściwe kolory są obliczane dla każdego wierzchołka trójkąta, a następnie interpolowane do jego wnętrza. 

W przypadku cieniowania Phonga dla każdego punktu wewnątrz trójkąta interpolowane są współrzędne oraz wektory normalne, a następnie odbywa się wyliczanie adekwatnego koloru dla każdego punktu z osobna. 

Wyświetlanie obrazu:
Do poprawnego wyświetlania modeli (zachowania relacji przesłaniania) został wykorzystany algorytm Z-bufor. 

Mgła :
Mgła została zaimplementowana jako odpowiednia domieszka zdefiniowanego wcześniej koloru mgły (FromArgb(171, 174, 176)) do uzyskanego wcześniej koloru piksela, która jest zależna od odległości obserwatora od danego punktu. 
https://old.cescg.org/CESCG-2004/papers/34_ZdrojewskaDorota.pdf

4.	Architektura i rozwiązania techniczne
Poniżej znajduje się krótki opis najważniejszych klas realizujących istotę działania programu:
•	Device – jej zadaniem jest symulacja urządzenia odpowiedzialnego za wyświetlanie obrazu, przechowuje ona odpowiedni rozmiar okna oraz bufor głębokości, do obiektu przekazywane są parametry oświetlenia oraz kamery występujących w danym momencie na scenie, następnie z częstotliwością globalnego Timera wywoływana jest funkcja Render, mająca na celu wygenerowanie kolejnej klatki obrazu, klasa jest również wyposażona w metodę umożliwiającą „ładowanie” obiektów z Blendera.
•	Mesh – przechowuje właściwości modeli znajdujących się na scenie.
•	ShadingMachine – abstrakcyjna klasa umożliwiająca łatwy i szybki wybór algorytmu cieniowania, klasy realizujące konkretne algorytmy cieniowania dziedziczą po klasie ShadingMachine.
•	FogMachine – abstrakcyjna klasa umożliwiająca włączanie i wyłączanie efektu mgły oraz łatwą rozszerzalność w przypadku dodania innych metod realizacji efektu mgły.

5.	Instrukcja obsługi aplikacji
Kontrola działania aplikacji odbywa się za pomocą panelu znajdującego się po prawej stronie okna aplikacji. W panelu użytkownik może dokonać wyboru: typu cieniowania, aktualnej kamery obserwującej scenę, efektu mgły. Istnieje możliwość obserwowania sceny z podłożem (podłogą) lub bez niej. 
