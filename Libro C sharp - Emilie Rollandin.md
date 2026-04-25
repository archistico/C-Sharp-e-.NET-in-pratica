# Titolo

**C# e .NET in pratica**
*Sviluppare applicazioni moderne, robuste e manutenibili con C#, .NET, ASP.NET Core, EF Core, Blazor e architetture modulari*

---

# Parte 01 - Orientarsi nell’ecosistema .NET

Questa parte serve a dare al lettore la mappa mentale. Prima di scrivere molto codice, deve capire che cosa sono C#, .NET, CLR, runtime, SDK, NuGet, solution, project, framework target e versioni.

## 01.01 - C#, .NET e il contesto moderno

### Argomenti

* Che cos’è C#
* Che cos’è .NET
* Differenza tra linguaggio, runtime, librerie e strumenti
* Dal .NET Framework a .NET moderno
* .NET Framework, .NET Core, .NET 5+, Mono, Xamarin, .NET MAUI
* Il concetto di **One .NET**
* Versioni LTS e STS
* Compatibilità, supporto e ciclo di vita
* Quando usare .NET Framework e quando usare .NET moderno
* Panoramica degli ambiti:

  * console
  * desktop
  * web
  * API
  * servizi
  * cloud
  * mobile
  * microservizi

### Perché qui

Questo capitolo evita confusione iniziale. Molti principianti confondono C#, .NET, Visual Studio, ASP.NET Core e .NET Framework come se fossero la stessa cosa.

---

## 01.02 - Ambiente di sviluppo e strumenti

### Argomenti

* Installazione di Visual Studio
* Installazione del .NET SDK
* Visual Studio Code e C# Dev Kit
* JetBrains Rider
* La CLI `dotnet`
* Creazione di una solution
* Creazione di progetti
* Struttura di un progetto `.csproj`
* Debug, build, run, publish
* Hot Reload
* Gestione di più progetti in una solution
* Introduzione a NuGet
* Central Package Management
* Git e controllo versione
* Uso ragionato di ChatGPT, GitHub Copilot e strumenti AI

### Perché qui

Prima di parlare di codice complesso, il lettore deve saper creare, eseguire e organizzare un progetto.

---

## 01.03 - Il primo programma C#

### Argomenti

* Hello World
* Top-level statements
* Metodo `Main`
* Differenza tra stile moderno e stile classico
* Input/output da console
* Argomenti da riga di comando
* Esecuzione di un singolo file `.cs`
* Direttive file-based, dove opportuno:

  * `#:package`
  * `#:sdk`
  * `#:property`
* Quando usare uno script e quando un progetto completo

### Perché qui

È il primo contatto pratico. Introduce il codice senza appesantire troppo.

---

# Parte 02 - Il linguaggio C#

Questa parte è il cuore didattico del libro. Deve essere molto solida, perché tutto il resto dipende da qui.

## 02.01 - Sintassi, variabili e tipi fondamentali

### Argomenti

* Struttura di un file C#
* Commenti
* Keyword
* Identificatori
* Variabili
* Costanti
* `var`
* Tipi numerici:

  * `int`
  * `long`
  * `decimal`
  * `double`
  * `float`
* `bool`
* `char`
* `string`
* Date e orari:

  * `DateTime`
  * `DateOnly`
  * `TimeOnly`
  * `TimeSpan`
* Nullable value types
* Inferenza del tipo
* Conversioni implicite ed esplicite

### Perché qui

È la grammatica base del linguaggio. Va trattata prima di operatori, flusso e funzioni.

---

## 02.02 - Operatori, espressioni e conversioni

### Argomenti

* Operatori aritmetici
* Operatori di confronto
* Operatori logici
* Operatori di assegnazione
* Operatore ternario
* Null-coalescing operator `??`
* Null-conditional operator `?.`
* Null-conditional assignment, se usato nella versione di C# trattata
* Casting
* `Convert`
* `Parse`
* `TryParse`
* Boxing e unboxing
* `is`
* `as`
* `nameof`
* `typeof`
* `sizeof`, dove utile
* `checked` e `unchecked`

### Perché qui

Qui il lettore impara a comporre istruzioni e a trasformare dati in modo sicuro.

---

## 02.03 - Controllo del flusso

### Argomenti

* `if`
* `else`
* `switch`
* `switch expression`
* `while`
* `do while`
* `for`
* `foreach`
* `break`
* `continue`
* `return`
* Pattern matching:

  * type pattern
  * property pattern
  * relational pattern
  * logical pattern
  * list pattern
* Guard clauses
* Codice difensivo

### Perché qui

È il capitolo in cui il codice diventa realmente decisionale.

---

## 02.04 - Funzioni, metodi e organizzazione del codice

### Argomenti

* Definizione di metodo
* Parametri
* Valori di ritorno
* Overload
* Parametri opzionali
* Named arguments
* `ref`
* `out`
* `in`
* Tuple
* Local functions
* Lambda expressions
* Expression-bodied members
* Principio DRY
* Differenza tra codice imperativo e funzionale
* Quando estrarre un metodo
* Come dare nomi chiari ai metodi

### Perché qui

Prima della OOP conviene insegnare bene il concetto di funzione/metodo.

---

## 02.05 - Gestione degli errori ed eccezioni

### Argomenti

