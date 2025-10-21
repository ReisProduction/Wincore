##  Table of Contents

- [ Overview](#overview)
- [ Features](#features)
- [ Project Structure](#project-structure)
  - [ Project Index](#project-index)
- [ Getting Started](#getting-started)
  - [ Prerequisites](#prerequisites)
  - [ Installation](#installation)
  - [ Usage](#usage)
  - [ Testing](#testing)
- [ Contributing](#contributing)
- [ License](#license)

---

##  Overview

The Wincore project is a robust .NET application designed to enhance security and manageability for Windows-based systems. It integrates seamlessly with Windows Credential Manager, offering secure credential storage and management, alongside advanced cryptographic functionalities including PBKDF2 and SHA hashing. Ideal for developers and IT professionals, Wincore ensures secure, efficient, and reliable system operations, making it essential for anyone managing sensitive data or requiring high security in their software environments.

---

##  Features

|      | Feature         | Summary       |
| :--- | :---:           | :---          |
| ⚙️  | **Architecture**  | <ul><li>Utilizes .NET framework targeting Windows environments.</li><li>Structured around robust security and credential management.</li><li>Designed for easy integration with Windows Credential Manager.</li></ul> |
| 🔩 | **Code Quality**  | <ul><li>Codebase primarily in CSharp, ensuring strong typing and object-oriented capabilities.</li><li>Includes detailed project configuration via Wincore.csproj.</li><li>Security-focused with dedicated modules for credential and hash management.</li></ul> |
| 📄 | **Documentation** | <ul><li>Documentation includes detailed setup and usage commands.</li><li>Codebase includes comments explaining the purpose and functionality of security keys and modules.</li><li>Documentation is likely accessible and maintained, given the structured commands and detailed code comments.</li></ul> |
| 🔌 | **Integrations**  | <ul><li>Direct integration with Windows Credential Manager.</li><li>Designed to work seamlessly within .NET and Windows ecosystems.</li><li>No external container dependencies, simplifying deployment in Windows environments.</li></ul> |
| 🧩 | **Modularity**    | <ul><li>Features modular design with separate classes for credential and hash management.</li><li>Allows for easy expansion or modification of security features.</li><li>Structured project files (.csproj) support modular builds and dependency management.</li></ul> |
| 🧪 | **Testing**       | <ul><li>Includes commands for running tests, indicating a test-driven development approach.</li><li>Test coverage likely focuses on security aspects, given the project's emphasis.</li><li>Utilizes .NET's built-in testing frameworks.</li></ul> |
| ⚡️  | **Performance**   | <ul><li>Optimized for performance with efficient credential management and hashing algorithms.</li><li>Performance enhancements through .NET optimizations and CSharp's efficient execution.</li><li>Security features designed to operate with minimal overhead to maintain performance.</li></ul> |
| 🛡️ | **Security**      | <ul><li>High focus on security with dedicated security key and credential management systems.</li><li>Uses robust hashing mechanisms (PBKDF2) for secure data handling.</li><li>Security features integrated directly with Windows' native capabilities.</li></ul> |
| 📦 | **Dependencies**  | <ul><li>Depends on NuGet for package management, typical for .NET projects.</li><li>Minimal external dependencies, focusing on native .NET and Windows features.</li><li>Project configuration file (Wincore.csproj) manages all dependencies efficiently.</li></ul> |
| 🚀 | **Scalability**   | <ul><li>Designed to scale within Windows environments effectively.</li><li>Modular architecture supports scaling up security features as needed.</li><li>Performance optimizations ensure that scalability does not compromise efficiency.</li></ul> |
```

---

##  Project Structure

```sh
└── Wincore/
    ├── Config
    │   ├── DatabaseManager.cs
    │   ├── INIManager.cs
    │   ├── JsonManager.cs
    │   ├── LocalizationManager.cs
    │   ├── ReswManager.cs
    │   ├── ResxManager.cs
    │   └── YamlManager.cs
    ├── LICENSE
    ├── Models
    │   ├── EventHelper.cs
    │   ├── ExceptionManager.cs
    │   ├── LogManager.cs
    │   ├── SizeConverter.cs
    │   └── TextUtils.cs
    ├── README.md
    ├── ReisProduction.ico
    ├── ReisProduction.png
    ├── Security
    │   ├── AesManager.cs
    │   ├── CreditinalManager.cs
    │   ├── HashManager.cs
    │   └── SHAManager.cs
    ├── Services
    │   ├── Bluetooth.cs
    │   ├── Disk.cs
    │   ├── Input.cs
    │   ├── InputConverter.cs
    │   ├── Interop.cs
    │   ├── Keyboard.cs
    │   ├── NativeMethods.cs
    │   ├── Network.cs
    │   └── Pointer.cs
    ├── System
    │   ├── AppInfo.cs
    │   ├── Form
    │   ├── ProcessManager.cs
    │   ├── RegeditManager.cs
    │   ├── ServiceManager.cs
    │   ├── SystemInfo.cs
    │   ├── WinRT
    │   └── WindowManager.cs
    ├── Utilities
    │   ├── Constants.cs
    │   ├── Enums
    │   ├── Interfaces.cs
    │   ├── Records.cs
    │   └── Structs
    ├── Wincore.csproj
    └── Wincore.snk
```


###  Project Index
<details open>
	<summary><b><code>WINCORE/</code></b></summary>
	<details> <!-- __root__ Submodule -->
		<summary><b>__root__</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Wincore.snk'>Wincore.snk</a></b></td>
				<td>- Wincore.snk serves as a security key for the project, ensuring the integrity and authenticity of the assemblies within the codebase<br>- It is crucial for maintaining a trusted environment by verifying that the components have not been altered or tampered with since their original compilation<br>- This file is integral to the security framework of the entire architecture.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Wincore.csproj'>Wincore.csproj</a></b></td>
				<td>- Wincore.csproj configures the build and packaging settings for a .NET application targeting Windows, specifying dependencies, assembly information, and resources<br>- It ensures the application is built with specific system requirements and includes metadata for distribution, such as versioning, authors, and licensing details, enhancing the project's manageability and deployability.</td>
			</tr>
			</table>
		</blockquote>
	</details>
	<details> <!-- Security Submodule -->
		<summary><b>Security</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Security/CreditinalManager.cs'>CreditinalManager.cs</a></b></td>
				<td>- CredentialManager in the Security directory facilitates secure storage, retrieval, and management of credentials using the Windows Credential Manager<br>- It provides methods to save, retrieve, delete, and check the existence of credentials, enhancing security by interfacing directly with Windows' native credential storage mechanisms<br>- Additionally, it includes robust error handling to ensure reliability in credential operations.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Security/HashManager.cs'>HashManager.cs</a></b></td>
				<td>- HashManager in the Security module of ReisProduction.Wincore provides robust cryptographic functionalities for hashing and verifying passwords and files using the PBKDF2 algorithm<br>- It supports customizable iterations, salt, and key sizes to enhance security, and includes methods for generating random salts and keys, alongside synchronous and asynchronous operations for hashing and verification.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Security/SHAManager.cs'>SHAManager.cs</a></b></td>
				<td>- SHAManager serves as a security utility within the ReisProduction.Wincore.Security namespace, providing SHA-256 hashing capabilities for strings, byte arrays, and files<br>- It supports hash computation with optional salting and verification against expected hashes, enhancing data integrity checks across the application.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Security/AesManager.cs'>AesManager.cs</a></b></td>
				<td>- AesManager in the Security directory provides robust AES encryption and decryption functionalities for data, strings, and files<br>- It supports key derivation using SHA256, handles encryption modes and padding, and offers both synchronous and asynchronous operations to secure and retrieve information efficiently within the ReisProduction.Wincore framework.</td>
			</tr>
			</table>
		</blockquote>
	</details>
	<details> <!-- Config Submodule -->
		<summary><b>Config</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Config/ReswManager.cs'>ReswManager.cs</a></b></td>
				<td>- ReswManager serves as a crucial component in the ReisProduction.Wincore.Config namespace, managing the reading, writing, and updating of resource files (.resw)<br>- It facilitates the efficient handling of key-value pairs for localization by loading, modifying, and persisting data, ensuring robust resource management across the application.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Config/LocalizationManager.cs'>LocalizationManager.cs</a></b></td>
				<td>- LocalizationManager serves as a central component within the ReisProduction.Wincore.Config namespace, dedicated to managing localization operations across the application<br>- It ensures that the software can adapt its interface to different languages and regions, enhancing user accessibility and providing a tailored user experience in a global context.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Config/DatabaseManager.cs'>DatabaseManager.cs</a></b></td>
				<td>- DatabaseManager.cs serves as a centralized component within the ReisProduction.Wincore.Config namespace, responsible for managing all database operations<br>- Positioned within the broader project architecture, it ensures efficient interaction with the database, facilitating data handling and operations crucial for the application's performance and reliability across various modules.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Config/YamlManager.cs'>YamlManager.cs</a></b></td>
				<td>- YamlManager serves as a comprehensive utility for managing YAML configurations within the ReisProduction.Wincore project<br>- It facilitates asynchronous loading, saving, and manipulation of YAML files, ensuring data integrity through concurrent access<br>- Key functionalities include reading, writing, and validating YAML content, enhancing configuration management across the software architecture.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Config/ResxManager.cs'>ResxManager.cs</a></b></td>
				<td>- ResxManager in the Config directory serves as a crucial component for managing resource files (.resx) within the ReisProduction.Wincore architecture<br>- It facilitates the loading, saving, and manipulation of resource key-value pairs, ensuring dynamic content management across different locales and configurations, thereby enhancing internationalization and localization efforts within the application.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Config/JsonManager.cs'>JsonManager.cs</a></b></td>
				<td>- JsonManager serves as a crucial component within the ReisProduction.Wincore.Config namespace, facilitating the serialization and deserialization of objects to and from JSON format<br>- It supports both synchronous and asynchronous operations, offering options for indented or compact JSON output, and handles file-based input/output operations to streamline data management across the application.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Config/INIManager.cs'>INIManager.cs</a></b></td>
				<td>- INIManager serves as a utility within the ReisProduction.Wincore.Config namespace, facilitating the management of INI files<br>- It provides functionality to read, write, and delete both keys and sections, and includes methods to verify the existence of these elements, enhancing configuration management across the software's architecture.</td>
			</tr>
			</table>
		</blockquote>
	</details>
	<details> <!-- Models Submodule -->
		<summary><b>Models</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Models/EventHelper.cs'>EventHelper.cs</a></b></td>
				<td>- EventHelper in the Models directory serves as a utility class for extracting various properties from WMI event data, such as process details, service information, and system changes<br>- It simplifies access to event-specific data, enhancing the handling and responsiveness of the system to Windows Management Instrumentation events within the broader application architecture.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Models/LogManager.cs'>LogManager.cs</a></b></td>
				<td>- LogManager in the ReisProduction.Wincore.Models namespace facilitates the creation of log files, essential for tracking application activities and errors<br>- It supports asynchronous logging of general information and exception details into separate files, enhancing the system's diagnostic capabilities by organizing logs at specified paths.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Models/TextUtils.cs'>TextUtils.cs</a></b></td>
				<td>- TextUtils in the ReisProduction.Wincore.Models namespace provides essential string manipulation and localization utilities<br>- It enables localized string retrieval, formatting strings for contextual display, transforming string cases based on culture, and applying keyboard transformations to characters<br>- Additionally, it translates boolean values into localized strings, enhancing user interface adaptability across different languages.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Models/SizeConverter.cs'>SizeConverter.cs</a></b></td>
				<td>- SizeConverter.cs serves as a utility within the ReisProduction.Wincore.Models namespace, providing conversion and formatting functionalities for various measurements including data sizes, data rates, lengths, and weights<br>- It enhances data representation across the application by allowing precise and adaptable formatting options for different units of measurement.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Models/ExceptionManager.cs'>ExceptionManager.cs</a></b></td>
				<td>- ExceptionManager in ReisProduction.Wincore.Models provides centralized exception handling by logging errors, prompting user interaction for email notification, and managing application shutdown processes<br>- It supports direct and fallback email mechanisms, enhancing reliability in error reporting and operational transparency.</td>
			</tr>
			</table>
		</blockquote>
	</details>
	<details> <!-- Services Submodule -->
		<summary><b>Services</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/Network.cs'>Network.cs</a></b></td>
				<td>- Network.cs provides a suite of network-related utility functions essential for managing and diagnosing network configurations within the ReisProduction.Wincore architecture<br>- It facilitates operations such as validating IP addresses, retrieving MAC addresses, managing network interfaces, and conducting network diagnostics through pinging and subnet scanning.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/Input.cs'>Input.cs</a></b></td>
				<td>- Input.cs serves as a utility class within the ReisProduction.Wincore architecture, facilitating interaction with system input devices<br>- It provides methods to send input events, check key states, and determine the activation status of toggling keys like Caps Lock and Num Lock, enhancing the application's ability to handle keyboard and mouse inputs effectively.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/Keyboard.cs'>Keyboard.cs</a></b></td>
				<td>- Keyboard.cs serves as a utility class within the Wincore Services, facilitating the handling of keyboard inputs and actions across the application<br>- It provides methods to check key states, send keyboard events, and simulate user input sequences using both low-level and high-level APIs, enhancing the application's interaction capabilities with various system and window contexts.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/NativeMethods.cs'>NativeMethods.cs</a></b></td>
				<td>- Provides a collection of platform-specific functionalities by interfacing with various system libraries like kernel32, user32, advapi32, and others<br>- It enables direct system calls for process management, UI adjustments, credential handling, and device interactions, crucial for enhancing the application's capabilities to interact seamlessly with the underlying operating system.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/Bluetooth.cs'>Bluetooth.cs</a></b></td>
				<td>- Bluetooth.cs serves as a utility module within the ReisProduction.Wincore.Services namespace, providing functionalities to manage Bluetooth operations<br>- It enables the discovery and connection settings of Bluetooth devices, retrieves radio information, and handles device authentication and service state management<br>- This module is crucial for integrating Bluetooth capabilities into the broader application framework.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/Interop.cs'>Interop.cs</a></b></td>
				<td>- Interop.cs provides essential services for Windows API and process management within the ReisProduction.Wincore framework<br>- It facilitates user interactions through message boxes and manages system operations such as shutdowns, restarts, and their scheduling, leveraging both immediate and delayed execution strategies<br>- Additionally, it supports logging application exit events.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/InputConverter.cs'>InputConverter.cs</a></b></td>
				<td>- The `InputConverter.cs` file within the `ReisProduction.Wincore.Services` namespace plays a crucial role in the codebase by facilitating the conversion between different input key types used within the application<br>- Specifically, it provides utility functions to convert between `VirtualKey` and `WinRTKey` formats, as well as mapping various mouse and navigation inputs to their corresponding virtual key codes.

This file is essential for ensuring that the application can handle and interpret different types of input correctly across various parts of the system, thereby enhancing the interoperability and responsiveness of the user interface components<br>- By abstracting these conversions into a dedicated service, the codebase maintains cleaner architecture and allows for easier maintenance and updates to input handling logic.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/Disk.cs'>Disk.cs</a></b></td>
				<td>- Provides a comprehensive suite of disk utility functions within the ReisProduction.Wincore.Services namespace, enabling the retrieval and analysis of disk information across the system<br>- It facilitates operations such as fetching all disk details, identifying disks by name, size, or free space, and filtering disks based on specific storage criteria.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Services/Pointer.cs'>Pointer.cs</a></b></td>
				<td>- Mouse provides a comprehensive suite of functionalities for simulating and managing mouse interactions within the ReisProduction.Wincore framework<br>- It supports checking button states, sending various mouse events, performing clicks, drags, and scrolls, and managing cursor visibility and position, enhancing the automation capabilities of applications using this architecture.</td>
			</tr>
			</table>
		</blockquote>
	</details>
	<details> <!-- System Submodule -->
		<summary><b>System</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/AppInfo.cs'>AppInfo.cs</a></b></td>
				<td>- AppInfo.cs serves as a utility class within the ReisProduction.Wincore.System namespace, providing centralized access to application-specific information such as version, name, build date, and runtime metadata<br>- It extracts and exposes details from the assembly and file version, enhancing the traceability and manageability of the application throughout its lifecycle.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/ServiceManager.cs'>ServiceManager.cs</a></b></td>
				<td>- ServiceManager in the ReisProduction.Wincore.System namespace provides comprehensive management capabilities for Windows services<br>- It enables checking existence, starting, stopping, restarting, and retrieving details such as description, status, start mode, and executable path of specified services, enhancing control and automation within the system's operational environment.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/RegeditManager.cs'>RegeditManager.cs</a></b></td>
				<td>- RegeditManager in the System directory provides utilities for managing Windows Registry operations, including saving, retrieving, and deleting registry values or subkeys<br>- It supports both direct operations and safer try-catch variants that handle exceptions, ensuring robust interaction with the registry across different parts of the application.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/WindowManager.cs'>WindowManager.cs</a></b></td>
				<td>- WindowManager serves as a utility class within the ReisProduction.Wincore.Services namespace, facilitating comprehensive management of window operations<br>- It provides methods for starting processes, manipulating window visibility, position, size, and state, and integrates closely with ProcessManager to handle process-related queries and actions, enhancing control over application windows within the system.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/SystemInfo.cs'>SystemInfo.cs</a></b></td>
				<td>- SystemInfo.cs serves as a utility class within the ReisProduction.Wincore.System namespace, providing comprehensive access to various system details such as user, domain, machine specifics, operating system, architecture, and memory stats<br>- It enhances the application's ability to fetch and display critical system information, supporting better system diagnostics and user environment management.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/ProcessManager.cs'>ProcessManager.cs</a></b></td>
				<td>- ProcessManager in the ReisProduction.Wincore.Services namespace provides comprehensive management of system processes<br>- It facilitates starting, finding, and managing processes with capabilities like checking running status, administrative rights, and resource usage<br>- Additionally, it supports advanced operations such as retrieving process details, handling process closures, and monitoring CPU usage, enhancing control over system processes within the application architecture.</td>
			</tr>
			</table>
			<details>
				<summary><b>Form</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/Form/ThemeManager.cs'>ThemeManager.cs</a></b></td>
						<td>- ThemeManager.cs provides functionality for managing user interface themes within the ReisProduction.Wincore system<br>- It enables switching between dark and light modes, retrieves the system's accent color, and determines the ideal text color based on background contrast<br>- Additionally, it checks if the system is currently using a dark mode theme.</td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>WinRT</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/System/WinRT/Pointer.cs'>Pointer.cs</a></b></td>
						<td>- Pointer.cs serves as a centralized utility within the ReisProduction.Wincore system, providing streamlined access to pointer device data such as position, type, and interaction states<br>- It abstracts complex pointer interactions, offering methods to retrieve pointer properties, calculate pointer distances, and normalize scroll deltas, enhancing the handling of user input across Windows-based applications.</td>
					</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
	<details> <!-- Utilities Submodule -->
		<summary><b>Utilities</b></summary>
		<blockquote>
			<table>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Interfaces.cs'>Interfaces.cs</a></b></td>
				<td>- Defines a series of interfaces central to the ReisProduction.Wincore.Utilities namespace, facilitating standardized interactions with system processes, windows, disk information, and various input events<br>- These interfaces are crucial for ensuring consistent data handling and functionality across different components of the application, particularly in user and system interactions.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Constants.cs'>Constants.cs</a></b></td>
				<td>- Defines a collection of constants used across the ReisProduction.Wincore application, facilitating standardized interactions with Windows API for tasks like window management, input handling, and system settings<br>- Constants include key codes, window styles, system parameters, and predefined names for scheduled tasks and temporary files, ensuring uniform functionality and integration within the software architecture.</td>
			</tr>
			<tr>
				<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Records.cs'>Records.cs</a></b></td>
				<td>- Defines data structures for system and user interaction information, including process details, window characteristics, disk statistics, and input events like keyboard, mouse, and gamepad actions<br>- These records support the application's ability to interact with and manage system resources, enhancing its functionality in monitoring and control tasks within a Windows environment.</td>
			</tr>
			</table>
			<details>
				<summary><b>Enums</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/WindowAnchor.cs'>WindowAnchor.cs</a></b></td>
						<td>- Defines a set of anchor positions for window placement within the ReisProduction.Wincore application, facilitating UI consistency and customization<br>- The `WindowAnchor` enumeration in the Utilities/Enums directory supports various screen positioning options, enhancing user interface flexibility and adaptability across different modules and components of the software architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/InputType.cs'>InputType.cs</a></b></td>
						<td>- Defines a comprehensive enumeration of input types for the ReisProduction.Wincore application, encompassing a wide range of user interactions from basic mouse and keyboard inputs to specialized commands<br>- This enumeration facilitates the handling and mapping of diverse user inputs across the application, ensuring a responsive and intuitive user interface.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/WindowsMessageType.cs'>WindowsMessageType.cs</a></b></td>
						<td>- Defines a set of enumerated values representing different types of Windows messages, specifically focusing on keyboard interactions such as KeyDown and KeyUp events<br>- Situated within the Utilities/Enums directory of the ReisProduction.Wincore project, it serves as a foundational component for handling keyboard input across the application, ensuring consistent message interpretation throughout the software architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/ScrollType.cs'>ScrollType.cs</a></b></td>
						<td>- Defines an enumeration `ScrollType` within the `ReisProduction.Wincore.Utilities.Enums` namespace, categorizing different types of scroll actions such as left, right, up, and down, along with a default 'None' option<br>- This enumeration is essential for managing scroll-related functionalities across the ReisProduction Wincore application, ensuring consistent handling of user input related to scrolling within the user interface.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/DialogResult.cs'>DialogResult.cs</a></b></td>
						<td>- Defines a set of standardized dialog result options within the ReisProduction.Wincore.Utilities.Enums namespace, facilitating consistent user interaction responses across the application<br>- These enumerated values represent common user actions such as acceptance, cancellation, or retry, ensuring uniform handling and interpretation of dialog outcomes throughout the software's various components.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/ButtonType.cs'>ButtonType.cs</a></b></td>
						<td>- Defines an enumeration within the ReisProduction.Wincore.Utilities.Enums namespace for different types of buttons, such as LeftButton, RightButton, and MiddleButton, among others<br>- This enumeration facilitates the identification and handling of various button inputs across the application, ensuring a standardized approach to user interactions within the broader codebase architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/TextCase.cs'>TextCase.cs</a></b></td>
						<td>- Defines an enumeration within the ReisProduction.Wincore.Utilities.Enums namespace, named TextCase, which specifies different text capitalization styles: Upper, Lower, Sentence, and Title<br>- This enumeration is likely used across the application to standardize text formatting, enhancing UI consistency and improving user experience by providing predefined text case options.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/MessageBoxIcon.cs'>MessageBoxIcon.cs</a></b></td>
						<td>- Defines a set of standardized icons for message boxes within the ReisProduction.Wincore application, facilitating consistent visual cues across the software<br>- The enumeration includes types like Hand, Question, and Exclamation, each mapped to specific hexadecimal values, ensuring uniformity in user interface alerts and prompts throughout the application's various modules.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/KeyEventType.cs'>KeyEventType.cs</a></b></td>
						<td>- Defines a set of constants representing different types of keyboard events within the ReisProduction.Wincore.Utilities.Enums namespace<br>- These constants are crucial for managing keyboard interactions across the application, enabling precise control and handling of key events, such as extended keys, key releases, and Unicode inputs, thereby supporting robust input management in the software's architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/MoveType.cs'>MoveType.cs</a></b></td>
						<td>- Defines a set of enumerated values under the `MoveType` enum, which categorizes different types of mouse navigation actions within the ReisProduction.Wincore application<br>- These actions include basic directional movements, smooth transitions, and precise cursor positioning, enhancing the user interface interaction capabilities across the software's modules.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/WinRTKey.cs'>WinRTKey.cs</a></b></td>
						<td>- Defines an enumeration `WinRTKey` within the `ReisProduction.Wincore.Utilities.Enums` namespace, mapping keyboard keys to their corresponding hexadecimal values for use across the application<br>- This enumeration facilitates the standardized handling and identification of keyboard inputs, ensuring consistent interaction patterns within the software's user interface components.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/MessageBoxButtons.cs'>MessageBoxButtons.cs</a></b></td>
						<td>- Defines a set of enumerated values representing different configurations of buttons that can appear in message boxes within the ReisProduction.Wincore application<br>- These configurations include options like OK, OKCancel, and YesNo, facilitating varied user interactions across the software's user interface components<br>- This enumeration aids in standardizing dialog responses throughout the application.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/MouseEvent.cs'>MouseEvent.cs</a></b></td>
						<td>- Defines a set of mouse event types within the ReisProduction.Wincore.Utilities.Enums namespace, crucial for handling different mouse interactions across the application<br>- These enumerated values facilitate the precise tracking and response to mouse actions such as movements, button presses, and scroll wheel activities, integrating seamlessly with the system's event handling architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Enums/MouseType.cs'>MouseType.cs</a></b></td>
						<td>- Defines a set of enumerations under the `MouseType` enum within the `ReisProduction.Wincore.Utilities.Enums` namespace, categorizing various mouse interactions such as button clicks and scroll directions<br>- These identifiers facilitate the handling and response to different mouse actions within the broader application, enhancing user interface interactions and input management.</td>
					</tr>
					</table>
				</blockquote>
			</details>
			<details>
				<summary><b>Structs</b></summary>
				<blockquote>
					<table>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/NativeCredential.cs'>NativeCredential.cs</a></b></td>
						<td>- Defines a structured representation for storing credential data in a Windows environment, facilitating secure handling and storage of user credentials<br>- The structure includes fields for credential properties such as type, size, and persistence, along with metadata like last modification time and user-related identifiers, ensuring comprehensive management within the system's security framework.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/BLUETOOTH_DEVICE_INFO.cs'>BLUETOOTH_DEVICE_INFO.cs</a></b></td>
						<td>- Defines a structured representation for Bluetooth device information within the ReisProduction.Wincore.Utilities.Structs namespace<br>- It encapsulates device attributes such as address, class, connection status, memory status, authentication status, and name, facilitating the management and interaction with Bluetooth devices across the application.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/INPUTUNION.cs'>INPUTUNION.cs</a></b></td>
						<td>- Defines a union structure INPUTUNION within the Utilities/Structs directory, crucial for handling different types of input in the ReisProduction.Wincore system<br>- It encapsulates variations of input data—mouse, keyboard, and hardware—into a single structure, facilitating streamlined processing and interaction handling across the software's input management system.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/MOUSEINPUT.cs'>MOUSEINPUT.cs</a></b></td>
						<td>- MOUSEINPUT.cs defines a structured representation for mouse input data within the ReisProduction.Wincore.Utilities.Structs namespace<br>- It encapsulates parameters such as mouse coordinates, button data, event flags, and timing information, essential for handling mouse interactions across the software's user interface components<br>- This structure facilitates the precise control and manipulation of mouse events within the application.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/BLUETOOTH_RADIO_INFO.cs'>BLUETOOTH_RADIO_INFO.cs</a></b></td>
						<td>- Defines a structured data representation for Bluetooth radio information within the ReisProduction.Wincore.Utilities.Structs namespace<br>- It encapsulates details such as device size, address, name, class of device, subversion, and manufacturer, facilitating the management and manipulation of Bluetooth device attributes across the software architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/MSLLHOOKSTRUCT.cs'>MSLLHOOKSTRUCT.cs</a></b></td>
						<td>- Defines the MSLLHOOKSTRUCT structure within the ReisProduction.Wincore.Utilities.Structs namespace, crucial for handling low-level mouse input data across the application<br>- It encapsulates mouse coordinates, button data, event flags, timestamp, and additional information, facilitating efficient mouse event processing and interaction within the broader system architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/RECT.cs'>RECT.cs</a></b></td>
						<td>- Defines the `RECT` structure within the ReisProduction.Wincore.Utilities.Structs namespace, providing a way to manage rectangular shapes by specifying coordinates and dimensions<br>- It supports conversions to and from the .NET `Rectangle` type and includes properties for accessing the rectangle's width, height, location, and size, enhancing spatial calculations across the application.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/POINT.cs'>POINT.cs</a></b></td>
						<td>- Defines a POINT structure within the ReisProduction.Wincore.Utilities.Structs namespace, representing a coordinate with X and Y integers<br>- It is designed for seamless integration with external systems requiring structured coordinate data, enhancing the interoperability of the application across different modules and external APIs.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/KEYBDINPUT.cs'>KEYBDINPUT.cs</a></b></td>
						<td>- Defines the KEYBDINPUT structure within the ReisProduction.Wincore.Utilities.Structs namespace, essential for simulating keyboard input in Windows applications<br>- It encapsulates keyboard event data such as virtual key codes, scan codes, flags, timing, and additional information, facilitating the integration and manipulation of keyboard inputs across the software's modules.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/BLUETOOTH_FIND_RADIO_PARAMS.cs'>BLUETOOTH_FIND_RADIO_PARAMS.cs</a></b></td>
						<td>- Defines a structure within the ReisProduction.Wincore.Utilities.Structs namespace to manage Bluetooth radio parameters, specifically the size of the radio structure<br>- This structure is crucial for initializing and managing Bluetooth operations across the application, ensuring that interactions with Bluetooth hardware are handled consistently and efficiently within the system's architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/KBDLLHOOKSTRUCT.cs'>KBDLLHOOKSTRUCT.cs</a></b></td>
						<td>- Defines the KBDLLHOOKSTRUCT structure within the ReisProduction.Wincore.Utilities.Structs namespace, crucial for handling keyboard input across the application<br>- It encapsulates keyboard data such as virtual key codes and event-specific information, integral for monitoring and responding to keyboard events system-wide, enhancing the application's interaction capabilities with the operating system's low-level keyboard hooks.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/INPUT.cs'>INPUT.cs</a></b></td>
						<td>- Defines a structured representation for user input within the ReisProduction.Wincore.Utilities.Structs namespace, crucial for managing input data across the system<br>- The `INPUT` struct, integral to the application's input handling, encapsulates different types of user inputs through the `INPUTUNION`, ensuring seamless interaction within the software's architecture.</td>
					</tr>
					<tr>
						<td><b><a href='https://github.com/ReisProduction/Wincore/blob/master/Utilities/Structs/HARDWAREINPUT.cs'>HARDWAREINPUT.cs</a></b></td>
						<td>- HARDWAREINPUT.cs defines a structure essential for interfacing with hardware devices within the ReisProduction.Wincore.Utilities.Structs namespace<br>- It encapsulates messages sent to hardware components, facilitating communication and control actions through structured data, crucial for the system's interaction with physical devices<br>- This structure supports the broader architecture's ability to handle hardware inputs efficiently.</td>
					</tr>
					</table>
				</blockquote>
			</details>
		</blockquote>
	</details>
</details>

---
##  Getting Started

###  Prerequisites

Before getting started with Wincore, ensure your runtime environment meets the following requirements:

- **Programming Language:** CSharp
- **Package Manager:** Nuget


###  Installation

Install Wincore using one of the following methods:

**Build from source:**

1. Clone the Wincore repository:
```sh
❯ git clone https://github.com/ReisProduction/Wincore
```

2. Navigate to the project directory:
```sh
❯ cd Wincore
```

3. Install the project dependencies:


**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet restore
```




###  Usage
Run Wincore using the following command:
**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet run
```


###  Testing
Run the test suite using the following command:
**Using `nuget`** &nbsp; [<img align="center" src="https://img.shields.io/badge/C%23-239120.svg?style={badge_style}&logo=c-sharp&logoColor=white" />](https://docs.microsoft.com/en-us/dotnet/csharp/)

```sh
❯ dotnet test
```

---

##  Contributing

- **💬 [Join the Discussions](https://github.com/ReisProduction/Wincore/discussions)**: Share your insights, provide feedback, or ask questions.
- **🐛 [Report Issues](https://github.com/ReisProduction/Wincore/issues)**: Submit bugs found or log feature requests for the `Wincore` project.
- **💡 [Submit Pull Requests](https://github.com/ReisProduction/Wincore/blob/main/CONTRIBUTING.md)**: Review open PRs, and submit your own PRs.

<details closed>
<summary>Contributing Guidelines</summary>

1. **Fork the Repository**: Start by forking the project repository to your github account.
2. **Clone Locally**: Clone the forked repository to your local machine using a git client.
   ```sh
   git clone https://github.com/ReisProduction/Wincore
   ```
3. **Create a New Branch**: Always work on a new branch, giving it a descriptive name.
   ```sh
   git checkout -b new-feature-x
   ```
4. **Make Your Changes**: Develop and test your changes locally.
5. **Commit Your Changes**: Commit with a clear message describing your updates.
   ```sh
   git commit -m 'Implemented new feature x.'
   ```
6. **Push to github**: Push the changes to your forked repository.
   ```sh
   git push origin new-feature-x
   ```
7. **Submit a Pull Request**: Create a PR against the original project repository. Clearly describe the changes and their motivations.
8. **Review**: Once your PR is reviewed and approved, it will be merged into the main branch. Congratulations on your contribution!
</details>

<details closed>
<summary>Contributor Graph</summary>
<br>
<p align="left">
   <a href="https://github.com{/ReisProduction/Wincore/}graphs/contributors">
      <img src="https://contrib.rocks/image?repo=ReisProduction/Wincore">
   </a>
</p>
</details>

---

##  License

This project is protected under the [MIT](https://choosealicense.com/licenses/mit) License. For more details, refer to the [LICENSE](https://github.com/ReisProduction/Wincore/blame/main/LICENSE) file.

---
