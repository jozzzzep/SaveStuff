### ;
![img](https://i.imgur.com/swFyjTR.png)

# Save Systems

- **JsonSaveSystem**

- **EncryptedSaveSystem**

- **QuickSaveSystem**

- **SlotSaveSystem**

- [**SaveSystem**](#savesystem-class)  
    The base class of all save systems.  
    **Do not use directly!** Use the Save Systems above instead.  
    Use this class only with inherience for creating other save systems

# SaveSystem class
[BACK TO MENU](#)  
The base class of all save systems.  
Don't use this class directly, use one of [these](#save-systems) save system classes instead.

- Properties
  - **FileType**  
  The file type of the saveable files - Always "json"  
    
- Methods
  - **Save(obj)**  
  Saves an object to a file
  Serializes its contents to JSON format