* Che cos’è un’eccezione
* `try`
* `catch`
* `finally`
* `throw`
* Eccezioni built-in comuni
* Creare eccezioni personalizzate
* Differenza tra errore atteso e bug
* Validazione input
* Guard clauses
* Logging base
* Eccezioni e async/await
* Errori da non nascondere
* Quando non usare le eccezioni

### Perché qui

È un tema trasversale. Meglio introdurlo presto, prima di file, database, rete e API.

---

## 02.06 - Classi, oggetti e incapsulamento

### Argomenti

* Classi
* Oggetti
* Campi
* Proprietà
* Metodi
* Costruttori
* Costruttori primari
* Proprietà `required`
* Proprietà automatiche
* Campo `field`, se si vuole trattare C# moderno
* Modificatori di accesso:

  * `public`
  * `private`
  * `protected`
  * `internal`
  * `protected internal`
* Incapsulamento
* Invarianti
* Oggetti validi e oggetti inconsistenti
* Classi parziali
* Classi statiche

### Perché qui

È il primo blocco OOP vero. Va trattato con molti esempi pratici.

---

## 02.07 - Struct, record, enum e tipi immutabili

### Argomenti

* `struct`
* Differenza tra value type e reference type
* `enum`
* `record class`
* `record struct`
* Immutabilità
* `init`
* `with`
* Uguaglianza per valore
* Tuple vs record
* Quando usare una classe
* Quando usare una struct
* Quando usare un record

### Perché qui

Questo capitolo aiuta a modellare bene i dati, tema fondamentale per EF Core, API, DTO e domain model.

---

## 02.08 - Ereditarietà, interfacce e polimorfismo

### Argomenti

* Ereditarietà
* Classi base
* `virtual`
* `override`
* `abstract`
* `sealed`
* Polimorfismo
* Cast tra tipi
* Interfacce
* Implementazione esplicita di interfaccia
* Default interface methods
* Membri statici nelle interfacce, se rilevanti
* Composizione vs ereditarietà
* Quando preferire interfacce e composizione

### Perché qui

Da qui il lettore inizia a progettare codice estendibile.

---

## 02.09 - Generics e tipi riutilizzabili

### Argomenti

* Introduzione ai generics
* Classi generiche
* Metodi generici
* Interfacce generiche
* Vincoli:

  * `where T : class`
  * `where T : struct`
  * `where T : new()`
  * `where T : interface`
* Covarianza e controvarianza, almeno a livello introduttivo
* `Nullable<T>`
* Generics e collezioni
* `nameof` con tipi generici non associati, se si vuole includere C# moderno

### Perché qui

Generics è essenziale per capire collezioni, LINQ, DI, repository, result pattern e molte API .NET.

---

## 02.10 - Delegati, eventi e programmazione basata su messaggi

### Argomenti

* Delegati
* `Action`
* `Func`
* `Predicate`
* Eventi
* Event handler
* Publisher/subscriber
* Observer pattern
* Eventi nei componenti UI
* Eventi nelle architetture applicative
* Differenza tra evento, callback e messaggio

### Perché qui

È un ponte naturale verso UI, async, observer, SignalR, messaging e microservizi.

---

## 02.11 - Nullable Reference Types e qualità del codice

### Argomenti

* Il problema dei `null`
* Nullable Reference Types
* `string` vs `string?`
* Operatore `!`
* Annotazioni nullable
* API più sicure
* Contratti impliciti ed espliciti
* Null Object pattern
* Optional values
* Errori comuni

### Perché qui

È uno dei temi più importanti nel C# moderno. Va trattato come argomento autonomo, non come nota a margine.

---

## 02.12 - Funzionalità moderne di C#

### Argomenti

* Global using
* File-scoped namespace
* Raw string literals
* List patterns
* Required members
* Primary constructors
* Collection expressions
* Extension methods
* Extension members moderni, se previsti dalla versione trattata
* `Span<T>` e `ReadOnlySpan<T>` a livello introduttivo
* Miglioramenti alle stringhe
* Evoluzione del linguaggio da C# 1 a C# moderno

### Perché qui

Qui puoi raccogliere le novità senza frammentare troppo i capitoli precedenti.

---

# Parte 03 - Librerie fondamentali di .NET

Questa parte serve a passare dal linguaggio alla piattaforma.

## 03.01 - Runtime, CLR, assembly e compilazione

### Argomenti

* CLR
* Intermediate Language
* JIT
* AOT
* Garbage Collector
* Assembly
* Namespace
* Differenza tra namespace e assembly
* Target Framework Moniker:

  * `net8.0`
  * `net9.0`
  * `net10.0`
  * `net48`
* Dipendenze
* Reference
* Build
* Debug
* Release
* Pubblicazione

### Perché qui

È un capitolo tecnico ma fondamentale per chi vuole capire davvero .NET e non limitarsi a usarlo.

---

## 03.02 - Numeri, stringhe, date e formattazione

### Argomenti

* Numeri interi e decimali
* `decimal` per importi monetari
* `double` e precisione
* `BigInteger`
* Numeri complessi
* Stringhe
* Interpolazione
* Formattazione
* Culture e localizzazione
* `StringBuilder`
* Ordinamento e confronto stringhe
* Ordinamento numerico delle stringhe, se trattato nella versione .NET scelta
* Date e fusi orari
* `DateTimeOffset`

