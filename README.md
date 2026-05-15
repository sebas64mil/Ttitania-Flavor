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


## Estructura del Proyecto (árbol completo)

```
d:\projectosunity\Ttitania Flavor/
- .gitattributes
- .gitignore
- .vsconfig
- Assembly-CSharp-Editor.csproj
- Assembly-CSharp.csproj
- Fusion.Addons.Physics.csproj
- Fusion.Addons.Physics.Editor.csproj
- Fusion.Menu.csproj
- Fusion.Menu.Editor.csproj
- Fusion.Unity.csproj
- Fusion.Unity.Editor.csproj
- README.md
- Ttitania Flavor.sln
- Unity.Fusion.CodeGen.csproj
- Packages/
	- manifest.json
	- packages-lock.json
- ProjectSettings/
	- AudioManager.asset
	- ClusterInputManager.asset
	- DynamicsManager.asset
	- EditorBuildSettings.asset
	- EditorSettings.asset
	- EntitiesClientSettings.asset
	- GraphicsSettings.asset
	- InputManager.asset
	- MemorySettings.asset
	- MultiplayerManager.asset
	- NavMeshAreas.asset
	- NetCodeClientAndServerSettings.asset
	- NetCodeServerSettings.asset
	- PackageManagerSettings.asset
	- Physics2DSettings.asset
	- PresetManager.asset
	- ProjectSettings.asset
	- ProjectVersion.txt
	- QualitySettings.asset
	- SceneTemplateSettings.json
	- ShaderGraphSettings.asset
	- TagManager.asset
	- TimeManager.asset
	- URPProjectSettings.asset
	- UnityConnectSettings.asset
	- VFXManager.asset
	- VirtualProjectsConfig.json
	- XRSettings.asset
	- Packages/
		- com.unity.services.vivox/Settings.json
- Assets/
	- InputSystem_Actions.cs
	- InputSystem_Actions.cs.meta
	- InputSystem_Actions.inputactions
	- InputSystem_Actions.inputactions.meta
	- Readme.asset
	- Readme.asset.meta
	- UserChoices.choices
	- UserChoices.choices.meta
	- Test.inputactions
	- Test.inputactions.meta
	- Photon/
		- PhotonLibs/
			- WebSocket/
				- WebSocket.jslib
				- WebSocket.jslib.meta
				- websocket-sharp.README
				- websocket-sharp.README.meta
				- websocket-sharp.dll
				- websocket-sharp.dll.meta
				- WebSocket.cs
				- WebSocket.cs.meta
				- SocketWebTcp.cs
				- SocketWebTcp.cs.meta
				- PhotonWebSocket.asmdef
				- PhotonWebSocket.asmdef.meta
			- netstandard2.0/
				- Photon3Unity3D.dll
				- Photon3Unity3D.dll.meta
				- Photon3Unity3D.xml
				- Photon3Unity3D.xml.meta
		- Fusion/
			- package.json
			- package.json.meta
			- build_info.txt
			- build_info.txt.meta
			- Assemblies/
				- Fusion.Realtime.dll
				- Fusion.Log.dll
				- Fusion.Common.dll
				- Fusion.Runtime.dll
				- Release/ (varios .release)
				- Debug/ (varios .debug)
			- Runtime/
				- FusionBootstrap.cs
				- FusionBasicBillboard.cs
				- FusionAddressablePrefabsPreloader.cs
				- Fusion.Unity.cs
				- Fusion.Unity.AssemblyAttributes.cs
				- NetworkSceneManagerDefault.cs
				- NetworkObjectProviderDefault.cs
				- NetworkCharacterController.cs
				- Utilities/
					- RunnerVisibility/
						- RunnerVisibilityLinksRoot.cs
						- RunnerVisibilityLink.cs
						- RunnerLagCompensationGizmos.cs
						- RunnerAOIGizmos.cs
						- RunnerEnableVisibility.cs
						- EnableOnSingleRunner.cs
				- Statistics/
					- FusionStatsWorldAnchor.cs
					- FusionStatsPanelHeader.cs
					- FusionStatsGraphDefault.cs
					- FusionStatsConfig.cs
					- FusionStatsCanvas.cs
					- FusionStatistics.cs
					- FusionNetworkObjectStatsGraphCombine.cs
					- FusionNetworkObjectStatsGraph.cs
					- FusionNetworkObjectStatistics.cs
					- Prefabs/ (varios)
					- Resources/
			- Editor/
				- NetworkProjectConfigImporterEditor.cs
				- NetworkProjectConfigImporter.cs
				- NetworkPrefabsInspector.cs
				- FusionWeaverTriggerImporter.cs
				- FusionRunnerVisibilityControlsWindow.cs
				- FusionHubWindow.cs
				- FusionEditorSkin.cs
				- FusionEditorConfigImporter.cs
				- Fusion.Unity.Editor.cs
				- Fusion.Unity.Editor.AssemblyAttributes.cs
				- EditorResources/ (imágenes y fuentes)
			- CodeGen/
				- Fusion.CodeGen.cs
				- Fusion.CodeGen.User.cs
				- Fusion.CodeGen.Trigger.fusionweavertrigger
	- Photon/Plugins/
		- NanoSockets/ (DLLs y libs para plataformas)
	- Photon/FusionAddons/
		- Physics/
			- RunnerSimulatePhysics/
				- RunnerSimulatePhysicsEnums.cs
				- RunnerSimulatePhysicsBaseT.cs
				- RunnerSimulatePhysicsBase.cs
				- RunnerSimulatePhysics3D.cs
				- RunnerSimulatePhysics2D.cs
			- NetworkRigidbody/
				- NetworkRigidbody2D.cs
				- NetworkRigidbody3D.cs
				- NetworkRigidbodyBase/
					- NetworkRigidbodyBase.cs
					- NetworkRigidbodyBase.Copy.cs
					- NetworkRigidbodyBase.Render.cs
					- NetworkRigidbodyBase.Teleport.cs
					- NetworkRigidbody.cs
					- NetworkRigidbodyTypes.cs
	- Photon/Fusion/CodeGen/ (asmdef y archivos generados)
	- Photon/Fusion/Resources/
		- PhotonAppSettings.asset
		- NetworkProjectConfig.fusion
	- Photon/Fusion/Plugins/ (NanoSockets etc.)
	- Photon/PhotonLibs/changes-library.txt
	- TextMesh Pro/
		- Sprites/
			- EmojiOne.png
			- EmojiOne.png.meta
			- EmojiOne.json
			- EmojiOne.json.meta
			- EmojiOne Attribution.txt
		- Fonts/
			- LiberationSans.ttf
			- LiberationSans.ttf.meta
			- LiberationSans - OFL.txt
		- Resources/
			- TMP Settings.asset
			- Style Sheets/
		- Shaders/ (varios .shader/.cginc/.shadergraph)
	- Settings/
		- UniversalRenderPipelineGlobalSettings.asset
		- SampleSceneProfile.asset
		- PC_RPAsset.asset
		- PC_Renderer.asset
		- Mobile_RPAsset.asset
		- Mobile_Renderer.asset
		- DefaultVolumeProfile.asset
	- TutorialInfo/
		- Layout.wlt
		- Icons/
			- URP.png
		- Scripts/
			- Readme.cs
			- Readme.cs.meta
			- Editor/
				- ReadmeEditor.cs
				- ReadmeEditor.cs.meta
	- Titania_Flavor/
		- Scenes/
			- Menu.unity
			- Game.unity
			- Menu.unity.meta
			- Game.unity.meta
		- Prefabs/
			- UI/
				- ContentRoom.prefab
				- Button.prefab
			- Props/
				- GrabObjectNetwork.prefab
				- Materials/
					- Object1.mat
			- Player/
				- PlayerPrefab.prefab
				- Materials/
					- Player.mat
		- Scripts/
			- Presentation/
				- Ui/
					- Menu/
						- SessionListUI.cs
						- SessionItemUI.cs
						- LobbyUI.cs
					- Juego/
						- RoomInfo.cs
						- PlayerSpawner.cs
						- LobbyUIReferences.cs
						- LobbyLoadingUI.cs
			- Network/
				- Lobby/
					- LobbyDecorator.cs
					- FusionStartHandler.cs
					- Core/
						- LobbyNetwork.cs
						- HostOffServerHandler.cs
						- ClientDisconnectHandler.cs
					- Decorator/
						- RoomCodeDecorator.cs
						- CallbackDecorator.cs
						- SceneDecorator.cs
				- FusionCallbacks.cs
			- Application/
				- Player/
					- PlayerMovement.cs
					- PlayerInputHandler.cs
					- PlayerGrabber.cs
					- GrabbableObject.cs
				- Managers/
					- GameManager.cs
			- Domain/
				- Interface/
					- Mechanics/
						- IGrabbable.cs
					- Lobby/
						- ILobbyStartHandler.cs
				- Data/
					- StartGameContext.cs
					- NetworkInputData.cs
				- Events/
					- LobbyEvents.cs
	- Prefabs/ (prefabs en raíz)
	- Readme.asset
	- Titania_Flavor.meta
- Library/ (cache de Unity; no enumerada completamente)
- Logs/
- obj/
	- Debug/
- UserSettings/ (no enumerado)
- .vs/ (posible)
- otros archivos generados/.meta

```

He incluido el árbol completo con las carpetas y los archivos principales detectados (incluye archivos `.meta` y carpetas generadas solo cuando son relevantes). Si quieres que liste absolutamente todas las 635 entradas línea por línea (incluyendo Library), creo un `project-tree.txt` en la raíz con la lista completa.
---

## Notas
- El proyecto utiliza Unity con Photon 2 para networking
- La arquitectura permite escalabilidad para futuras mejoras
- Se requiere configuración de Photon AppID para funcionamiento en línea
