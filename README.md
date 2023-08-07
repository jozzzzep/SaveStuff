![img](https://i.imgur.com/id2iZ7Q.png)

 <p align="center">
        <img src="https://img.shields.io/codefactor/grade/github/jozzzzep/SaveStuff/main">
        <img src="https://img.shields.io/github/languages/code-size/jozzzzep/SaveStuff">
        <img src="https://img.shields.io/github/license/jozzzzep/SaveStuff">
        <img src="https://img.shields.io/github/v/release/jozzzzep/SaveStuff">
</p>
<p align="center">
        <img src="https://img.shields.io/github/followers/jozzzzep?style=social">
        <img src="https://img.shields.io/github/watchers/jozzzzep/SaveStuff?style=social">
        <img src="https://img.shields.io/github/stars/jozzzzep/SaveStuff?style=social">
</p>

The **SavesStuff** system consists of a set of interrelated classes that provide a robust and flexible solution for saving and loading data in Unity projects. This system offers several functionalities and benefits that simplify the process of managing saved data in games and applications.

The project offers a structured and modular approach to data management, promoting code reusability and maintainability. Additionally it provides a seamless integration with Unity's PlayerPrefs and a customizable directory structure for saving data externally, offering a comprehensive solution for managing saved data in Unity projects. Whether it's implementing a simple save system or a complex multi-slot saving system, **SavesStuff** solves the challenges associated with data persistence, allowing developers to focus on creating engaging experiences for players.

### Contents
  - [**Documentation**](#when-to-use-what)
  - [**Examples**](#examples)
  - [**Imporing**](#importing)


![img](image.png)

### When to use what?
---

- [**QuickSaveSystem**](#quick-save-system)  
A static utility class that simplifies data saving and loading tasks, providing methods to save and load objects implementing the IQuickSaveable interface without the need for creating a save system instance.

- [**SlotSaveSystem**](#slotsavesystem)  
A slot-based save system with a fixed number of slots for data storage, allowing developers to save, load, and delete data from specific slots and manage multiple save files efficiently.

- **JsonSaveSystem**  
A save system that uses JSON serialization to save and load data of type T, converting objects to JSON format for storage and deserializing them upon loading, suitable for non-sensitive data storage.

- **EncryptedSaveSystem**  
An extension of SaveSystem<T> that adds support for encrypted data saving and loading, providing an extra layer of security for sensitive data.


---
- **Saved Variables**
  - - **SavedBool**
    - **SavedFloat**
    - **SavedInt**
    - **SavedString**

  - **SavedVariable** - *Base class for every Saved Variable*  
  A generic class for creating saved variables with specific data types (e.g., bool, float, int, string), allowing easy saving and loading of simple data types to and from different storage systems. 
 -------
- **Base Save Systems (Advanced)**

  - **SaveSystem\<T>**  
  The Base SaveSystem of **every save system** in the project.  
  A flexible save system for saving and loading data of type T to and from files,supporting custom data types by implementing the ISaveable interface, and offering properties for managing file paths and events for handling save system events.

  - **ManagedSaveSystem\<T>** - *Extends the Base SaveSystem*  
  A base class for creating managed save systems, allowing developers to extend save system functionality for specific data types by inheriting this class and defining their own internal save system.  

  
---
# Quick Save System
The **QuickSaveSystem** is a static class that provides an easy-to-use save system for saving and loading data in Unity projects without the need for creating save system instances. It offers seamless integration with the SaveStuff system and is designed to work with objects that implement the **IQuickSaveable** interface.

### Contents
- [Documentation](#quick-save-system-documentation)
- [Setup & Usage](#setup-quick-save-system)

## Quick Save System Documentation

### Properties
- **DirectoryPath**  
Gets or sets the directory path where the save system saves and loads files.  
If not set, it defaults to a directory named **"general"** generated using **PathGenerator.GeneratePathDirectory**.

- **FilesPrefix**  
Gets or sets the prefix of every file created with the save system.  
If not set, it defaults to "generalData".

### Methods
- **Save\<T>(T savableObj)**  
Saves the given object using the appropriate saving method based on the object's SavingMethod property (either JSON serialization or encrypted data storage).

- **Load\<T>(T savableObj)**  
Loads the object's data from a file using the appropriate saving method based on the object's SavingMethod property.

- **Delete\<T>(T savableObj)**  
Deletes the saved file associated with the given object.

- **LoadIfExist\<T>(T savableObj)**  
Loads the object's data from a file only if the file exists, otherwise returns null.

- **FileExists\<T>(T savableObj)**  
Checks if there is an existing saved file for a given object.

- **GeneratePath(string fileName, string fileType)**  
Generates a file path based on the given file - name and file type (extension).

- **GeneratePath\<T>(T savableObj)**  
Generates a file path for the given IQuickSaveable object based on - its Name and SavingMethod.

- **GetFileType\<T>(T savableObj)**  
Returns the file type (extension) for a given IQuickSaveable object based - on its SavingMethod.

- **InitializeObject\<T>(T defaultData)**  
If there is an existing save for the object, returns the loaded object data from the file. otherwise, returns the given default value. 

---

### Setup Quick Save System
To use the QuickSaveSystem to save and load your custom objects, you need to set up your object to implement the IQuickSaveable interface. This allows the QuickSaveSystem to properly serialize and deserialize your object's data. Here's how you can set up a QuickSavable object:

- **Step 1: Implement the IQuickSaveable Interface**  
  First, make sure your custom object's class implements the IQuickSaveable interface. The IQuickSaveable interface requires your class to have a property called SavingMethod, which indicates the saving method to be used (either SavingMethod.Encrypted or SavingMethod.Json).  
  In addition, you need to choose a file name, by implementing the Name property of the interface.
  And lastly, add a LastUsage property of type DateTime.

  ```csharp
  using SavesStuff;
  using System;

  [Serializable]
  public class SaveFile : IQuickSaveable
  {
      public string textContent;

      public SavingMethod SavingMethod => SavingMethod.Json;
      public string Name => "saveFileName";
      public DateTime LastUsage { get; set; }

      public SaveFile(string textContent)
      {
          this.textContent = textContent;
      }
  }
  ```
- **Step 2: Save and Load Your Object**  
  Now that your custom object implements the IQuickSaveable interface, you can easily save and load its data using the QuickSaveSystem methods. Remember to use the appropriate methods from QuickSaveSystem based on your saving method (Json or Encrypted).

  ```csharp
  using UnityEngine;
  using SavesStuff;

  public class Script : MonoBehaviour
  {
      // implement a default object at the start
      SaveFile saveFile = new SaveFile("");

      private void Awake()
      {
          // initialize the object in the system in the Awake method
          saveFile = QuickSaveSystem.InitializeObject(saveFile);
      }

      public void LoadData()
      {
          saveFile = QuickSaveSystem.Load(saveFile);
          // use data and dot whit it what you want
      }

      public void SaveData()
      {
          // do stuff for data before saving
          QuickSaveSystem.Save(saveFile);
      }
  }
  ```

# SlotSaveSystem
The **SlotSaveSystem** class is a specialized save system that manages a fixed number of save slots. It is designed to work with objects that inherit from the **SaveSlot** class and provides functionalities to save, load, and delete data in these slots.

### Content
- [Documentation](#slot-save-system-documentation)
- [Setup and Example](#setup-and-examples-for-slot-save-system)

## Slot Save System Documentation
### Properties

- **SlotsAmount**  
Gets the maximum number of slots in the save system.
- **FileType**  
The file type of the saveable files.
- **DirectoryPath**  
The directory path the save system saves to and loads from.
- **FilesPrefix**  
The prefix of every file created with the save system.
- **Events**  
Events of the save system.
- **SlotEvents**  
Events of the slot save system.

- **InternalSaveSystem**  
The internal save system used for saving and loading data.

### Methods
- **Save (T toSave)**  
Saves an object (toSave) to a save slot.
- **Load (int slot)**  
Loads an object from the specified slot index.
- **Delete (int slot)**  
Deletes the data saved in a slot.
- **SlotIsEmpty (int slot)**  
Checks if a slot is currently not storing any data.
- **LoadIfContatinsData (int slot)**  
Loads the data from a slot only if the slot contains data.
- **GetSlotsState()**  
Checks which slots are currently empty and which are not, returning a boolean array representing the slot states.
- **LoadSlots()**  
Loads all slots and returns an array with the objects they store or null in empty slots.
- **LoadSlots (bool[] slotStates)**  
Loads all slots based on the provided slot states and returns an array with the objects they store or null in empty slots.
- **EmptySlotIndex()**  
Returns the index of the first empty slot or null if there are no empty slots.

- **IndexToPath (int slot)**  
Takes a slot index and returns a file path.

### Static Methods

- **EmptySlotIndex (bool[] slotsState)**  
Takes an array of slot states and returns the index of the first empty slot or null if there are no empty slots.

- **IndexToFileName (int slot)**  
Takes a slot index and returns a file name corresponding to that slot.

### Constructor
- **SlotSaveSystem(int slotsAmount, SaveSystem<T> internalSaveSystem)**   
  Constructs a SlotSaveSystem instance with the specified number of slots and an internal save system (internalSaveSystem) used for saving the slot data. It automatically loads the slots when initialized.

### Events
You have two event classes for the slot save system. The one named Events is the base event class from the parent class ManagedSaveSystem, it contains generic SaveSystem events. And the one named SlotEvents is similar to the previous but also contain data about the loaded/saved slots when events are raised.
- Events
  - **FilesUpdated**  
  This event is raised when saving or deleting.  
  It indicates that the files in the save system have been updated.
  - **Saved**  
  This event is raised when an object is saved  
  It provides the saved object of type T as the event argument.

  - **Loaded**  
  This event is raised after loading an object.  
  It provides the loaded object of type T as the event argument.
  - **Deleted**  
  This event is raised after the deletion of a saved file.  
  It provides the file path (of type string) of the deleted file as the event argument.

- SlotEvents
  - **FilesUpdated**  
  This event is raised when saving or deleting slots.  
  It indicates that the files in the slot save system have been updated.

  - **Saved**  
  This event is raised when a slot has been saved to.  
  It provides the saved object of type T as the event argument.
  - **Loaded**  
  This event is raised when a slot has been loaded.  
  It provides the loaded object of type T as the event argument.
  - **Deleted**  
  This event is raised after a slot is deleted.  
  It provides the slot index (of type int) as the event argument.

## Setup and Examples for Slot Save System
### Step 1 - Create a Sample Saveable Class.  
We need a class that inherits from **SaveSlot** and represents the data you want to save. 
  ```csharp
  using System;
  using SavesStuff;

  [Serializable]
  public class PlayerData : SaveSlot
  {
      public string playerName;
      public int playerScore;

      public PlayerData(int slotIndex, string name, int score) : base(slotIndex)
      {
          playerName = name;
          playerScore = score;
      }
  }
  ```
### Step 2 - Instantiate and Utilize the SlotSaveSystem in Unity 
This is pretty self-explanatory, just read the comments:
```csharp
using UnityEngine;
using SavesStuff;

public class SaveSystemDemo : MonoBehaviour
{
    private SlotSaveSystem<PlayerData> _saveSystem;

    private void Start()
    {
        // Setup the save system.
        // This is a dummy setup and assumes
        // that the SaveSystem<PlayerData> has a parameterless constructor.
        SaveSystem<PlayerData> internalSystem = new SaveSystem<PlayerData>();
        
        // Choosing to use 5 slots
        _saveSystem = new SlotSaveSystem<PlayerData>(5, internalSystem); 

        // Subscribe to events
        _saveSystem.SlotEvents.Saved += OnSaved;
        _saveSystem.SlotEvents.Loaded += OnLoaded;
        _saveSystem.SlotEvents.Deleted += OnDeleted;
    }

    // ----- Events called functions ----

    private void OnSaved(PlayerData data)
    {
        Debug.Log($"Saved data for player: {data.playerName} with score: {data.playerScore} at slot: {data.SlotIndex}");
    }

    private void OnLoaded(PlayerData data)
    {
        Debug.Log($"Loaded data for player: {data.playerName} with score: {data.playerScore} from slot: {data.SlotIndex}");
    }

    private void OnDeleted(int slot)
    {
        Debug.Log($"Deleted data from slot: {slot}");
    }

    // ----------------------------------

    public void SavePlayerData(string name, int score)
    {
        // returns the first available slot, if there is none - returns null
        int? emptySlot = _saveSystem.EmptySlotIndex();

        // Check if there is an available save slot
        if (emptySlot.HasValue)
        {
            PlayerData player = new PlayerData(emptySlot.Value, name, score);
            _saveSystem.Save(player);
        }
        else
        {
            Debug.LogWarning("All save slots are occupied!");
        }
    }

    public void LoadPlayerData(int slot)
    {
        if (!_saveSystem.SlotIsEmpty(slot))
        {
            PlayerData loadedData = _saveSystem.Load(slot);
            // Do something with the loaded data if required
        }
        else
        {
            Debug.LogWarning($"Slot {slot} is empty!");
        }
    }

    public void DeletePlayerData(int slot)
    {
        _saveSystem.Delete(slot);
    }
}
```

### Step 3 - Unity Integration

1. Attach the **SaveSystemDemo** script to an empty **GameObject**.

2. You can now call the **SavePlayerData**, **LoadPlayerData**, and **DeletePlayerData** functions based on user input or other game logic.
3. To demonstrate, you could have UI elements (like buttons) that trigger these functions.

# JsonSaveSystem
**JsonSaveSystem** is a save system, derived from the base **SaveSystem**, designed to serialize saveable objects to JSON format and store them as files. This system provides various utilities for saving, loading, and managing these saved files. As it inherits from **SaveSystem**, it benefits from a collection of methods and functionalities that are consistent across save systems.

### Content
- [Documentation](#documentation---json-save-system)
- [Setup and Example](#setup-and-example---json-save-system)

## Documentation - Json Save System 
### Properties
- FileType
Description: Represents the file type of the savable files. In the context of JsonSaveSystem<T>, this value is always "json".
- DirectoryPath
Description: The directory path where the save system saves and retrieves data from. This property is inherited from SaveSystem<T>.
- FilesPrefix
Description: The prefix applied to every file created with the save system. This property is inherited from SaveSystem<T>.
- Events
Description: Represents various events related to the save system's operations. This property is inherited from SaveSystem<T>.

## Setup and Example - Json Save System 

# Examples

# Importing