### Perché qui

Sono i tipi che si usano in tutte le applicazioni reali.

---

## 03.03 - Collezioni

### Argomenti

* Array
* `List<T>`
* `Dictionary<TKey,TValue>`
* `HashSet<T>`
* `Queue<T>`
* `Stack<T>`
* `SortedList`
* `SortedDictionary`
* Collezioni immutabili
* Collezioni frozen
* Scelta della collezione corretta
* Complessità base:

  * accesso
  * ricerca
  * inserimento
  * cancellazione

### Perché qui

Prima di LINQ conviene conoscere bene le strutture dati su cui LINQ lavora.

---

## 03.04 - File, directory e percorsi

### Argomenti

* `File`
* `Directory`
* `Path`
* Percorsi Windows/Linux/macOS
* Path assoluti e relativi
* File temporanei
* Permessi
* Encoding del testo
* Lettura e scrittura file
* Gestione errori su file system

### Perché qui

È uno degli scenari più concreti e frequenti.

---

## 03.05 - Stream, compressione e serializzazione

### Argomenti

* Concetto di stream
* `FileStream`
* `MemoryStream`
* `StreamReader`
* `StreamWriter`
* Operazioni async su stream
* Compressione ZIP
* Brotli
* JSON con `System.Text.Json`
* XML
* Serializzare oggetti
* Deserializzare oggetti
* DTO
* Versionamento dei dati serializzati

### Perché qui

Questo capitolo collega file, rete, API, dati e persistenza.

---

## 03.06 - Espressioni regolari

### Argomenti

* Sintassi Regex
* Match
* Group
* Replace
* Split
* Validazione input
* Regex compilate
* Regex source generated
* Errori comuni
* Quando non usare Regex

### Perché qui

Utile ma non centrale. Va dopo stringhe, file e serializzazione.

---

## 03.07 - Logging, configurazione e opzioni

### Argomenti

* Configurazione con `appsettings.json`
* Environment
* User secrets
* Options pattern
* Logging con `ILogger`
* Livelli di log
* Provider di logging
* Logging strutturato
* Correlation ID
* Best practice di configurazione

### Perché qui

È un capitolo da applicazioni reali. Prepara ASP.NET Core, worker service, microservizi e osservabilità.

---

## 03.08 - Dependency Injection

### Argomenti

* Che cos’è la Dependency Injection
* Inversion of Control
* Service container .NET
* Lifetime:

  * Singleton
  * Scoped
  * Transient
* Registrazione servizi
* Constructor injection
* Factory
* Anti-pattern:

  * Service Locator
  * dipendenze statiche
* DI in console app
* DI in ASP.NET Core
* DI e testabilità

### Perché qui

La Dependency Injection è uno snodo fondamentale per tutto il .NET moderno.

---

# Parte 04 - LINQ, dati e persistenza

Questa parte deve essere molto concreta. Il lettore deve imparare a interrogare, trasformare e salvare dati.

## 04.01 - LINQ: interrogare collezioni e sequenze

### Argomenti

* Che cos’è LINQ
* Query syntax
* Method syntax
* `Where`
* `Select`
* `OrderBy`
* `ThenBy`
* `GroupBy`
* `Join`
* `GroupJoin`
* `Any`
* `All`
* `Count`
* `Sum`
* `Average`
* `Min`
* `Max`
* `First`
* `Single`
* `FirstOrDefault`
* Deferred execution
* Immediate execution
* `IEnumerable<T>`
* `IQueryable<T>`

### Perché qui

LINQ è uno dei pilastri di C# moderno e di EF Core.

---

## 04.02 - LINQ avanzato e trasformazione dati

### Argomenti

* Proiezioni complesse
* Oggetti anonimi
* DTO
* Flattening con `SelectMany`
* Raggruppamenti
* Join esterni
* `LeftJoin` e `RightJoin`, se disponibili nella versione .NET trattata
* `CountBy`
* `AggregateBy`
* `Aggregate`
* Comparer personalizzati
* LINQ e performance
* Errori comuni con LINQ

### Perché qui

Serve a portare LINQ da uso base a uso professionale.

---

## 04.03 - Accesso ai database con ADO.NET

### Argomenti

* Perché conoscere ancora ADO.NET
* `DbConnection`
* `DbCommand`
* `DbDataReader`
* `DataTable`
* `DataRow`
* `DataColumn`
* `DataSet`
* `DataView`
* `TableAdapter` ...
* Parametri SQL
* SQL injection
* Transazioni
* Stored procedure
* Connessioni a SQL Server
* Connessioni a SQLite
* Quando preferire ADO.NET a EF Core

### Perché qui

Anche se EF Core è centrale, un libro solido su .NET dovrebbe spiegare almeno le basi di ADO.NET, soprattutto per applicazioni aziendali e legacy.

---

## 04.04 - Entity Framework Core: introduzione

### Argomenti

* Che cos’è un ORM
* DbContext
* DbSet
* Entity
* Provider
* SQL Server
* SQLite
* Configurazione connessione
* Migrations
* Creazione database
* Query base
* Insert
* Update
* Delete
* Change tracking

### Perché qui

È il cuore della persistenza moderna in .NET.

---

## 04.05 - Modellazione con EF Core

