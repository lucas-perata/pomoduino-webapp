# 🚀 Pomoduino  

**Pomoduino** es un proyecto que desarrollé entre 2024 y 2025 combinando software, hardware y técnicas de programación baremetal. Durante mucho tiempo busqué un dispositivo que me ayudara a organizarme sin depender del celular, y así nació esta herramienta única diseñada para mejorar mi uso del tiempo.  

## 🎛️ Hardware  

- **Arduino Nano** combinado con un módulo **ESP-8266 Wi-Fi**, permitiendo sincronización en tiempo real con una API y una base de datos SQL.  
- Diseño único creado con **impresión 3D**.  

### Prototipo  
- Pomoduino ensamblado.  

## 💻 Software  

- Programado en **C/C++** para la parte de Arduino.  
- **C# con Blazor .NET** para la web app.  
- La web app incluye una **API CRUD** para gestionar y medir el tiempo estés donde estés.  

### Web App  
- Funcionamiento básico del sistema.  

## 🔑 Características principales  

- ✅ **Reloj Pomodoro**: Optimiza el uso del tiempo con ciclos de concentración y descanso.  
- ✅ **Múltiples categorías**: Organiza tareas según etiquetas personalizadas.  
- ✅ **Sincronización con base de datos**: Cada ciclo de trabajo queda registrado.  
- ✅ **Hardware personalizado**: Conectividad y diseño ergonómico adaptable.  
- ✅ **Interfaz web intuitiva**: Gestiona el tiempo desde cualquier navegador.  

## 🌐 Pomoduino en acción  

Pomoduino muestra cómo la tecnología puede facilitar la vida diaria y mejorar la productividad.  

### Funcionamiento básico del Pomoduino  

## 🛠️ Desafíos técnicos  

1. **Memoria limitada**: Optimizar el código para 32 KB de memoria FLASH en el Arduino Nano y Uno fue un gran desafío.  
2. **Conexión estable**: Asegurar la comunicación entre el módulo ESP8266 y el Arduino utilizando comandos AT y una fuente step-down para la adaptación de voltaje (5V a 3.3V).  
3. **Reloj de cuenta regresiva no bloqueante**: Utilicé el temporizador TCNT1 con un prescaler para evitar que el microcontrolador quedara congelado durante la cuenta regresiva, permitiendo la ejecución paralela de otros procesos como lectura de botones o comunicación serial.  
4. **Del prototipo a la versión final**: Pasar del protoboard al dispositivo ensamblado implicó superar problemas de organización y estructura del proyecto.  

## 📂 Repositorio del proyecto  

- [Pomoduino Web App](https://github.com/lucas-perata/pomoduino-webapp)  
- [Pomoduino Firmware](https://github.com/lucas-perata/pomoduino)  

## 🛠️ Habilidades destacadas  

- Desarrollo de software **baremetal y web** (C/C++, C#, Blazor).  
- Programación en hardware (**Arduino**, **ESP-8266**).  
- Diseño e **impresión 3D**.  
- Gestión de bases de datos en **SQL** (local y en la nube).  
