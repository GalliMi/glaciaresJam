# Glaciar Jam - Sistema de UI y Gestión de Juego

## Descripción del Proyecto

Glaciar Jam es un juego de defensa donde el jugador debe proteger tres perglaciares de ataques enemigos. El sistema de interfaz de usuario (UI) proporciona una experiencia completa con menú principal, sistema de derrota y gestión del estado del juego.

## Sistema de UI Implementado

### 1. Menú Principal (`MenuPrincipal.cs`)

**Funcionalidad:**
- Botón "Comenzar": Inicia el juego cargando la escena principal
- Botón "Salir": Cierra la aplicación (funciona tanto en editor como en build)
- Control del tiempo del juego (`Time.timeScale = 1f`)
- Sistema de listeners para eventos de botones

**Configuración en Unity:**
```csharp
// Variables asignables desde el Inspector
public Button botonComenzar;
public Button botonSalir;
public GameObject canvasMenuPrincipal;
public string nombreEscenaJuego = "SampleScene";
```

**Uso:**
1. Crear un Canvas con dos botones
2. Asignar los botones a las variables correspondientes
3. Configurar el nombre de la escena del juego
4. Asegurar que la escena del menú esté configurada en Build Settings

### 2. Gestor de Juego (`GestorJuego.cs`)

**Funcionalidad Principal:**
- **Sistema de Derrota**: Monitorea las barras de energía de los tres perglaciares
- **Game Over**: Activa pantalla de derrota cuando todas las barras llegan a cero
- **Control de Tiempo**: Detiene el juego (`Time.timeScale = 0f`) al perder
- **Opciones de Reinicio**: Botón para reiniciar o volver al menú principal

**Características:**
- Patrón Singleton para acceso global
- Verificación constante del estado de los perglaciares
- Sistema de eventos para botones de Game Over
- Manejo de escenas para reinicio y navegación

**Configuración en Unity:**
```csharp
// Referencias a perglaciares
public Glaciar periglaciar1;
public Glaciar periglaciar2;
public Glaciar periglaciar3;

// UI de Game Over
public GameObject canvasGameOver;
public TextMeshProUGUI textoGameOver;
public Button botonReiniciar;
public Button botonMenuPrincipal;

// Configuración
public string mensajeDerrota = "Has perdido el juego";
public string nombreEscenaMenu = "MenuPrincipal";
public string nombreEscenaJuego = "SampleScene";
```

## Requisitos Técnicos

### Dependencias
- **Unity 2021.3+**: Versión mínima recomendada
- **TextMeshPro**: Para todos los textos de la UI
- **UnityEngine.UI**: Para componentes de interfaz
- **UnityEngine.SceneManagement**: Para gestión de escenas

### Paquetes Necesarios
```
com.unity.ugui (UI System)
com.unity.textmeshpro (TextMeshPro)
```

## Configuración del Proyecto

### 1. Configuración de Escenas
1. **Menú Principal**: Crear escena "MenuPrincipal"
2. **Juego**: Escena principal "SampleScene" (o la existente)
3. **Build Settings**: Añadir ambas escenas en orden correcto

### 2. Configuración del Menú Principal
1. Crear Canvas (`UI > Canvas`)
2. Añadir dos botones (`UI > Button - TextMeshPro`)
3. Configurar textos: "Comenzar" y "Salir"
4. Añadir el script `MenuPrincipal.cs` al Canvas
5. Asignar referencias en el Inspector

### 3. Configuración del Gestor de Juego
1. Crear GameObject vacío llamado "GestorJuego"
2. Añadir el script `GestorJuego.cs`
3. Crear Canvas de Game Over (inicialmente inactivo)
4. Añadir texto y botones con TextMeshPro
5. Asignar todas las referencias:
   - Los tres perglaciares (scripts Glaciar)
   - Canvas de Game Over
   - Texto y botones

### 4. Configuración de Perglaciares
1. Asegurar que cada periglaciar tenga el script `Glaciar.cs`
2. Verificar que las referencias de salud (healthBar) estén configuradas
3. Los perglaciares deben tener etiquetas o ser referenciables

## Flujo del Juego

### Inicio
1. El juego comienza en el menú principal
2. El jugador presiona "Comenzar"
3. Se carga la escena del juego
4. `GestorJuego` inicializa y monitorea el estado

### Durante el Juego
1. Los perglaciares reciben daño y pierden salud
2. `GestorJuego` verifica constantemente el estado en `Update()`
3. Si las tres barras de energía llegan a cero:
   - Se activa `ActivarGameOver()`
   - `Time.timeScale = 0f` detiene el juego
   - Se muestra UI de Game Over

### Game Over
1. Aparece pantalla con mensaje "Has perdido el juego"
2. El jugador puede:
   - **Reiniciar**: Recarga la escena actual
   - **Menú Principal**: Vuelve al menú inicial

## Características Adicionales

### Sistema de Logs
- Todos los eventos importantes registran logs en consola
- Facilita debugging y seguimiento de problemas

### Manejo de Errores
- Validación de referencias nulas
- Mensajes de error específicos
- Prevención de múltiples activaciones

### Optimización
- Limpieza de listeners para evitar memory leaks
- Patrón Singleton para acceso eficiente
- Verificación de estado antes de ejecutar acciones

## Solución de Problemas Comunes

### Error CS0246 (Tipo no encontrado)
**Causa**: Falta de using UnityEngine.UI o TMPro
**Solución**: Asegurar que los scripts incluyan:
```csharp
using UnityEngine.UI;
using TMPro;
```

### Botones no funcionan
**Causa**: Referencias no asignadas en el Inspector
**Solución**: Verificar que todos los botones estén asignados

### Game Over no se activa
**Causa**: Referencias a perglaciares nulas o incorrectas
**Solución**: Asignar correctamente los scripts Glaciar de cada periglaciar

### Tiempo del juego no se detiene
**Causa**: `Time.timeScale` no se está configurando correctamente
**Solución**: Verificar que `ActivarGameOver()` se esté ejecutando

## Mejoras Futuras Sugeridas

### Gameplay
- Sistema de puntuación
- Niveles de dificultad
- Efectos visuales y sonoros

### UI/UX
- Animaciones de transición
- Efectos de partículas
- Sistema de tutoriales

### Técnico
- Sistema de guardado de progreso
- Optimización de rendimiento
- Soporte para múltiples idiomas

## Notas de Desarrollo

- Los scripts están diseñados para ser modulares y reutilizables
- Se siguió buenas prácticas de programación (Singleton, limpieza de memoria, etc.)
- El código está completamente comentado para facilitar mantenimiento
- Se utilizó TextMeshPro para mejor calidad de texto

---

**Desarrollado para Glaciar Jam**  
*Sistema de UI y Gestión de Juego*