### Argomenti

* Data annotations
* Fluent API
* Relazioni:

  * uno-a-uno
  * uno-a-molti
  * molti-a-molti
* Chiavi primarie
* Chiavi esterne
* Indici
* Owned types
* Value objects
* Shadow properties
* Concurrency token
* Soft delete
* Global query filters

### Perché qui

Dopo aver usato EF Core, bisogna imparare a modellare bene.

---

## 04.06 - EF Core avanzato e performance

### Argomenti

* Lazy loading
* Eager loading
* Explicit loading
* `Include`
* `AsNoTracking`
* Query compilate
* Modelli compilati
* Batch update
* `ExecuteUpdate`
* `ExecuteDelete`
* Transazioni
* Concorrenza ottimistica
* Logging SQL
* N+1 problem
* Repository pattern: pro e contro
* Unit of Work: pro e contro

### Perché qui

Questo capitolo evita che il lettore usi EF Core in modo ingenuo e inefficiente.

---

# Parte 05 - Qualità, test e manutenzione

Questa parte è importante perché distingue un libro “di sintassi” da un libro per sviluppare davvero.

## 05.01 - Debugging con Visual Studio

### Argomenti

* Breakpoint
* Conditional breakpoint
* Tracepoint
* Watch
* Immediate Window
* Call stack
* Exception settings
* Debug di codice async
* Debug di LINQ
* Hot Reload
* Diagnostica performance base

### Perché qui

Prima dei test avanzati, il lettore deve saper diagnosticare.

---

## 05.02 - Unit test con xUnit

### Argomenti

* Perché testare
* Test unitari
* xUnit
* Arrange, Act, Assert
* Fact
* Theory
* InlineData
* Test dei casi limite
* Test delle eccezioni
* Naming dei test
* Code coverage
* Test e refactoring

### Perché qui

È un capitolo essenziale per costruire software manutenibile.

---

## 05.03 - Test di integrazione

### Argomenti

* Differenza tra unit test e integration test
* Test con database
* SQLite in-memory
* Testcontainers, se vuoi includere Docker
* Test di API ASP.NET Core
* `WebApplicationFactory`
* TestServer
* Seed dati
* Isolamento dei test
* Pulizia ambiente

### Perché qui

Serve per passare dai test “didattici” ai test utili in progetti reali.

---

## 05.04 - Refactoring e pulizia del codice

### Argomenti

* Code smell
* Metodi troppo lunghi
* Classi troppo grandi
* Duplicazione
* Nomi poco chiari
* Separazione delle responsabilità
* Refactoring sicuro
* Introduzione ai principi SOLID
* Tecniche:

  * Extract Method
  * Extract Class
  * Introduce Parameter Object
  * Replace Conditional with Polymorphism
* Quando non rifattorizzare

### Perché qui

Questo capitolo prepara naturalmente la parte su architettura e design pattern.

---

## 05.05 - SOLID in C#

### Argomenti

* Single Responsibility Principle
* Open/Closed Principle
* Liskov Substitution Principle
* Interface Segregation Principle
* Dependency Inversion Principle
* Esempi sbagliati e corretti
* SOLID e Dependency Injection
* SOLID e testabilità
* Limiti dei principi SOLID
* La sovra-ingegnerizzazione (over-engineering)

### Perché qui

SOLID va messo dopo OOP, DI, test e refactoring, non troppo presto.

---

# Parte 06 - ASP.NET Core, API e sviluppo web

Questa parte dovrebbe essere pratica e progressiva: prima HTTP, poi ASP.NET Core, poi API, poi Blazor.

## 06.01 - Fondamenti del web moderno

### Argomenti

* HTTP
* Request
* Response
* Header
* Status code
* Query string
* Form
* JSON
* Cookie
* Sessione
* HTML essenziale
* CSS essenziale
* JavaScript essenziale
* Client/server
* Server-side rendering
* Single-page application

### Perché qui

Prima di ASP.NET Core, il lettore deve capire il web.

---

## 06.02 - Introduzione ad ASP.NET Core

### Argomenti

* Creazione progetto web
* `Program.cs`
* Middleware pipeline
* Routing
* Endpoint
* Static files
* `MapStaticAssets`, se rilevante
* Configurazione
* Logging
* Dependency Injection
* Ambienti:

  * Development
  * Staging
  * Production
* Kestrel
* Hosting

### Perché qui

È la base comune per MVC, Razor, Minimal API e Blazor.

---

## 06.03 - Minimal API

### Argomenti

* Creazione API minimale
* Routing
* Binding parametri
* Validazione
* Data annotations
* Risposte HTTP
* `Results`
* Problem Details
* Endpoint group
* Filtri
* Versionamento API
* OpenAPI
* Scalar/Swagger UI
* Test delle API
* Consumo API con `HttpClient`

### Perché qui

Minimal API è oggi uno degli approcci più semplici per insegnare backend .NET.

---

## 06.04 - API robuste con ASP.NET Core

### Argomenti

* DTO
* Validazione input
* Mapping
* Error handling centralizzato
* Logging
* Autenticazione
* Autorizzazione
* JWT
* Policy
* Rate limiting
* CORS
* Caching
* Idempotenza
* Pagination
* Filtering
* Sorting

### Perché qui

Dopo aver creato API base, bisogna renderle professionali.

