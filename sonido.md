**Configurar una escena simple en 3D con un objeto cubo que hará de player y varias esferas de color. Agregar un objeto AudioSource desde el menú GameObject → Audio Seleccionar un clip de audio en algún paquete de la Asset Store de tu gusto y adjuntarlo a una esfera. El audio se debe reproducir en cuanto se carga la escena y en bucle.** 

Para la implementación del sistema de sonido en Unity, se configuró una escena 3D utilizando el *Built-in Render Pipeline*, donde un objeto cubo actúa como jugador y varias esferas funcionan como emisores estáticos. El componente **AudioListener**, situado en la cámara principal, se vinculó al movimiento del jugador para permitir el cálculo de la posición relativa, mientras que a las esferas se les asignó el componente **AudioSource** con las propiedades *Loop* y *Play On Awake* activadas.


**En la escena anterior crea un objeto con una fuente de audio a la que le configures el efecto Doppler elevado y que se mueva al pulsar la tecla m a una velocidad alta. Explica los efectos que produce: ● Incrementar el valor del parámetro Spread ● Cambiar la configuración de Min Distance y Max Distance ● Cambiar la curva de Logarithmic Rollof a Linear Rollof**

En esta actividad, he implementado un sistema de física de sonido 3D en Unity con el objetivo de analizar cómo el movimiento y la configuración de los parámetros de audio afectan a la percepción del usuario. Para ello, he configurado un objeto con una fuente de sonido y un script de movimiento de alta velocidad activado por la tecla M. El propósito principal ha sido simular y estudiar el efecto Doppler, observando cómo la compresión de las ondas sonoras altera la frecuencia del audio en función de la velocidad relativa entre la fuente y el oyente.

Asimismo, he experimentado con la personalización del entorno acústico mediante el ajuste de las curvas de Rolloff (cambiando el modelo logarítmico por uno lineal para una caída de volumen predecible) y la modificación del Spread y los rangos de distancia mínima y máxima. Estas pruebas me han permitido comprender que el sonido en un entorno virtual no solo depende de la fuente, sino de cómo se gestiona su difusión y atenuación en el espacio, permitiéndome controlar con precisión la inmersión sonora y la relevancia de los objetos en la escena según su proximidad.

**Configurar un mezclador de sonidos, aplica a uno de los grupo un filtro de echo y el resto de filtros libre. Configura cada grupo y masteriza el efecto final de los sonidos que estás mezclando. Explica los cambios que has logrado con tu mezclador.**

Al ejecutar el juego con esta configuración, esta es la masterización y explicación del resultado final:

Al ejecutar el proyecto con esta configuración, he logrado una masterización dinámica del entorno sonoro mediante el uso de mezcladores y efectos en tiempo real. Al dirigir el audio a través del grupo Echo, he conseguido que los sonidos no se corten de forma abrupta, sino que generen repeticiones con una degradación progresiva que simula el rebote en grandes estructuras, mientras que el grupo Ambiente con *SFX Reverb* ha eliminado la "sequedad" de la grabación original. Esto dota al objeto de una presencia física real, permitiendo que el usuario perciba el espacio (como una nave industrial o un exterior amplio) a través de la reverberación y la mezcla de frecuencias.

Finalmente, he aplicado un Compressor en el canal Master para unificar toda la mezcla y garantizar un acabado profesional. Este efecto actúa controlando el rango dinámico: reduce los picos de volumen que podrían causar distorsión al solaparse el eco y el reverb, y eleva los matices más sutiles. El resultado es un audio compacto y equilibrado donde, a pesar de la complejidad de los efectos aplicados, el nivel de salida se mantiene estable y agradable para el oyente, evitando saturaciones indeseadas durante el movimiento a alta velocidad.

**Implementar un script que al pulsar la tecla p accione el movimiento de una esfera en la escena y reproduzca un sonido en bucle hasta que se pulse la tecla s.**\


Se desarrolló el script ControlEsfera para gestionar la sincronización entre el desplazamiento del objeto y la reproducción sonora mediante la API de AudioSource. Tras declarar la variable de referencia myAudio y activar su propiedad de bucle (loop = true) en la inicialización para garantizar una ejecución continua, se implementó una lógica de control de flujo donde la tecla P habilita el movimiento y dispara el método Play() —verificando previamente que no esté activo para evitar reinicios—, mientras que la tecla S interrumpe el desplazamiento y silencia el audio inmediatamente mediante el comando Stop().


` `**Implementar un script en el que el cubo-player al colisionar con las esferas active un sonido.**

Se implementó el método OnCollisionEnter para detectar interacciones físicas entre el jugador y los objetos del entorno. Mediante una condicional, se verifica si el objeto impactado posee la etiqueta ("Tag") **"Esfera"**. En caso afirmativo, se utiliza el método PlayOneShot() del componente AudioSource, el cual permite reproducir el sonido de impacto completo sin cortar otros sonidos que pudieran estar sonando simultáneamente.



**Modificar el script anterior para que según la velocidad a la que se impacte, el cubo lance un sonido más fuerte o más débil.** 

