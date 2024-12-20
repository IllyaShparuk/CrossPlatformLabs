# Варіант 27

## Лаб 1:

### Опис

Для заданих натуральних чисел $N$ і $K$ потрібно обчислити кількість чисел від 1 до $N$, що мають у двійковому записі
рівно $K$ нулів.

#### Приклад:

Якщо $N = 8$ і $K = 1$, можна записати всі числа від 1 до 8 в двійковій системі числення:

1, 10, 11, 100, 101, 110, 111 та 1000.

Числа 10, 101 і 110 мають рівно один нуль у записі, тому правильна відповідь — 3.

### Вхідні дані

У єдиному рядку вхідного файлу `INPUT.TXT` записано два натуральні числа через пропуск $N$ і $K$, що не
перевищують $10^9$.

### Вихідні дані

У єдиний рядок вихідного файлу `OUTPUT.TXT` потрібно вивести одне ціле число — кількість чисел від 1 до $N$ з $K$ нулями
в двійковому поданні.

### Обмеження

- $0 < N \leq 10^9$
- $0 \leq K \leq 10^9$

### Приклади

| № | INPUT.TXT | OUTPUT.TXT |
|---|-----------|------------|
| 1 | 8 1       | 3          |
| 2 | 1000 5    | 210        | 

## Лаб 2:

### Опис

На вершині драбинки, що містить $N$ сходинок, знаходиться м'ячик, який починає стрибати по них вниз, до основи. М'ячик
може стрибнути на наступну сходинку, на сходинку через одну чи дві. Тобто, якщо м'ячик лежить на 8 сходинці, то він може
переміститися на 5, 6 або 7.
Потрібно написати програму, яка визначить кількість усіляких "маршрутів" м'ячика з вершини на землю.

### Вхідні дані

Вхідний файл `INPUT.TXT` містить число $N (0 < N \leq 70)$.

### Вихідні дані

Вихідний файл `OUTPUT.TXT` повинен містити потрібне число.

### Приклади

| № | INPUT.TXT | OUTPUT.TXT |
|---|-----------|------------|
| 1 | 1         | 1          |
| 2 | 4         | 7          | 

## Лаб 3:

### Опис

В одному із парків одного великого міста нещодавно було організовано новий атракціон Кольоровий лабіринт. Він
складається з $n$ кімнат, з'єднаних $m$ двонаправленими коридорами. Кожен із коридорів пофарбований в один із ста
кольорів, при цьому від кожної кімнати відходить не більше одного коридору кожного кольору. При цьому дві кімнати можуть
бути з'єднані будь-якою кількістю коридорів.

Людина, яка купила квиток на атракціон, опиняється у кімнаті номер один. Крім квитка, він також отримує опис шляху, яким
він може вибратися з лабіринту. Цей опис є послідовністю кольорів $c_{1}…c_{k}$. Користуватися їй треба так: перебуваючи
в кімнаті, треба подивитися на черговий колір у цій послідовності, вибрати коридор такого кольору та піти ним. При цьому
якщо з кімнати не можна піти коридором відповідного кольору, то людині доводиться далі самій вибирати, куди йти.

Останнім часом до адміністрації парку почали часто надходити скарги від людей, що заблукали в лабіринті. У зв'язку з цим
виникла необхідність написання програми, що перевіряє коректність опису та шляху, і, у разі її коректності, повідомляє
номер кімнати, до якої веде шлях.

Опис шляху некоректно, якщо на шляху, який він описує, виникає ситуація, коли з кімнати не можна піти коридором
відповідного кольору.

### Вхідні дані

Перший рядок вхідного файлу `INPUT.TXT` містить два цілих числа $n$ $(1 \leq n \leq 10000)$
та $m$ $(1 \leq m \leq 100000)$ - відповідно кількість кімнат та коридорів у лабіринті. Наступні рядки $m$ містять описи
коридорів. Кожен опис містить три числа $u$ $(1 \leq u \leq n)$, $v$ $(1 \leq v \leq n)$, $c$ $(1 \leq c \leq 100)$ -
відповідно номери кімнат, з'єднаних цим коридором, та колір коридору. Наступний $(m+2)$-ий рядок вхідного файлу містить
довжину опису шляху - ціле число $k$ $(0 \leq k \leq 100000)$. Останній рядок вхідного файлу містить до цілих чисел,
розділених пробілами, - опис шляху лабіринтом.

### Вихідні дані

У вихідний файл `OUTPUT.TXT` виведіть рядок **INCORRECT**, якщо опис шляху некоректний, інакше виведіть номер кімнати,
до якої веде описаний шлях. Пам'ятайте, що шлях починається в номері номер один.

### Приклади

| № | INPUT.TXT                                    | OUTPUT.TXT |
|---|----------------------------------------------|------------|
| 1 | 3 2<br>1 2 10<br>1 3 5<br>5<br>10 10 10 10 5 | 3          |
| 2 | 3 2<br>1 2 10<br>2 3 5<br>5<br>5 10 10 10 10 | INCORRECT  | 
| 3 | 3 2<br>1 2 10<br>2 3 5<br>4<br>10 10 10 5    | INCORRECT  |

## Лаб 4:

### Опис

Створити консольний застосунок, що задовольнятиме наступним вимогам.

1. Складається з двох проєктів:
   - **Безпосередньо консольний додаток**
   - **Бібліотека класів**, що дає змогу запускати практичні 1, 2 або 3
2. Пакується як **Nuget застосунок** (`dotnet tool`) та публікується у приватному **Nuget репозитарії**
3. Має наступний консольний інтерфейс:
   - При передачі команди `version` виводить інформацію про програму:
      1. Автора
      2. Версію
   - При передачі команди `run` очікує підкоманду `lab1`, `lab2` або `lab3` для запуску відповідної практичної:
      1. Має необов'язкові параметри:
         - `-I` або `--input` — інпут файл
         - `-o` або `--output` — аутпут файл
   - При передачі команди `set-path` очікує обов'язковий параметр:
      - `-p` або `--path` — задає шлях до теки з інпут та аутпут файлами.  
        Отриманий шлях виставляється в змінну середовища з ім'ям `LAB_PATH`.
4. Пріоритетність шляху:

   a. Якщо шлях до файлу заданий параметрами консолі, то саме його слід застосувати.  
   b. Якщо параметри консолі не задані, то слід перевірити значення змінної `LAB_PATH`.  
   c. Якщо пункти a та b не задані, то слід пошукати файли спочатку в домашній директорії користувача.  
   d. Якщо умови a, b, c не допомогли знайти інпут файл — вивести помилку.
5. Всі інші вводи в консольний застосунок мають бути проігноровані, і виведена підказка щодо правильних команд та синтаксису.
6. Застосунок має бути розгорнутий на 3-х операційних системах **Linux**, **Mac**, **Windows** за допомогою віртуальних машин, побудованих за допомогою **Vagrant файлів**.
   - Для інсталяції слід використати можливості `vagrant provision`.
   - Весь програмний код для створення віртуальних машин та запуску процесу інсталяції слід додати в репозиторій.
7. Для приватного **Nuget репозитарію** використати **BaGet** (його завантажувати не потрібно).
   - `vagrant provision` має сконфігурувати доступ до приватного репозитарія і виконати необхідні команди для інсталяції пакета.
8. Ім'я пакета має бути **ваше ім'я** перша літера та **прізвище** латинською.