---

## 06.05 - Razor, MVC e rendering server-side

### Argomenti

* Razor syntax
* Razor Pages
* MVC
* Controller
* Action
* View
* Model binding
* Validation
* Tag Helpers
* Layout
* Partial view
* Form
* Upload file
* Quando usare Razor/MVC rispetto a Blazor

### Perché qui

Anche se Blazor è centrale, MVC e Razor restano importanti nel mondo reale.

---

## 06.06 - Blazor: componenti interattivi

### Argomenti

* Componenti
* Parametri
* EventCallback
* Data binding
* Lifecycle
* Rendering
* Blazor Server
* Blazor WebAssembly
* Blazor Web App
* Static SSR
* Interactive Server
* Interactive WebAssembly
* Auto render mode
* Form con `EditForm`
* Validazione
* Componenti riutilizzabili

### Perché qui

Blazor va trattato dopo le basi web e ASP.NET Core.

---

## 06.07 - Blazor avanzato

### Argomenti

* Stato applicativo
* Persistenza stato tra prerendering e interattività
* QuickGrid
* Reconnect UI
* JavaScript interop
* Autenticazione in Blazor
* Chiamate API
* Streaming rendering
* Server-Sent Events
* Performance
* Error boundary
* Component library

### Perché qui

Serve a portare Blazor oltre l’esempio base.

---

## 06.08 - SignalR e comunicazione real-time

### Argomenti

* Concetto di real-time
* Hub
* Client
* Gruppi
* Connessioni
* Notifiche
* Chat
* Dashboard live
* SignalR in Blazor
* SignalR con JavaScript
* Scalabilità
* Redis backplane
* Sicurezza

### Perché qui

SignalR collega web, async, eventi e architetture distribuite.

---

# Parte 07 - Programmazione asincrona, concorrenza e performance

Questa parte è molto importante, ma non va messa troppo presto. Prima il lettore deve conoscere C#, LINQ, web e dati.

## 07.01 - Async/await

### Argomenti

* Perché esiste async/await
* I/O-bound vs CPU-bound
* `Task`
* `Task<T>`
* `async`
* `await`
* Eccezioni in async
* Cancellazione con `CancellationToken`
* Timeout
* `ConfigureAwait`
* `ValueTask`
* Fire-and-forget
* Errori comuni
* Deadlock

### Perché qui

È il cuore della programmazione moderna .NET.

---

## 07.02 - Thread, ThreadPool e Task

### Argomenti

* Thread
* ThreadPool
* Worker thread
* I/O completion thread
* `Task.Run`
* Task scheduler
* Continuazioni
* `ContinueWith`
* Task padre/figlio
* Long-running task
* `BackgroundService`
* Hosted services

### Perché qui

Dopo async/await, il lettore deve capire cosa succede sotto.

---

## 07.03 - Sincronizzazione e dati condivisi

### Argomenti

* Race condition
* Thread safety
* `lock`
* `Monitor`
* Nuovo tipo `Lock`, se trattato
* `Interlocked`
* `volatile`
* `SemaphoreSlim`
* `Mutex`
* `ReaderWriterLockSlim`
* Deadlock
* Starvation
* Strategie per ridurre la condivisione

### Perché qui

È fondamentale per evitare bug difficili.

---

## 07.04 - Collezioni concorrenti e producer/consumer

### Argomenti

* `ConcurrentDictionary`
* `ConcurrentQueue`
* `ConcurrentStack`
* `ConcurrentBag`
* `BlockingCollection`
* `Channel<T>`
* Producer/Consumer
* Backpressure
* Pipeline asincrone
* Code interne a un’applicazione

### Perché qui

È il capitolo pratico sulla concorrenza applicativa.

---

## 07.05 - Parallelismo e PLINQ

### Argomenti

* `Parallel.For`
* `Parallel.ForEach`
* Controllo del grado di parallelismo
* Break/Stop
* Eccezioni aggregate
* PLINQ
* `AsParallel`
* `AsOrdered`
* `WithDegreeOfParallelism`
* Quando il parallelismo peggiora le prestazioni
* CPU-bound workload

### Perché qui

Da usare per elaborazioni intensive, non per sostituire async/await.

---

## 07.06 - Performance, memoria e diagnostica

### Argomenti

* Misurare prima di ottimizzare
* BenchmarkDotNet
* Profilazione
* Allocazioni
* Garbage Collector
* Stack vs heap
* `Span<T>`
* `Memory<T>`
* Pooling
* SIMD
* Native AOT
* JIT
* Escape analysis
* ThreadPool diagnostics
* Visual Studio Profiler
* Concurrency Visualizer
* Parallel Stacks
* Parallel Watch

### Perché qui

È il capitolo che crea una vera mentalità da sviluppatore professionale.

---

# Parte 08 - Design pattern e architettura applicativa

Questa parte deve essere meno “catalogo di pattern” e più “come progettare un’applicazione reale”.

## 08.01 - Dalla classe all’architettura

### Argomenti

* Perché serve architettura
* Complessità accidentale e complessità essenziale
* Modularità
* Separation of Concerns
* Coesione
* Accoppiamento
* Layer
* Tier
* Monolite
* Monolite modulare
* Architettura a tre livelli
* Architettura a quattro layer DDD

### Perché qui

