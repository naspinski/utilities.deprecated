{{
Utilities class for .Net including Dynamic property getters/setters, automatic IQueryable searching, LinqToSql shortcuts, FileStream shortcuts, String extensions and more.
}}
## [Naspinski.Utilities is now available via NuGet!](http://nuget.org/packages/Naspinski.Utilities)
**PM> Install-Package Naspinski.Utilities**

## DynamicProperty
**Change a property value at run time without knowing the property ahead of time**
* **someObject.SetPropertyValue("Name", "new value")** is the same as _someObject.Name = "new value";_ - no need to know which property to code in

**More Information:**
* [http://naspinski.net/post/2010/06/01/Edit-an-Object-Property-Value-Dynamically-at-Runtime](http://naspinski.net/post/2010/06/01/Edit-an-Object-Property-Value-Dynamically-at-Runtime)

## IQueryableSearch
**Search all/any properties of an IQueryable with one single search.**

The 'quick search' is a great tool. Google has shown us that searching with one single, universal search field is the way that people prefer to search. Anyone who has worked with Linq for any amount of time knows this is possible, but it requires a lot of hard-coding and a long jumble of 'where' statements. This class will allow you to run a universal 'google-like' search on any IQueryable.

**More Information:**
* [http://naspinski.net/post/Universal-IQueryable-Search-Version-2-with-Reflection.aspx](http://naspinski.net/post/Universal-IQueryable-Search-Version-2-with-Reflection.aspx)
* [http://naspinski.net/post/Universal-IQueryable-Search-Usable-by-Linq-to-SQL-and-Linq-to-Entities.aspx](http://naspinski.net/post/Universal-IQueryable-Search-Usable-by-Linq-to-SQL-and-Linq-to-Entities.aspx)

## LinqToSql
**Universal _Get_ Extensions for your DataContexts, Find the Primary Key of any table, and more**

* **GetPrimaryKey** will get the primary key of any Linq-to-SQL table
	* _Naspinski.Utilities.GetPrimaryKey<table>()_ will return the _PropertyInfo_ of the Primary Key(s) of 'table'
* **someDataContext.Get** is a generic _Get_ accessor for all of your tables
	* _someDataContext.Get<Car>(someKey)_ is the same as writing _someDataContext.Cars.FirstOrDefault(x => x.id == someKey)_ regardless of what Type someKey is or what the PropertyInfo.Name of the Primary Key is; never write a Get accessor again!

## FileStreamSave
**Simple extension to save a _FileStream_ to disk, option to avoid overwriting will automatically update the filename to avoid overwriting**
* **someFileStream.Save(@"C:\file.txt")** will save the file to the given path, or save as 'file{"[1](1)"}.txt' if the file is already there (file{"[2](2)"}.txt if that is there and so on); the file name will be returned
* **someFileStream.Save(@"C:\file.txt", false)** will save the file to the given path, overwriting if the file already exists

## StringConversions
**Convert strings to things you often need to convert them to... easily**
* **RemoveCharacters** removes any instances of a character array
* **Strings.Random** creates a random string with lower and upper limits of special characters
* **To** turns a string into any Type
* **ToEnum** turns a string into an Enum... simple!
* **ToNullable** turns a string into any Nullable Type you want