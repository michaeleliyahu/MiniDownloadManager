\# MiniDownloadManager



MiniDownloadManager is a lightweight Windows Forms application written in C# that allows users to download files from a remote server.  

The app fetches a JSON list of available files, displays the highest rated file’s details, and lets the user download and run the selected file easily.



---



\## Requirements



\- Windows 10 or later  

\- .NET Runtime (if not published as self-contained)  

\- Internet connection to fetch the file list and download files



---



\## How to Run



1\. Run the `MiniDownloadManager.exe` found in the publish folder.  

2\. The app will automatically fetch the list of files from the server.  

3\. Select a file from the list and start the download.  

4\. Track download progress with the progress bar.



---



\## External Libraries



\- Newtonsoft.Json (for JSON parsing)  

\- System.Net.Http (for downloading files over the network)  

\- WinForms (user interface)



---



\## Folder Structure (brief)





