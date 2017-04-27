# Naspinski.Utilities

Utilities class for .Net including Dynamic property getters/setters, automatic IQueryable searching, LinqToSql shortcuts, FileStream shortcuts, String extensions and more.

## Getting Started
Naspinski.Utilities is now available via [NuGet](http://nuget.org/packages/Naspinski.Utilities)
Open your Package Manager and search for naspinski.utilities or type the following in the command line:
```
 PM> Install-Package Naspinski.Utilities
```

## Utilities

### [DynamicProperty](https://github.com/naspinski/utilities/blob/master/README.md#dynamicproperty-1)
Change a property value at run time without knowing the property ahead of time

### [EnumUtilities](https://github.com/naspinski/utilities/blob/master/README.md#enumutilities)
Enumerate through enums easily

### [FileStreamSave](https://github.com/naspinski/utilities/blob/master/README.md#filestreamsave-1)
Simple extension to save a FileStream to disk, option to avoid overwriting will automatically update the filename to avoid overwriting

### [StringConversions](https://github.com/naspinski/utilities/blob/master/README.md#stringconversions-1)
Convert strings to things you often need to convert them to... easily

### IQueryableSearch
Search all/any properties of an IQueryable with one single search.

The 'quick search' is a great tool. Google has shown us that searching with one single, universal search field is the way that people prefer to search. Anyone who has worked with Linq for any amount of time knows this is possible, but it requires a lot of hard-coding and a long jumble of 'where' statements. This class will allow you to run a universal 'google-like' search on any IQueryable.


### LinqToSql [Deprecated]
Universal Get Extensions for your DataContexts, Find the Primary Key of any table, and more


* *RemoveCharacters* removes any instances of a character array
* *Strings.Random* creates a random string with lower and upper limits of special characters
* *To* turns a string into any Type
* *ToEnum* turns a string into an Enum... simple!
* *ToNullable* turns a string into any Nullable Type you want

# Documentation
## DynamicProperty
Change a Property value at run-time without knowing which Property at compile-time

### SetPropertyValue(string, object)
```
void SetPropertyValue(string propertyName, object newValue)
```
*Details:*
* Change the Property *propertyName* of a given Object to the value *newValue*
* Will throw an Exception if the Property is not present
* Will throw an Exception if the Property does not allow writes
*Usage:*
```
someObject.SetPropertyValue("Admin", false);
someObject.SetPropertyValue("Name", "Stan");
```

### GetPropertyValue&lt;T&gt;(string)
```
void GetPropertyValue<T>(string propertyName)
```
*Details:*
* Get the Property *propertyName* of a given Object and cast to T
* Will throw an Exception if the Property is not present
*Usage:*
```
bool isAdmin = someObject.GetPropertyValue<bool>("IsAdmin");
string name = someObject.GetPropertyValue<string>("Name");
```
### GetPropertyValue(string)
```
void GetPropertyValue(string propertyName)
```
*Details:*
* Get the Property *propertyName* of a given Object as an Object
* Will throw an Exception if the Property is not present
*Usage:*
```
object isAdmin = someObject.GetPropertyValue("IsAdmin");
object name = someObject.GetPropertyValue("Name");
```

## EnumUtilities
Simple Enum utilities

### GetValues&lt;T&gt;()

*Details:*
* Gets a collection of enums (useful for looping)
* ```T : enum```

*Usage:*
```
enum Tester { Test = 1, Bla = 5 }

// loop through them
foreach(var e in EnumUtilities.GetValues<Tester>())
    System.PrintLine(e.ToString() + " = " + (int)e);
```

## FileStreamSave
Extension for simply saving a FileStream

### Save()
```
string Save(string path, (optional)bool overwrite)
```

*Details:*
* Saves FileStream to *path* then returns the saved file name
* *overwrite* defaults to **false** if it is not specified
* if *overwrite* is **false** or omitted and the file already exists, it will append a counter to the file name; **file.txt**, **file[1].txt**, **file[2].txt**, etc.
* if *overwrite* is **true** it will overwrite any existing files
* if the folder you are trying to write to does not exist, it will be automatically made

*Usage:*
```
//getting a FileStream
FileStream stream = File.OpenRead(@"C:\file.txt");

//save it to a new directory
string fileName = stream.Save(@"C:\new_directory\file.txt");
//fileName now equals 'file.txt'

//save it again
string fileName = stream.Save(@"C:\new_directory\file.txt");
//fileName now equals 'file[1].txt' as overwrite defaults to false
//now there are 2 files in that folder: file.txt & file[1].txt

//overwrite the original file
stream.Save(@"C:\new_directory\file.txt", true);
```

## [StringConversions](https://github.com/naspinski/utilities/blob/master/README.md#stringconversions-1)
Turns string into other useful stuff without all the code

### RemoveCharacters()
```
string RemoveCharacters(char[] characters)
```

*Details:*
* Removes all instances of provided characters from the string

*Usage:*
```
string s = "abcdefgaaaaa";

//remove some characters
s = s.RemoveCharacters(new[] { 'a', 'b' });

// s is now "cdefg" 
```
 
### SplitCamelCase()

```
string SplitCamelCase()
```

*Details:*
* Splits a CamelCase string into a human readable string
* Deals with Acronyms pretty well

*Usage:*
```
string s = "StanRulesTheUSA";
string sReadable = s.SplitCamelCase();

// sReadable is now "Stan Rules The USA"
```

### ToEnum&lt;T&gt;()
```
T ToEnum<T>()
```

*Details:*
* Attempts to convert input string into an enum of Type *T*
* If string is not a match for the enum *T*, a *ArgumentException* is thrown
* If conversion fails, it will try to capitalize the fist letter to go with .Net naming conventions, i.e.: "abc" -> "Abc"

*Usage:*
```
//showing what the enum looks like:
public enum Cars { "Corvette", "Pinto" };

//convert a string to the enum
Cars car = "Corvette".ToEnum<Cars>();

Debug.Assert(car == Cars.Corvette);
```

### Strings.Random(int, *int)

```
string Strings.Random(int length, *int minSpecialCharacters)
```

*Details:*
* Creates a random string
* if *minSpecialCharacters* is not specified, it will produce a random string that is *alphanumeric only*

*Usage:*
```
string s = Strings.Random(10);
// s = mPbsKC968r

s = Strings.Random(10, 3);
// s = 4(8VU_GyS|
// or more special characters like: >=irRG+1a!
```

### ToEnum&lt;T&gt;()
```
T ToEnum<T>()
```

*Details:*
* Attempts to convert input string into an enum of Type *T*
* If string is not a match for the enum *T*, a *ArgumentException* is thrown
* If conversion fails, it will try to capitalize the fist letter to go with .Net naming conventions, i.e.: "abc" -> "Abc"

*Usage:*
```
//showing what the enum looks like:
public enum Cars { "Corvette", "Pinto" };

//convert a string to the enum
Cars car = "Corvette".ToEnum<Cars>();

Debug.Assert(car == Cars.Corvette);
```
### To&lt;T&gt;()
```
T To<T>()
```

*Details:*
* Attempts to convert input into a Type *T*
* *{{ T : struct }}*

*Usage:*
```
double eight = "8".To<double>();
```

### ToNullable&lt;T&gt;()
```
Nullable<T> ToNullable<T>()
```

*Details:*
* Attempts to convert input into a nullable version of Type *T*
* *{{ T : struct }}*

*Usage:*
```
int? eight = "8".ToNullable<int>();
decimal? twelve = "12".ToNullable<decimal>();
double? nullDouble = "".ToNullable<double>(); //outputs null
```

## IQueryableSearch
Versatile 'Search-All' Extension for IQueryable objects


### Search()

*Notes about all overloads*
* When searching strings, the search method uses *Contains()* and not *Equals()* unless specified (searching 'bc' would return true for 'abcd')
* If *{"typesToExplore"}* is not specified, the search will only search the immediate object and will not traverse through embedded properties
* If *keywords* is of Type *object* it can take in any object for search, and will only be compared against properties of their Type, ie: if you search for string "3" and there is a property with value of int '3', it will not be returned

### Overload
```
IQueryable Search(object[] keywords)
```

*Details:*
* Search an IQueryable's properties for the *keywords* (objects)
* *keywords* can be of any Type, will only compare against properties that match each keyword's Type
* this does not iterate through levels of an object

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new object[] { "Leather", "2-Door" });

