# EnumUtilities
Simple Enum utilities
----
## GetValues<>()
**Details:**
* Gets a collection of enums (useful for looping)
* **{{ T : enum }}**
**Usage:**
{code:c#}
enum Tester { Test = 1, Bla = 5 }

// loop through them
foreach(var e in EnumUtilities.GetValues<Tester>())
    System.PrintLine(e.ToString() + " = " + (int)e);
{code:c#}
----