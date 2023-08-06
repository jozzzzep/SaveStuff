![img](https://i.imgur.com/id2iZ7Q.png)

 <p align="center">
        <img src="https://img.shields.io/codefactor/grade/github/jozzzzep/SaveStuff/main">
        <img src="https://img.shields.io/github/languages/code-size/jozzzzep/SaveStuff">
        <img src="https://img.shields.io/github/license/jozzzzep/SaveStuff">
        <img src="https://img.shields.io/github/v/release/jozzzzep/SaveStuff">
</p>
<p align="center">
        <img src="https://img.shields.io/github/followers/jozzzzep?style=social">
        <img src="https://img.shields.io/github/watchers/jozzzzep/AudUnity?style=social">
        <img src="https://img.shields.io/github/stars/jozzzzep/AudUnity?style=social">
</p>

The **SavesStuff** system consists of a set of interrelated classes that provide a robust and flexible solution for saving and loading data in Unity projects. This system offers several functionalities and benefits that simplify the process of managing saved data in games and applications.

The project offers a structured and modular approach to data management, promoting code reusability and maintainability. It provides a seamless integration with Unity's PlayerPrefs and a customizable directory structure for saving data externally, offering a comprehensive solution for managing saved data in Unity projects. Whether it's implementing a simple save system or a complex multi-slot saving system, "SavesStuff" solves the challenges associated with data persistence, allowing developers to focus on creating engaging experiences for players.


# Documentation

# Save Systems by usage

- **QuickSaveSystem**  
A static utility class that simplifies data saving and loading tasks, providing methods to save and load objects implementing the IQuickSaveable interface without the need for creating a save system instance.

- **SlotSaveSystem\<T>**  
A slot-based save system with a fixed number of slots for data storage, allowing developers to save, load, and delete data from specific slots and manage multiple save files efficiently.


- **JsonSaveSystem\<T>**  
A save system that uses JSON serialization to save and load data of type T, converting objects to JSON format for storage and deserializing them upon loading, suitable for non-sensitive data storage.

- **EncryptedSaveSystem\<T>**  
An extension of SaveSystem<T> that adds support for encrypted data saving and loading, providing an extra layer of security for sensitive data.


---
- **Saved Variables**
  - - **SavedBool**
    - **SavedFloat**
    - **SavedInt**
    - **SavedString**

  - **SavedVariable** - *Base class for every Saved Variable*  
  A generic class for creating saved variables with specific data types (e.g., bool, float, int, string), allowing easy saving and loading of simple data types to and from different storage systems. 
 ----
- **Base Save Systems (Advanced)**

  - **SaveSystem\<T>**  
  The Base SaveSystem of **every save system** in the project.  
  A flexible save system for saving and loading data of type T to and from files,supporting custom data types by implementing the ISaveable interface, and offering properties for managing file paths and events for handling save system events.

  - **ManagedSaveSystem\<T>** - *Extends the Base SaveSystem*  
  A base class for creating managed save systems, allowing developers to extend save system functionality for specific data types by inheriting this class and defining their own internal save system.
