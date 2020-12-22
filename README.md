# Sem3LAb4
Отчет по данным из бд. Данные подключение к бд через xml/json данные DataOptions (строка подключения ).  Само подключение в классе DataAccessLayerClass. Для получения данных из таблиц были разработатны две хранимые sql процедуры:
CREATE PROCEDURE GETALLADDRESS AS
BEGIN
SELECT TOP (1000) [BusinessEntityID]
      ,Address.AddressID
      ,AddressType.AddressTypeID
	  ,[AddressLine1]
      ,[AddressLine2]
      ,[City]
	  ,[Name]
  FROM [AdventureWorks2017].[Person].[BusinessEntityAddress]
  inner join Person.Address on
  Address.AddressID=BusinessEntityAddress.AddressID
  inner join Person.AddressType on
  AddressType.AddressTypeID = BusinessEntityAddress.AddressTypeID
  END;
Соединяет данные об адресе из 3 таблиц(Person].[BusinessEntityAddress, Person.Address, Person.AddressType) и
USE AdventureWorks2017;
GO
CREATE PROCEDURE GETPERSONANDEMAIL AS
BEGIN
SELECT TOP (1000) Person.BusinessEntityID
      ,[FirstName]
      ,[MiddleName]
      ,[LastName]
   ,[EmailAddress]
   ,[EmailAddressID]


      
  FROM [AdventureWorks2017].[Person].[Person]
  INNER JOIN Person.EmailAddress ON
  Person.BusinessEntityID = EmailAddress.BusinessEntityID
  END;

Берет данные о персоне из 2 таблиц(Person.EmailAddress, AdventureWorks2017].[Person].[Person])


Читаем SqlDataReader(ом)  методом GetAdd таблицу в сттруктуру  DataEntity состаящую из полей (public List<string> names;// название столбцов
        public List<object[]> values;//и их значений)



Далее для обработки вызывается метод CreateContent (DataEntity dataEntity)  из XmlGeneratorService для создания xml документа . Пока весь результат сохранен в виде строки , далее вызывается метод LogContent(), который создает док и сохраняет его в sourceDirectory. Также было организовано логирование (таблица logData(id, date ,message)).Создана хранимая процедура createLodData:

Работа проведена отличная:) Не один литр слез был выплкан.По мотивам работы снят остросюжнтный блокбастер )
