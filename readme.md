# Keyboard Language Changer

---

Simple app that will change active keyboard layout in Windows based on CLI arguments. 
Use case: run it with keyboard macro to bind specific key to layout.

# Installation

Just download or build `KeyblangChanger.exe` and you're good to go.

# Usage
Run application without arguments to get current layout id.

You can check all supported cultures (but not all layouts!) here: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c?redirectedfrom=MSDN

## CLI arguments

### set layout by culture name

`keyblangchanger.exe set en-us`

### set layout by layout id

`keyblangchanger.exe set 4090409`

> NOTE: if you set known layout that is not installed, you'll have to install this language and remove it to get rid of additional layout
 
# Licence
MIT