//build keywords for a more advanced search, against different multiple Types in an object
object[] keywords = new object[] { 1, 2, 3, new Engine() { Cylinders = 8 }, "Leather" };
var results = Cars.Search(keywords);
```

### Overload
```
IQueryable Search(object[] keywords, Type[] typesToExplore)
```
*Details:*
* Search an IQueryable's properties for the *keywords* (objects)
* *keywords* can be of any Type, will only compare against properties that match each keyword's Type
* this iterates down through any objects specified in *{"types_to_explore"}*

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new object[] { "Leather", "2-Door" }, new Type[] { typeof(Engine) });

//build keywords for a more advanced search, against different multiple Types in an object
object[] keywords = new object { "V8", 350 };
Type[] explore = new Type { typeof(Engine), typeof(Size) };
var results = Cars.Search(keywords, explore);

//this will search each Car for the string 'V8' and int '350' as well as each Car.Engine and Car.Engine.Size for 'V8' and '350' as well
//any combination that might be included of these types will be searched, ie: Car.Size.Engine.Size, Car.Engine.Size.Size if they were to exist
```

### Overload
```
IQueryable Search(string[] properties_to_search, object[] keywords)
```

*Details:*
* Search an IQueryable's properties for the *keywords* (objects), _only_ including the properties specified in *{"propertiesToSearch"}*
* *keywords* can be of any Type, will only compare against properties that match each keyword's Type included in *{"propertiesToSearch"}*

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new string[] { "Seats" }, new object[] { 2 });

