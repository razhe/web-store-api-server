# Proyecto Web Store

Este proyecto ha sido desarrollado por mí y concebido exclusivamente con fines académicos. Lo que me motivó a crearlo es la posibilidad de desarrollar un sistema backend para gestionar un sistema de ventas o carrito de compras desde el módulo de administración.

## Información del proyecto:

- Este proyecto está desarrollado en .NET 6.
- Los datos están almacenados en SQL Server.
- Se está trabajando bajo un patrón CQRS y Mediador.
- Se utiliza una clase genérica para manejar los errores entre capas de la aplicación (Features y Controllers).
- Se emplea una técnica para capturar cualquier error arrojado por la aplicación mediante el "GlobalExceptionHandlerMiddleware" y entregar la información adecuada para la depuración.
- Se hacen uso de conceptos como interceptores de EF para auditar las entidades e implementar estrategias de eliminación lógica.

---

# Web Store Project

This project has been developed by me and conceived solely for academic purposes. What motivated me to create it is the opportunity to develop a backend system to manage a sales system or shopping cart from the administration module.

## Project Information:

- This project is developed in .NET 6.
- Data is stored in SQL Server.
- It is being developed under a CQRS and Mediator pattern.
- A generic class is used to handle errors between application layers (Features and Controllers).
- A technique is employed to capture any errors thrown by the application through the "GlobalExceptionHandlerMiddleware" and provide appropriate information for debugging.
- Concepts such as EF interceptors are used to audit entities and implement logical deletion strategies.

---

© Gustavo Acuña
