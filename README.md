# Uma Club Fan Scraper

Uma Club Fan Scraper, or UCFS for short, is a tool I am actively developing to improve QoL for leaders of my club, by scraping data from sources provided by them and returning a list of fans for the members of their respective clubs.

## Prerequisites:
This tool requires you to have .NET 8.0 Runtime installed on your computer.

You can install it from here: [.NET 8.0 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-8.0.25-windows-x64-installer?cid=getdotnetcore)

## How to set up scraping:
The following instruction is written for v.0.1.2.

Before Running the tool. Please make sure you have updated the following spots in the "Sources" folder.

### CircleIds.json
The following file is a description of clubs the tool will attempt to scrape for.
Each club described takes a name - for the output text file name, and a circle_id - the id of a described club.

An example of a properly setup CircleIds.json file looks as follows:

<img width="330" height="231" alt="image" src="https://github.com/user-attachments/assets/80db2e72-90a1-48b8-8f7e-5955b4ec000b" />

### Clubs Folder
In the clubs folder, ensure you have a text file of the same name that you have given to the club in the circleIds.json file.
Inside this text file, please ensure you have the names of the players you wish to scrape data for.

**The order of names in the text file is _EXTREMELY_ important, as it determines the order of the fans in the output folder**

A properly set up text file for a club has the following characteristics:
1. The file name resembles the name parameter in the circleIds.json
2. The file consists of just names with no other separators aside from each name being in a separate line.

The following is an example based on the **EXAMPLE 1** club from the circleIds.json image above:

<img width="345" height="197" alt="image" src="https://github.com/user-attachments/assets/2d617a77-266f-4116-8f5f-672a42d2fd28" />