//build keywords for a more advanced search, against different properites
string[] properties = new string[] { "Seats", "AC" }
object[] keywords = new object { 2, true };
var results = Cars.Search(properties, keywords);

//this will only search the 2 properties 'Seats' (which is an int) and 'AC' which is a bool
//since the 2 objects specified are of Type int and boot will search for Cars where 'Cars.Seats == 2 && AC == true'
```

### Overload
```
IQueryable Search(string[] properties_to_search, string[] keywords)
```

*Details:*
* Basic single level string only search

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new string[] { "Details" }, new string[] { "fast", "budget" });
```

### Overload
```
IQueryable Search(Type[] typesToExplore, object[] keywords)
```

*Details:*
* This does _not_ iterate down into any nested objects
* Recommended you don't use this method as it is very limited, it is mostly used internally

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new Type[] { typeof(string) }, new object[] { "Leather", "2-Door" });

//showing a more detailed example to show some limitations of this method
Type[] explore = new Type { typeof(string), typeof(Engine) };
object[] keywords = new object { "V8", 350 };
var results = Cars.Search(explore, keywords);

//note that is will *only* search the first level of car, it will *not* search Car.Engine at all
//if you wanted to compare against an Engine, you would have to add new Engine(){ ... } to your keywords
```


### Overload
```
IQueryable Search(string[] properties_to_search, object[] keywords, Type[] typesToExplore)
```

*Details:*
* The same as {{ IQueryable Search(object[] keywords, Type[] types_to_explore) }} but limited to properties in *{"propertiesToSearch"}*
* *keywords* can be of any Type, will only compare against properties that match each keyword's Type
* this iterates down through any objects specified in *{"types_to_explore"}*

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new string[] { "Details", "Power" }, new object[] { "Leather", "2-Door" }, new Type[] { typeof(Engine) });

//build keywords for a more advanced search, against different multiple Types in an object
string[] properties = new string[] { "Details", "Power" };
object[] keywords = new object { "V8", 350 };
Type[] explore = new Type { typeof(Engine), typeof(Size) };
var results = Cars.Search(keywords, explore);

//this will search each Car for the string 'V8' and int '350' in fields named "Details" and "Engine"
```

### Overload
```
IQueryable Search(string[] properties_to_search, string[] keywords)
```

*Details:*
* Basic single level string only search

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new string[] { "Details" }, new string[] { "fast", "budget" });
```

### Overload
```
IQueryable Search(string[] propertiesToSearch, string[] keywords, IQueryableSearch.StringSearchType stringSearchType)
```

*Details:*
* Basic single level string only search
* If *{"stringSearchType"}* is set to _Equals_ it will only count exact matches (case matters), otherwise it behaves the same as *{{ IQueryable Search(string[] propertiesToSearch, string[] keywords) }}*
* Generally not used as this extension is for more dynamic searches, but available nonetheless

*Usage:*
```
//simple search
var simpleResults = Cars.Search(new string[] { "Details" }, new string[] { "fast", "budget" },
    Naspinski.Utilities.IQueryableSearch.StringSearchType.Equals);
```

### Overload
```
IQueryable Search(Dictionary<string, Type> properties_to_search, object[] keywords)
```

*Details:*
* Recommended you don't use this method as it adds extra work that can be handled by Reflection
* Only use _if_ :
* You have multiple nested properties with the same name, different type _and_
* You don't want to search them both _and_
* You want to do a multi-level search

*Usage:*
```
Dictionary<string, Type> properties = new Dictionary<string, Type>() { { "name", typeof(string) } };
object[] keywords = new object[] { "Corvette" };
var results = Cars.Search(properties, keywords);
```

### Overload
```
IQueryable Search(Dictionary<string, Type> properties_to_search, object[] keywords, IQueryableSearch.StringSearchType stringSearchType)
```

*Details:*
* Recommended you don't use this method as it adds extra work that can be handled by Reflection
* If *{"stringSearchType"}* is set to _Equals_ it will only count exact matches (case matters), otherwise it behaves the same as ```{{ IQueryable Search(Dictionary<string, Type> properties_to_search, object[] keywords) }}```
* Only use _if_ :
* You have multiple nested properties with the same name, different type _and_
* You don't want to search them both _and_
* You want to do a multi-level search

*Usage:*
```
Dictionary<string, Type> properties = new Dictionary<string, Type>() { { "name", typeof(string) } };
object[] keywords = new object[] { "Corvette" };
var results = Cars.Search(properties, keywords, Naspinski.Utilities.IQueryableSearch.StringSearchType.Equals);
```
