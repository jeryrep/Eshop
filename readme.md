# Laboratorium 5: Wprowadzenie do Domain Driven Design (DDD) oraz Command Query Responsibility Segregation (CQRS)

## Cel zadania
Celem tego laboratorium jest praktyczne zapoznanie się z kluczowymi aspektami Domain Driven Design (DDD) oraz podstawami Command Query Responsibility Segregation (CQRS). Przed rozpoczęciem, zapoznaj się z dostępnym kodem. W razie pytań, skontaktuj się przez platformę Teams.

### Wskazówki:
- Pamiętaj o odpowiednim oddzieleniu warstwy domenowej, aplikacji oraz infrastruktury (w ramach uproszczenia w tym projekcie modele domeny oraz infrastruktury są takie same).
- Staraj się trzymać zasad SOLID w trakcie projektowania klas.
- Projekt uruchamia się pod adresem http://localhost:8080
- Do połączenia z bazą danych możesz użyć narzędzi takich jak Studio3T. Wymagany jest następujący ciąg połączeniowy: `mongodb://root:example@eshop.mongodb:27017`.
- Podczas instalacji Visual Studio upewnij się, że zainstalowałeś pakiet ASP.NET and web development.
- Zachecam do stworzenia klona tego repozytorum, w przypadku potrzeby dodania poprawek z mojej strony będzie Państwu łatwiej się z nimi integrować. 
- Przykładowe zapytania
    - POST /api/v1/customers/{customerId}/orders
        - customerId: 3fa85f64-5717-4562-b3fc-2c963f66afa6
        - products id bazuje na `ProductPriceDataApi`
        ```json
        {
          "products": [
            {
              "id": "514f6265-a9b8-46da-a31d-50f4f4c20911", 
              "quantity": 1
            }
          ]
        }
        ```
    - POST /api/v1/customers/{customerId}/orders
        - customerId: 3fa85f64-5717-4562-b3fc-2c963f66afa6
        - products id bazuje na `ProductPriceDataApi`
        ```json
        {
          "products": [
            {
              "id": "514f6265-a9b8-46da-a31d-50f4f4c20911", 
              "quantity": 1
            },
            {
              "id": "514f6265-a9b8-46da-a31d-50f4f4c20912", 
              "quantity": 2
            }
          ]
        }
        ```
    - GET /api/v1/orders/{orderId}
        - orderd: Wartość zwrócona przez zapytanie POST
        
- Z powodów technicznych (serializacja) wszystkie właściwości modeli domnenowych muszą mieć settery i gettery np. `public Guid Id { get; private set; }`

### Ocena 3.5: Tworzenie nowych użytkowników
Twoim zadaniem jest zaimplementowanie funkcjonalności tworzenia nowych użytkowników przy użyciu agregatu `Customer`.
1. Utwórz klasy `CreateCustomerCommand` i `GetCustomerQuery`.
2. Zaimplementuj obsługę tych komend i zapytań.
3. Dodaj odpowiednie endpointy do API.
4. Upewnij się, że po stworzeniu klienta generowane jest zdarzenie `CustomerCreatedEvent`.

Pamiętaj aby stworzyć nową kolekcję dla tego użytkoników w bazie danych. 

Zwróć szczegególną uwagę na aby do warstwy API nie dostały się modele domenowe!

### Ocena 3.5: Implementacja reguł biznesowych
Stwórz następujące reguły biznesowe (np. `OrderMustHaveAtLeastOneProductRule`):
1. Nazwa użytkownika nie może być pusta i powinna składać się tylko z liter.
2. Łączny koszt produktów w zamówieniu nie może przekroczyć 15000.

### Ocena 4.5: Obsługa koszyka produktów
Twoim zadaniem jest zaimplementowanie obsługi koszyka produktów.
1. Stwórz nowy agregat do obsługi koszyka z produktami.
2. Dodaj odpowiednie komendy (commands) i zapytania (queries) do zarządzania koszykiem.
3. Dodaj odpowiednie endpointy do API.
4. Przerób implementację `Order`, aby była twrzona w za pomocą następującej metody `public static Order Create(CheckoutCart checkoutCart)`


**Uwaga:** Utwórz nową kolekcję dla tej funkcjonalności w bazie danych.

### Ocena 5.0: Integracja z innym projektem
Za pomocą Refit (https://github.com/reactiveui/refit) nawiąż połączenie z projektem z laboratorium 2, aby pobrać informacje o produktach. Podczas integracji zwróć uwagę na wykorzystanie kontenerów.

Przykładowy interfejs do wykorzystania:

```csharp
public interface IProductsApi
{
    [Get("/products")]
    Task<List<Product>> GetAllProductsAsync();
}
```

Wykorzystaj zaimplementowany interfejs `IProductsApi` w klasie `ProductPriceDataApi`.