# Documentations

# Advanced Classes


## Save Systems

- **JsonSaveSystem**


- **EncryptedSaveSystem**

- **QuickSaveSystem**  
  Static save system class for quick basic usage.  
  Allows saving and loading objects without the need for a save system instance.  
  This class takes the objects taht implemet the interface IQuickSaveable

- **SlotSaveSystem**  
Save system that manages your saves in slots.  
If you want to implement a save sysytem with constant amount of slots.

- [**SaveSystem**](#savesystem-class)  
    The base class of all save systems.  
    **Do not use directly!** Use the Save Systems above instead.  
    Use this class only with inherience for creating other save systems

# SaveSystem class
[BACK TO MENU](#documentations)  
The base class of all save systems.  
Don't use this class directly, use one of [these](#save-systems) save system classes instead.

- Properties
  - **FileType**  
  The file type of the saveable files - Always "json"  
    
- Methods
  - **Save(obj)**  
  Saves an object to a file
  Serializes its contents to JSON format