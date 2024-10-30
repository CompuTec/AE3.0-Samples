# Computec.Core2 Ecosystem is a powerful framework designed for developing extensions tailored for SAP UI and SAP web-based business applications. This versatile ecosystem emphasizes unification, scalability, maintainability, and advanced analytics, streamlining the development and delivery of business applications.
## Key Features:
•	Unified Logging System: A cohesive logging approach that simplifies tracking and debugging across all extensions.
•	Comprehensive Reporting & Metrics: Built-in tools for generating insightful reports and performance metrics to enhance decision-making.
•	Plugin Delivery & Updates: A centralized store for seamless management and delivery of plugin updates, ensuring users have the latest features and fixes.
•	Licensing Management: Ongoing development of a licensing feature to facilitate secure and efficient management of application access.
•	Event Bus Architecture: Supports asynchronous communication within applications, enabling a responsive and dynamic system.
•	Job Scheduling & Event-Driven Execution: Easily configure jobs that can be triggered by specific events or scheduled to run at defined intervals.
•	Single Sign-On (SSO) Support: Enhance user experience with seamless authentication across multiple applications.
•	Rapid Development with .NET 8: Leverage a fast and streamlined codebase that accelerates development cycles without compromising quality.
•	Service-Oriented Architecture: Enjoy a well-structured architecture that promotes flexibility and ease of integration with other services.
•	Abstracted SAP APIs: Simplifies integration by abstracting standard SAP DI and SL APIs, allowing developers to focus on business logic without navigating multiple technologies.
•	Licensing Management: Ongoing development of a licensing feature to facilitate secure and efficient management of application access.
•	Unified Business Logic Access: All user-defined objects are automatically accessible in both .NET Core assemblies and OData/CRUD controllers, enabling easy integration with third-party applications.
•	Custom API Development: Simplified creation of custom OData/API controllers with support for Minimal APIs, including built-in authentication features.
•	Full Support for Extensions of Extensions: Develop extensions that leverage existing plugins as dependencies, allowing you to enhance their logic with your own code. This feature promotes modular development and fosters a rich ecosystem of interconnected functionalities.
•	Complete SAP UI5 Framework Support: Integrate seamlessly with the SAP UI5 framework using TypeScript, utilizing built-in libraries for quick and easy development. This support enables developers to harness the power of modern web technologies while ensuring compatibility with SAP standards.
## Why Choose Computec.Core2?
The Computec.Core2 ecosystem empowers developers to create robust extensions with minimal overhead, providing tools and features that enhance productivity while ensuring high standards of performance and security. Whether you're looking to streamline business processes or integrate new functionalities, Computec.Core2 is your go-to solution for modern business application development.
## Environment Overview
At the heart of the Computec.Core2 Ecosystem is the AppEngine server, which serves as the central management hub for the SAP installation (SAP SLD) along with all associated databases and servers. This robust architecture enables administrators to efficiently manage all plugins across multiple companies within the ecosystem, ensuring streamlined operations and enhanced performance.
## AppEngine Server Responsibilities:
•	Central Administration of SAP Companies Ecosystem:
o	Activating Companies: Facilitate the activation of multiple SAP companies within the ecosystem, ensuring seamless integration and management.
o	Installing and Updating Plugins: Efficiently manage the installation and updates of plugins for specific companies, ensuring that all applications are up-to-date and functioning optimally.
•	Event Bus Management:
o	The AppEngine server is responsible for hosting and managing the Event Bus, enabling asynchronous communication between different components of the ecosystem. This architecture promotes responsiveness and dynamic interactions across applications.
o	The Event Bus can be configured to utilize external message queue services such as Microsoft Event Bus or RabbitMQ, providing flexibility and scalability in communication.
•	Hosting AE Plugins:
o	The server hosts AE plugins, including controllers and web applications, allowing for modular development and enhancing the overall functionality of the ecosystem.
•	Job Execution:
o	The AppEngine server executes scheduled and event-driven jobs, ensuring timely execution of tasks critical to the operation of the ecosystem.
•	Connection Logging and OpenTelemetry Management:
o	Manages connection logging and OpenTelemetry settings, providing comprehensive insights into application performance and facilitating effective monitoring and troubleshooting.
## Load Balancing Capabilities
The architecture of the AppEngine server allows for the deployment of multiple instances to load balance the tasks handled by each server. While this capability is currently in progress and not fully supported yet, plans are in place to enhance scalability and reliability, ensuring optimal performance as demands increase.
