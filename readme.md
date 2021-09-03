# Keyboard Language Changer

---

Simple app that will change active keyboard layout in Windows based on CLI arguments. 
Use case: run it with keyboard macro to bind specific key to layout.

# Installation

Just download or build `KeyblangChanger.exe` and you're good to go.

# Usage
Run application without arguments to get current layout id.

You can check all supported cultures (but not all layouts!) here: https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-lcid/a9eac961-e77d-41a6-90a5-ce1a8b0cdb9c?redirectedfrom=MSDN

> NOTE: if you set known layout that is not installed, you'll have to install this language and remove it to get rid of additional layout

## Change layout for applications launched with Admin privileges

Unfortunately, when KeyblangChanger doesn't have admin privileges, it cannot change layout for applications that does. And if you'll require admin on launch, you will have to confirm this using prompt (unless you want to disable UAC and you don't!). You can work around this by launcher application (like Logitech G-HUB) as administrator. Or you can use proposed hacky solution with Task Scheduler tasks.

Using Task Scheduler you can run application from specific user without prompt. And then you can trigger task without admin privileges.

Application supports running tasks with `Keyblangchanger_<layout>` naming convention. Creation of these tasks can be automated using `UpdateSchedulerTasks.ps1` script. It will remove all existing tasks with this naming convention and create new ones per layout.

## CLI arguments

### set layout by culture name

`keyblangchanger.exe set en-us`

### set layout by layout id

`keyblangchanger.exe set 4090409`

### set layout by scheduler task

`keyblangchanger.exe task en-us`
 
# Licence
MIT
