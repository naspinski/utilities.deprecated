# Naspinski.Utilities

Utilities class for .Net including Dynamic property getters/setters, automatic IQueryable searching, LinqToSql shortcuts, FileStream shortcuts, String extensions and more.

## Getting Started
Naspinski.Utilities is now available via [NuGet](http://nuget.org/packages/Naspinski.Utilities)
Open your Package Manager and search for naspinski.utilities or type the following in the command line:
```
 PM> Install-Package Naspinski.Utilities
```

## Utilities

### DynamicProperty
Change a property value at run time without knowing the property ahead of time

### IQueryableSearch
Search all/any properties of an IQueryable with one single search.

The 'quick search' is a great tool. Google has shown us that searching with one single, universal search field is the way that people prefer to search. Anyone who has worked with Linq for any amount of time knows this is possible, but it requires a lot of hard-coding and a long jumble of 'where' statements. This class will allow you to run a universal 'google-like' search on any IQueryable.

### LinqToSql
Universal Get Extensions for your DataContexts, Find the Primary Key of any table, and more

### FileStreamSave
Simple extension to save a FileStream to disk, option to avoid overwriting will automatically update the filename to avoid overwriting

### StringConversions
Convert strings to things you often need to convert them to... easily
* *RemoveCharacters* removes any instances of a character array
* *Strings.Random* creates a random string with lower and upper limits of special characters
* *To* turns a string into any Type
* *ToEnum* turns a string into an Enum... simple!
* *ToNullable* turns a string into any Nullable Type you want