Con el objetivo de aumentar el realismo en la interacción física, se modificó el script de colisión para que la intensidad auditiva fuese proporcional a la fuerza del choque. Dado que el objeto jugador cuenta con propiedades físicas, se accedió a su componente Rigidbody dentro del evento OnCollisionEnter para obtener la magnitud de su velocidad actual (velocity.magnitude).

Este valor de velocidad se utilizó para calcular el nivel de volumen adecuado, dividiéndolo por un factor de escala predefinido para normalizar el resultado dentro del rango audible (0 a 1). Antes de emitir el sonido, se asignó este valor calculado directamente a la propiedad volume del componente AudioSource. Finalmente, se invocó el método PlayOneShot(), logrando que los impactos a mayor velocidad generen una respuesta sonora más contundente sin necesidad de utilizar múltiples clips de audio.

**Agregar un sonido de fondo a la escena que se esté reproduciendo continuamente desde que esta se carga. Usar un mezclador para los sonidos.**

Para la ambientación sonora de la escena, se ha incorporado una fuente de audio configurada para ejecutarse automáticamente al inicio de la aplicación. Se ha creado un objeto independiente con un componente **AudioSource**, activando las propiedades **Play On Awake** para su ejecución inmediata tras la carga de la escena y **Loop** para garantizar una reproducción cíclica e ininterrumpida.

Siguiendo la arquitectura de audio planteada, la salida (Output) de esta fuente se ha enrutado hacia un nuevo grupo dedicado en el **Audio Mixer** denominado "Musica\_Fondo". Esta separación en la mesa de mezclas permite gestionar el volumen de la música de forma independiente al de los efectos de sonido (SFX) y aplicar ecualización específica sin afectar al resto de elementos sonoros del proyecto.

**Crear un script para simular el sonido que hace el cubo-player cuando está en contacto con el suelo (mecánica para reproducir sonidos de pasos).** 

Para recrear el sonido de la locomoción del personaje, se implementó un sistema basado en intervalos de tiempo, ya que la reproducción continua en cada frame generaría saturación auditiva.

Se desarrolló el script PasosJugador que detecta la entrada del usuario (Input.GetAxis). Cuando se detecta movimiento horizontal o vertical, se inicia una cuenta regresiva utilizando una variable acumuladora y Time.deltaTime. Al cumplirse el ciclo de tiempo establecido (frecuencia del paso), se dispara el método PlayOneShot() con el clip de audio correspondiente y se reinicia el temporizador. Esto garantiza que el sonido se reproduzca rítmicamente y sincronizado con el desplazamiento, silenciándose automáticamente cuando el jugador detiene su marcha.

**En la escena de tus ejercicios 2D incorpora efectos de sonido ajustados a los siguientes requisitos: Crea un grupo SFX en el AudioMixer para eventos.**

Para integrar la ambientación sonora en el proyecto 2D, se creó un grupo dedicado "SFX" en el Audio Mixer y se modificó el script principal del jugador (Salto) incorporando un componente AudioSource y referencias a los *clips* de audio para salto, aterrizaje, recolección y cambio de estado (*Power Up*). Se implementó la reproducción mediante PlayOneShot sincronizada con eventos específicos: la detección de entrada para el salto, la colisión con superficies ("suelo" o "Plataforma") para el aterrizaje y el incremento de estadísticas para la mejora de vida/habilidad. Adicionalmente, se delegó la ejecución del sonido de las monedas en el propio jugador, permitiendo que el script Moneda invoque el método público de audio antes de su destrucción (Destroy), garantizando así que el efecto se escuche completo sin interrumpirse.


**Crear un grupo ambiente**

Para mejorar la inmersión atmosférica del nivel, se ha sustituido la música melódica por un sistema de sonido ambiental continuo. Se configuró un emisor de audio global (AudioSource) que reproduce un bucle de ambiente base (ej. viento o naturaleza) desde la carga de la escena.

Para diferenciar áreas narrativas o físicas (como entrar en una cueva o sumergirse en agua), se desarrolló el script ControlAmbiente. Mediante el uso de zonas de activación (*Triggers*), el sistema detecta la presencia del jugador e intercambia en tiempo real el *clip* de audio ambiental. Al entrar en la zona designada, el sonido base se sustituye por uno específico del entorno (ej. reverberación o humedad) y, al salir, se restaura automáticamente la atmósfera original, garantizando una transición coherente en la experiencia auditiva del entorno.

**Crea un grupo para música**

Para complementar el diseño sonoro y reforzar el tono estético del juego, se ha incorporado una capa musical independiente. Se creó un nuevo grupo denominado "Musica" dentro del Audio Mixer, permitiendo un control de volumen y ecualización segregado del resto de canales (SFX y Ambiente).

En la escena, se implementó una fuente de audio (AudioSource) dedicada exclusivamente a la reproducción musical. Esta fuente se configuró con la propiedad Loop habilitada para garantizar una repetición continua y fluida de la pista, y se enrutó su salida (Output) hacia el grupo "Musica" del mezclador. Esta arquitectura asegura que la banda sonora se mantenga constante como base auditiva, permitiendo al mismo tiempo que los efectos de sonido y el ambiente destaquen en la mezcla final gracias a la jerarquía de grupos establecida.