È il ponte tra codice e sistema.

---

## 08.02 - Layered Architecture, DDD e Application Layer

### Argomenti

* Presentation Layer
* Application Layer
* Domain Layer
* Infrastructure Layer
* Entity
* Value Object
* Aggregate
* Repository
* Domain Service
* Application Service
* Use case
* DTO
* Mapping
* Linguaggio ubiquo
* Bounded context

### Perché qui

Introduce il vocabolario architetturale più utile per applicazioni aziendali.

---

## 08.03 - Clean Architecture, Hexagonal Architecture e Ports & Adapters

### Argomenti

* Clean Architecture
* Dipendenze verso l’interno
* Use case
* Interfacce
* Adapter
* Port
* Infrastructure esterna
* Testabilità
* Indipendenza dal database
* Indipendenza dalla UI
* Pro e contro

### Perché qui

È importante mostrare alternative moderne alla classica architettura a layer.

---

## 08.04 - Vertical Slice Architecture e CQRS

### Argomenti

* Organizzazione per feature
* Command
* Query
* Handler
* MediatR
* CQRS
* Separazione lettura/scrittura
* Validazione per use case
* Pipeline behavior
* Quando usare VSA
* Quando non usarla

### Perché qui

È molto utile per applicazioni ASP.NET Core moderne.

---

## 08.05 - Design pattern creazionali

### Argomenti

* Factory Method
* Abstract Factory
* Builder
* Prototype
* Singleton
* Singleton e DI
* Quando un Singleton è pericoloso
* Esempi pratici in .NET

### Perché qui

I pattern vanno trattati dopo SOLID, DI e architettura, non prima.

---

## 08.06 - Design pattern strutturali

### Argomenti

* Adapter
* Facade
* Decorator
* Proxy
* Composite
* Bridge
* Esempi:

  * Adapter per sistemi legacy
  * Decorator con DI
  * Proxy per chiamate remote
  * Composite per menu/alberi UI

### Perché qui

Questi pattern sono utili per integrare sistemi e organizzare dipendenze.

---

## 08.07 - Design pattern comportamentali

### Argomenti

* Strategy
* Command
* Observer
* Mediator
* Template Method
* Chain of Responsibility
* State
* Esempi:

  * Strategy per algoritmi intercambiabili
  * Command con MediatR
  * Observer con eventi
  * Chain of Responsibility nelle pipeline

### Perché qui

Sono pattern centrali nelle applicazioni moderne, soprattutto web e business.

---

## 08.08 - Pattern applicativi moderni

### Argomenti

* Result pattern
* Options pattern
* Repository pattern
* Unit of Work
* Specification pattern
* Outbox pattern
* Saga
* Circuit Breaker
* Retry
* Bulkhead
* Idempotenza
* Event sourcing, introduzione
* Domain events
* Integration events

### Perché qui

È il capitolo che collega design pattern classici e architetture distribuite moderne.

---

# Parte 09 - Sicurezza, identità e applicazioni production-ready

Questa parte è indispensabile se vuoi un libro orientato alle applicazioni reali.

## 09.01 - Sicurezza applicativa di base

### Argomenti

* Principi base di sicurezza
* Validazione input
* Output encoding
* SQL injection
* XSS
* CSRF
* Gestione segreti
* HTTPS
* CORS
* Logging sicuro
* Password e hashing
* Least privilege

### Perché qui

La sicurezza non deve essere trattata solo nei microservizi.

---

## 09.02 - Autenticazione e autorizzazione in ASP.NET Core

### Argomenti

* Authentication
* Authorization
* Cookie authentication
* JWT bearer
* Claims
* Roles
* Policies
* RBAC
* PBAC
* OpenID Connect
* OAuth2
* Identity provider
* Duende IdentityServer, eventualmente
* Entra ID, eventualmente
* Service-to-service authentication

### Perché qui

Serve sia per API sia per applicazioni web e architetture distribuite.

---

## 09.03 - Configurazione, segreti e ambienti

### Argomenti

* Development/Staging/Production
* Variabili d’ambiente
* User Secrets
* Secret manager
* Azure Key Vault, se vuoi includere cloud
* Configurazione per container
* Feature flags
* Configurazione per test

### Perché qui

È una parte molto pratica e spesso trascurata.

---

# Parte 10 - Distribuzione, container e DevOps

Questa parte porta il progetto fuori dal computer dello sviluppatore.

## 10.01 - Build, publish e distribuzione

### Argomenti

* Build Debug/Release
* `dotnet publish`
* Framework-dependent deployment
* Self-contained deployment
* Single-file app
* Trimming
* Native AOT
* Distribuzione Windows
* Distribuzione Linux
* Servizi Windows
* Systemd
* IIS
* Reverse proxy
* Nginx

### Perché qui

È il minimo indispensabile per consegnare applicazioni .NET.

---

## 10.02 - NuGet e packaging

### Argomenti

* Creare una libreria
* Creare un pacchetto NuGet
* Versionamento semantico
* Pubblicazione pacchetti
* Feed privati
* Source generators
* Analyzer
* Distribuzione di tipi riutilizzabili

### Perché qui

Collega modularità, riuso e distribuzione.

---

## 10.03 - Docker per applicazioni .NET

### Argomenti

