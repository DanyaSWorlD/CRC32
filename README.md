# CRC32
Простая и понятная реализация алгоритма CRC32 на языке C#. Главной целью было соответствие высчитываемого хеша хешу, который выдает программа sfv32w.
## Использование:
#### Подключение к своему проекту: 
```c# 
using Daquga.Security.Cryptography;
```
#### Получение хеша строки:
```c# 
var hash = CRC32.FromString("123456789");
```
#### Получение хеша из файла:
```c# 
var hash = CRC32.FromFile(FileInput);
```

>Для лучшего понимания рекомендуется открыть и изучить проект  [**Example**](https://github.com/DanyaSWorlD/CRC32/tree/master/Example) 
