# LinqToSql
Helper methods for use with Linq-to-SQL tables
**DEPRECATED**
----
## Get<>()
{code:c#}
T Get<T>(this DataContext dataContext, object primaryKey)
{code:c#}
**Details:**
* Returns an object of Type **T** from the supplied **dataContext** with it's Primary Key equal to **primaryKey**, **primaryKey** can also be a **Dictionary<string, object>** for composite keys.
* **{{ T : class, INotifyPropertyChanged }}**
* **T** must be a table member of **DataContext**
* **T** must have a CustomAttribute of Type **ColumnAttribute** that  _.IsPrimaryKey == true_ and _.DbType_ includes _NOT NULL_
* Throws a details **ArgumentException** if **T**'s Primary Key Type and **primaryKey**'s type do not match
**Usage:**
{code:c#}
//initialize the DataContext
StoreDataContext db = new StoreDataContext();

//gets a Product with Primary Key 5 from the Products Table
Product product =  db.Get<Product>(5);

//gets an Employee from the Employees table, this table has a Guid for the Primary Key
Guid stevesId = new Guid("4fcc0b82-b137-4e4b-935e-872ed662ba53");
Employee steve = db.Get<Employee>(stevesId);

//notice that it works for all types of Primary Keys
{code:c#}
----
## GetPrimaryKey()
{code:c#}
IPrimaryKey GetPrimaryKey<T>()
{code:c#}
**Details:**
* Gets the PropertyInfo(s) of the supplied object **T**
* **{{ T : class, INotifyPropertyChanged }}**
* At least one property of **T** must have a CustomAttribute of Type **ColumnAttribute** that  _.IsPrimaryKey == true_ and _.DbType_ includes _NOT NULL_
* Throws a **NotSupportedException** if the object supplied does not have a Primary Key
**Usage:**
{code:c#}
//gets the IPrimaryKey of the Primary Key Column of the Products table
IPrimaryKey info = Naspinski.Utilities.LinqToSql.GetPrimaryKey<Product>(); 

//get the Type of the Primary Key of table Cars
TableKey carPrimaryKeyType = ((TableKey)Naspinski.Utilities.LinqToSql.GetPrimaryKey<Car>()).PropertyInfo.PropertyType;

//get the Primary Keys of a multi-keyed table
IEnumerable<TableKey> keys = ((TableMulitpleKeys)Naspinski.Utilities.LinqToSql.GetPrimaryKey<SomeObject>()).Keys;
{code:c#}
----
## GetCompositeKey()
{code:c#}
TableCompositeKey GetCompositeKey<T>()
{code:c#}
**Details:**
* Gets the TableCompositeKey(s) of the supplied object **T**
* **{{ T : class, INotifyPropertyChanged }}**
* At least two properties of **T** must have a CustomAttribute of Type **ColumnAttribute** that  _.IsPrimaryKey == true_ and _.DbType_ includes _NOT NULL_
* Throws a **InvalidOperationException** if the object supplied does not have multiple Primary Key
**Usage:**
{code:c#}
//gets the primary keys for Widget
TableMultipleKey info = Naspinski.Utilities.LinqToSql.GetCompositeKey<Widget>();
{code:c#}
----
## GetSingleKey()
{code:c#}
TableKey GetSingleKey<T>()
{code:c#}
**Details:**
* Gets the TableKey of the supplied object **T**
* **{{ T : class, INotifyPropertyChanged }}**
* Only one property of **T** must have a CustomAttribute of Type **ColumnAttribute** that  _.IsPrimaryKey == true_ and _.DbType_ includes _NOT NULL_
* Throws a **InvalidOperationException** if the object supplied does not have only a single Primary Key
**Usage:**
{code:c#}
//gets the primary keys for DooDad
TableKey info = Naspinski.Utilities.LinqToSql.GetSingleKey<DooDad>();
{code:c#}
----
## IsCompositeKey()
{code:c#}
bool IsCompositeKey() [IPrimaryKey extension](IPrimaryKey-extension)
{code:c#}
**Details:**
* Returns true if the IPrimary key is a composite, false if not
* **{{ IPrimaryKey }}**
**Usage:**
{code:c#}
bool keyIsComposite = Naspinski.Utilities.LinqToSql.GetPrimaryKey<Product>().IsCompositeKey();
{code:c#}