* Concetto di container
* Dockerfile
* Multi-stage build
* Immagini base .NET
* Container per API
* Container per worker
* Variabili d’ambiente
* Volumi
* Docker Compose
* Database in container
* Debug in container
* Sicurezza immagini

### Perché qui

È prerequisito naturale per microservizi, Kubernetes e cloud.

---

## 10.04 - CI/CD

### Argomenti

* Pipeline
* Build automatica
* Test automatici
* Pubblicazione artifact
* GitHub Actions
* Azure DevOps
* Deploy automatico
* Secret nelle pipeline
* Scansione vulnerabilità
* Versionamento release
* Rollback

### Perché qui

Trasforma il libro da “sviluppo locale” a “sviluppo professionale”.

---

## 10.05 - Osservabilità

### Argomenti

* Logging centralizzato
* Metriche
* Tracing distribuito
* OpenTelemetry
* Health checks
* Prometheus
* Grafana
* Application Insights
* Jaeger
* Correlation ID
* Diagnosi di problemi in produzione

### Perché qui

Osservabilità è fondamentale prima di parlare seriamente di microservizi.

---

# Parte 11 - Architetture distribuite e microservizi

Questa parte deve essere avanzata. Non va proposta come “il modo giusto”, ma come una scelta architetturale con costi e benefici.

## 11.01 - Monolite, monolite modulare e microservizi

### Argomenti

* Monolite
* Monolite modulare
* Microservizi
* Pro e contro
* Quando evitare i microservizi
* Complessità operativa
* Team e organizzazione
* Database condiviso: rischio
* Distributed monolith
* Strangler Fig pattern
* Matrice rischio/valore

### Perché qui

È importante evitare il messaggio sbagliato: “microservizi = modernità”. Spesso un monolite modulare è la scelta migliore.

---

## 11.02 - Progettare i confini dei servizi

### Argomenti

* Bounded context
* Autonomia del servizio
* Database-per-service
* Coerenza eventuale
* CAP theorem
* Contratti API
* Versionamento
* Granularità
* Comunicazione sincrona
* Comunicazione asincrona
* REST
* gRPC
* Messaging

### Perché qui

È il problema più importante nei microservizi: decidere dove tagliare.

---

## 11.03 - Messaging, eventi e coerenza distribuita

### Argomenti

* Eventi di dominio
* Eventi di integrazione
* RabbitMQ
* MassTransit
* Outbox pattern
* Inbox pattern
* Saga
* Coreografia
* Orchestrazione
* Idempotenza
* Retry
* Dead-letter queue
* Consistenza eventuale

### Perché qui

È il nucleo tecnico delle architetture distribuite robuste.

---

## 11.04 - API Gateway, service discovery e resilienza

### Argomenti

* API Gateway
* BFF
* Service discovery
* Load balancing
* Circuit breaker
* Retry
* Timeout
* Bulkhead
* Rate limiting
* Health check
* Service mesh, introduzione
* YARP, eventualmente

### Perché qui

Qui il lettore capisce l’infrastruttura logica intorno ai servizi.

---

## 11.05 - Sicurezza nei microservizi

### Argomenti

* Identity provider
* JWT
* OAuth2
* OpenID Connect
* Token delegation
* Client credentials flow
* Service-to-service authentication
* mTLS, introduzione
* Authorization centralizzata/decentrata
* Secrets
* Zero Trust

### Perché qui

La sicurezza distribuita è più complessa della sicurezza in una singola app.

---

## 11.06 - Kubernetes e orchestrazione

### Argomenti

* Perché Kubernetes
* Pod
* Deployment
* Service
* Ingress
* ConfigMap
* Secret
* Helm
* Scaling orizzontale
* HPA
* Limiti risorse
* Rolling update
* Blue-Green
* Canary
* Monitoraggio su Kubernetes

### Perché qui

Kubernetes va trattato come strumento di deployment avanzato, non come prerequisito per imparare .NET.

---

## 11.07 - .NET Aspire e applicazioni distribuite

### Argomenti

* Che cos’è .NET Aspire
* Orchestrazione locale
* Service discovery
* Configurazione
* Telemetria
* Dashboard
* Integrazione con container
* Redis
* Database
* API
* Worker
* Quando usare Aspire

### Perché qui

Aspire è utile come ponte tra sviluppo locale e architetture distribuite.

---

# Parte 12 - Applicazioni client e desktop

Accenno per un libro completo sull’ecosistema .NET.

## 12.01 - Applicazioni desktop in .NET

### Argomenti

* WinForms
* WPF
* WinUI
* .NET MAUI
* Avalonia
* Differenze
* Quando usare ciascuna tecnologia
* Desktop moderno vs web
* MVVM
* Binding
* Comandi
* Validazione
* Distribuzione desktop

### Perché qui

Serve per non ridurre .NET solo al web.

---

## 12.02 - WPF e MVVM in sintesi

### Argomenti

* XAML
* Window
* UserControl
* Binding
* DataContext
* INotifyPropertyChanged
* ICommand
* ObservableCollection
* ResourceDictionary
* Styling
* Navigazione tra view
* Integrazione con servizi e DI

### Perché qui

Puoi collegarlo al tuo lavoro sul libro WPF, senza duplicare tutto.

---

## 12.03 - .NET MAUI e sviluppo cross-platform

### Argomenti

