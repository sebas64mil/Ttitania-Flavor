# Titania Flavor

## Integrantes del Grupo
- Sebastian Villamil
- Katherine Guayazan
- Elisa Ingilar

---

## Descripción del Proyecto

**Titania Flavor** es un juego multijugador de cocina espacial desarrollado con **Photon 2** como solución de networking. El juego soporta hasta **4 jugadores máximo** y utiliza una arquitectura **Client/Player Host** donde uno de los jugadores actúa como anfitrión de la sesión.

### Características Principales
- Experiencia multijugador en tiempo real
- Temática de cocina en el espacio
- Soporte para hasta 4 jugadores por partida
- Arquitectura descentralizada con host
- Sistema de lobby para gestión de jugadores

---

## Estado del Proyecto

### ✅ Tareas Realizadas
- Integración de Photon 2 PUN2
- Sistema base de conexión multijugador
- Sala de lobby funcional
- Sistema de identificación de jugadores

### ⏳ Tareas Pendientes

1. **Lista de secciones creadas para el ingreso de jugadores**
   - Crear interfaz visual para mostrar los jugadores conectados
   - Diseñar slots de jugadores disponibles

2. **Límite máximo de 4 jugadores**
   - Implementar validación para rechazar conexiones cuando la sala esté llena
   - Mostrar indicador visual de capacidad de la sala

3. **Mejorar la información vista solamente por el host en la sala de lobby**
   - Mostrar opciones adicionales exclusivas para el host
   - Implementar interfaz diferenciada para anfitrión

4. **Que el host pueda expulsar jugadores**
   - Crear sistema de expulsión de jugadores
   - Implementar confirmación y notificaciones

5. **Que el client pueda salir de la sala**
   - Implementar botón de salida/abandonar
   - Gestionar desconexión correctamente

6. **Pasar del lobby a la escena de game**
   - Crear flujo de transición entre escenas
   - Sincronizar inicio de partida para todos los jugadores

---

## Estructura del Proyecto

```
Assets/
├── Photon/                 # Recursos de Photon 2
├── Titania_Flavor/         # Código y recursos del juego
│   ├── Scenes/             # Escenas del juego
│   ├── Scripts/            # Scripts C#
│   ├── Prefabs/            # Prefabs
│   └── UI/                 # Interfaz de usuario
├── Settings/               # Configuración del proyecto
└── TextMesh Pro/           # Fuente para UI
```

---

## Notas
- El proyecto utiliza Unity con Photon 2 para networking
- La arquitectura permite escalabilidad para futuras mejoras
- Se requiere configuración de Photon AppID para funcionamiento en línea
