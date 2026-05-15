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

### ⏳ Tareas Pendientes (priorizadas)

- **Alta:** Implementar validación del límite de 4 jugadores (rechazar nuevas conexiones y mostrar indicador de capacidad).
- **Alta:** Flujo de transición lobby → partida y sincronización del inicio de partida para todos los jugadores.
- **Media:** Interfaz del lobby: lista visual de jugadores conectados y UI diferenciada para el host.
- **Media:** Permitir que el host pueda expulsar jugadores (confirmación y notificaciones).
- **Baja:** Botón para que el cliente salga/abandone la sala y manejo correcto de la desconexión.
- **Baja:** Configurar y validar Photon AppID y ajustes de red para despliegue.

Si quieres, puedo convertir cada ítem en tickets más detallados (issues) con subtareas y estimaciones.

---


## Estructura del Proyecto 

```text
Assets/
├── Titania_Flavor/
│   ├── Scenes/
│   │   ├── Menu.unity
│   │   └── Game.unity
│   │
│   ├── Prefabs/
│   │   ├── Player/
│   │   │   └── PlayerPrefab.prefab
│   │   │
│   │   ├── Props/
│   │   │   └── GrabObjectNetwork.prefab
│   │   │
│   │   └── UI/
│   │       ├── Button.prefab
│   │       └── ContentRoom.prefab
│   │
│   ├── Scripts/
│   │   ├── Application/
│   │   │   ├── Managers/
│   │   │   │   └── GameManager.cs
│   │   │   │
│   │   │   └── Player/
│   │   │       ├── PlayerMovement.cs
│   │   │       ├── PlayerInputHandler.cs
│   │   │       ├── PlayerGrabber.cs
│   │   │       └── GrabbableObject.cs
│   │   │
│   │   ├── Domain/
│   │   │   ├── Data/
│   │   │   ├── Events/
│   │   │   └── Interface/
│   │   │
│   │   ├── Network/
│   │   │   ├── Lobby/
│   │   │   └── FusionCallbacks.cs
│   │   │
│   │   └── Presentation/
│   │       └── Ui/
│   │
│   └── Materials/
│
└── Photon/
    ├── Fusion/
    └── FusionAddons/
```


## Notas
- El proyecto utiliza Unity con Photon 2 para networking
- La arquitectura permite escalabilidad para futuras mejoras
- Se requiere configuración de Photon AppID para funcionamiento en línea
