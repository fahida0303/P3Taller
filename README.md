# Sistema de Gestión de Reservas de Salas

## Descripción del Proyecto

Este proyecto implementa un sistema de consola para gestionar reservas de salas universitarias, desarrollado siguiendo los principios y patrones GRASP (General Responsibility Assignment Software Patterns). El sistema permite registrar y listar reservas, almacenando la información en un archivo de texto plano.

## Arquitectura del Sistema

El proyecto está estructurado en 4 capas principales, cada una con responsabilidades específicas:

### 1. **ENTITY (Capa de Entidades)**
- **Archivo**: `ENTITY/Reserva.cs`
- **Responsabilidad**: Contiene únicamente la definición de datos de la entidad Reserva
- **Propiedades**: Id, Solicitante, Sala, Fecha
- **Métodos**: Constructor por defecto, constructor parametrizado, ToString()

### 2. **DAL (Data Access Layer - Capa de Acceso a Datos)**
- **Archivos**: 
  - `DAL/IReservaRepository.cs` - Interfaz del repositorio
  - `DAL/ReservaRepositoryTxt.cs` - Implementación para archivos de texto
- **Responsabilidad**: Manejo de la persistencia de datos
- **Funcionalidades**:
  - Guardar reservas en archivo `reservas.txt`
  - Leer todas las reservas del archivo
  - Obtener el último ID para generar nuevos IDs incrementales

### 3. **BLL (Business Logic Layer - Capa de Lógica de Negocio)**
- **Archivo**: `BLL/ReservaService.cs`
- **Responsabilidad**: Contiene toda la lógica de negocio y validaciones
- **Funcionalidades**:
  - Validación de datos de entrada (solicitante y sala no vacíos)
  - Asignación automática de ID incremental
  - Asignación de fecha actual
  - Coordinación con la capa de datos

### 4. **GUI (Graphical User Interface - Capa de Presentación)**
- **Archivo**: `taller/Program.cs`
- **Responsabilidad**: Interfaz de usuario por consola
- **Funcionalidades**:
  - Menú interactivo
  - Captura de datos del usuario
  - Mostrar resultados y mensajes

## Historias de Usuario Implementadas

### HU-01: Registrar Reserva
**Como** coordinador académico  
**Quiero** registrar una reserva indicando Solicitante y Sala  
**Para** dejar constancia del uso del espacio

**Criterios de Aceptación:**
- ✅ Validación de campos vacíos con mensajes de error
- ✅ Asignación automática de ID incremental
- ✅ Guardado en formato `Id|Solicitante|Sala|Fecha` en `reservas.txt`

### HU-02: Listar Reservas
**Como** coordinador académico  
**Quiero** ver todas las reservas registradas  
**Para** revisar el historial de uso de las salas

**Criterios de Aceptación:**
- ✅ Mostrar cada reserva en formato: `Id | Solicitante | Sala | Fecha`
- ✅ Mensaje "(No hay reservas aún)" cuando no existen datos

## Justificaciones GRASP

### Justificación 1 (Alta Cohesión - High Cohesion)

**Aplicación del Patrón:**
Cada clase en el sistema tiene una responsabilidad única y bien definida:

- **Clase Reserva (ENTITY)**: Se encarga únicamente de representar los datos de una reserva. No contiene lógica de negocio ni de persistencia, solo propiedades y métodos básicos de representación.

- **ReservaRepositoryTxt (DAL)**: Su única responsabilidad es el manejo de la persistencia en archivos de texto. No contiene validaciones de negocio ni lógica de presentación.

- **ReservaService (BLL)**: Se enfoca exclusivamente en la lógica de negocio: validaciones, asignación de IDs, coordinación entre capas. No maneja persistencia directamente ni interfaz de usuario.

- **Program (GUI)**: Su responsabilidad se limita a la interacción con el usuario: mostrar menús, capturar entrada, mostrar resultados. No contiene lógica de negocio.

**Beneficios Obtenidos:**
- **Mantenibilidad**: Cada clase es fácil de entender y modificar independientemente
- **Reutilización**: Las clases pueden ser reutilizadas en diferentes contextos
- **Testabilidad**: Cada componente puede ser probado de forma aislada
- **Legibilidad**: El código es más claro al tener responsabilidades bien definidas

### Justificación 2 (Bajo Acoplamiento - Low Coupling)

**Aplicación del Patrón:**
El sistema implementa bajo acoplamiento mediante el uso de interfaces y inyección de dependencias:

- **Uso de Interfaz IReservaRepository**: El `ReservaService` depende de la abstracción `IReservaRepository`, no de la implementación concreta `ReservaRepositoryTxt`. Esto permite cambiar la forma de persistencia (TXT, CSV, JSON, Base de Datos) sin modificar la lógica de negocio.

- **Inyección de Dependencias**: El `ReservaService` recibe la implementación del repositorio a través de su constructor, no la crea internamente. Esto reduce el acoplamiento y facilita las pruebas unitarias.

- **Separación de Capas**: Cada capa solo conoce la interfaz de la capa inferior, no su implementación específica. La GUI solo conoce el Service, el Service solo conoce la interfaz del Repository.

**Beneficios Obtenidos:**
- **Flexibilidad**: Fácil intercambio de implementaciones (cambiar de archivo TXT a base de datos)
- **Extensibilidad**: Nuevas funcionalidades pueden agregarse sin afectar código existente
- **Testabilidad**: Posibilidad de usar mocks para pruebas unitarias
- **Mantenimiento**: Cambios en una capa no afectan otras capas del sistema

## Estructura de Archivos

\`\`\`
taller1/
├── ENTITY/
│   ├── ENTITY.csproj
│   └── Reserva.cs
├── DAL/
│   ├── DAL.csproj
│   ├── IReservaRepository.cs
│   └── ReservaRepositoryTxt.cs
├── BLL/
│   ├── BLL.csproj
│   └── ReservaService.cs
├── taller/ (GUI)
│   ├── GUI.csproj
│   └── Program.cs
├── taller.sln
└── reservas.txt (generado automáticamente)
\`\`\`

## Referencias entre Proyectos

- **GUI** → BLL, ENTITY
- **BLL** → DAL, ENTITY  
- **DAL** → ENTITY

## Tecnologías Utilizadas

- **.NET Framework 4.8**
- **C#**
- **Persistencia en archivo de texto**
- **Aplicación de consola**

## Cómo Ejecutar

1. Abrir la solución `taller.sln` en Visual Studio
2. Establecer el proyecto `taller` (GUI) como proyecto de inicio
3. Compilar la solución (Build → Build Solution)
4. Ejecutar el proyecto (F5 o Ctrl+F5)

## Funcionalidades del Sistema

### Menú Principal
1. **Registrar reserva**: Permite ingresar solicitante y sala para crear una nueva reserva
2. **Listar reservas**: Muestra todas las reservas registradas en formato tabular
3. **Salir**: Termina la ejecución del programa

### Validaciones Implementadas
- Solicitante no puede estar vacío
- Sala no puede estar vacía
- Manejo de errores en operaciones de archivo
- Formato de fecha consistente (dd/MM/yyyy)

## Patrones de Diseño Aplicados

- **Repository Pattern**: Abstracción del acceso a datos
- **Service Layer Pattern**: Encapsulación de lógica de negocio
- **Dependency Injection**: Inyección de dependencias para bajo acoplamiento
- **Layered Architecture**: Separación en capas con responsabilidades específicas
