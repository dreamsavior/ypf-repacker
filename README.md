# ypf-repacker
Repacker tool for YPF (YU-RIS engine) archive 

A fork of **YPF Manager Tool**.
Renamed so the tool doesn't include space. Added more fault tolerance, ect.

## Usage
```shell
Create archive:         
    -c <folders_list> -v <version> [options]
Extract archive:        
    -e <files_list> [options]
Print info:             
    -p <files_list> [options]
```
## Options

|Short arg|Info|
|---|---|
|-c <folders_list>| Pack folders to ypf|
|-e <files_list>| Extract ypf archives|
|-p <files_list>| Print information|
|-v <version>| target YU-RIS engine version|
|-w| Wait for user input before exit|
|-sdc|Skip data integrity validation (Print info only)|

## Example
**Unpacking**
```shell
ypf-repacker.exe -e C:\Some\YU-RIS\pac\ysbin.ypf
```
**Repacking**
```shell
ypf-repacker.exe -c C:\Some\YU-RIS\folder -v 0.479
```