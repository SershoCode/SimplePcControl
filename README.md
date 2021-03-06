# SimplePCControl
#### Клиент для управления ПК. Ответная часть для [SimpleHomeBroker](https://github.com/SershoCode/SimpleHomeBroker)
![GitHub last commit](https://img.shields.io/github/last-commit/sershocode/simplepccontrol)
![Lines of code](https://img.shields.io/tokei/lines/github/sershocode/simplepccontrol?color=brigtgeen&label=Lines%20of%20code)
![net core version](https://img.shields.io/badge/.net%20core-3.1-brightgreen)  

Как сказано выше, этот клиент является ответной частью для брокера. Включает/выключает мониторы, перезагружает, блокирует ПК и разлогинивает пользователя по команде на контроллер.  
Авторизация сделана простая, через middleware проверяется нужный токен в header'e запроса. В случае успеха запрос считается авторизованным.