* Che cos’è .NET MAUI
* XAML in MAUI
* Controlli base
* CollectionView
* Shell
* Navigazione
* Platform-specific
* HybridWebView
* Animazioni async
* Integrazione con API
* Limiti pratici

### Perché qui

È utile come panoramica moderna, senza trasformare il libro in un manuale MAUI completo.

---

## 12.04 - Avalonia e lo sviluppo cross-platform

### Argomenti

* Che cos’è Avalonia
* Differenza tra Avalonia, WPF e .NET MAUI
* Piattaforme supportate
* Rendering cross-platform
* XAML e file `.axaml`
* Window e UserControl
* Controlli base
* Layout
* Binding
* MVVM
* `ObservableCollection`
* Comandi
* Styling
* Resource dictionary
* DataTemplate
* Property system
* Navigazione tra view
* Integrazione con servizi e Dependency Injection
* Platform-specific
* Deployment
* Migrazione da WPF
* Limiti pratici
* Quando usare Avalonia

### Perché qui

Avalonia completa la panoramica sulle applicazioni client .NET perché copre uno spazio diverso da WPF e .NET MAUI: lo sviluppo desktop cross-platform, in particolare Windows, macOS e Linux, con un modello vicino a XAML/MVVM.

---

# Parte 13 - Caso pratico guidato

Questa secondo me è indispensabile. Un libro moderno deve chiudere con un progetto reale, non solo con capitoli separati.

## 13.01 - Progetto finale: gestionale modulare in .NET

### Idea applicativa

**Mini gestionale editoriale con API, database, dashboard e report**

### Tecnologie

* C#
* .NET moderno
* ASP.NET Core
* Minimal API
* EF Core
* SQLite 
* Blazor
* xUnit
* Dependency Injection
* Logging
* Docker
* GitHub Actions
* Architettura modulare

---

## 13.02 - Analisi del dominio

### Argomenti

* Requisiti
* Casi d’uso
* Entità
* Value object
* DTO
* Regole di business
* Validazioni
* Errori
* Confini dei moduli

---

## 13.03 - Progettazione della solution

### Argomenti

* Struttura dei progetti:

  * `App.Domain`
  * `App.Application`
  * `App.Infrastructure`
  * `App.Api`
  * `App.Web`
  * `App.Tests`
* Dipendenze tra progetti
* Clean Architecture leggera
* DI
* Configurazione
* Logging

---

## 13.04 - Implementazione dominio e use case

### Argomenti

* Entity
* Value object
* Repository interface
* Command
* Query
* Handler
* Validazione
* Result pattern
* Test unitari

---

## 13.05 - Persistenza con EF Core

### Argomenti

* DbContext
* Mapping Fluent API
* Migrations
* Repository concreto
* Transazioni
* Seed dati
* Test di integrazione

---

## 13.06 - API REST

### Argomenti

* Endpoint CRUD
* DTO
* Validazione
* Error handling
* OpenAPI
* Autenticazione
* Test API

---

## 13.07 - Interfaccia Blazor

### Argomenti

* Pagine
* Componenti
* Form
* Validazione
* Tabelle
* Chiamate API
* Gestione stato
* Errori UI
* Autenticazione lato client

---

## 13.08 - Background service e operazioni asincrone

### Argomenti

* Worker service
* Code interne con `Channel<T>`
* Elaborazioni schedulate
* Logging
* Cancellazione
* Retry
* Errori

---

## 13.09 - Docker, CI/CD e rilascio

### Argomenti

* Dockerfile
* Docker Compose
* Pipeline GitHub Actions
* Test automatici
* Publish
* Deploy
* Configurazione produzione
* Health checks

---

## 13.10 - Evoluzione verso microservizi

### Argomenti

* Quando separare un modulo
* Strangler Fig
* Estrazione di un servizio
* Eventi di integrazione
* Outbox
* RabbitMQ
* Aspire
* Osservabilità distribuita
* Costi e benefici

### Perché qui

Questo è il modo migliore per parlare di microservizi: non come punto di partenza, ma come evoluzione possibile.

---

# Parte 14 - Appendici operative

Le appendici alleggeriscono il corpo principale del libro e raccolgono materiale utile.

## 14.01 - Comandi utili della CLI .NET

* `dotnet new`
* `dotnet build`
* `dotnet run`
* `dotnet test`
* `dotnet publish`
* `dotnet add package`
* `dotnet ef`
* `dotnet workload`
* alias CLI

---

## 14.02 - Tabelle di confronto

* `class` vs `struct` vs `record`
* `IEnumerable` vs `IQueryable`
* `Task` vs `Thread`
* `async/await` vs parallelismo
* Minimal API vs MVC vs Razor Pages vs Blazor
* Monolite vs monolite modulare vs microservizi
* SQL diretto vs EF Core
* WPF vs WinUI vs MAUI

---

## 14.03 - Checklist di qualità

* Checklist codice C#
* Checklist API
* Checklist EF Core
* Checklist sicurezza
* Checklist performance
* Checklist deployment
* Checklist logging/osservabilità

---

## 14.04 - Roadmap di studio

* Percorso principiante
* Percorso sviluppatore backend
* Percorso sviluppatore web
* Percorso sviluppatore desktop
* Percorso architettura
* Percorso performance
* Percorso microservizi



