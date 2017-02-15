h-extensions
==========

An extension library for .Net classes and types.

## Requirements
h-extensions works with .NET Framework 4.5 and higher

## Usage

Build a string from any IEnumerable of char's.

````C#
  var chars = new []{'a', 'b', 'f', 'o', 'o'};
  var foo = chars.Where(chr => chr > 0x65).BuildString();
  // foo == "foo"
````

Build a list string from any IEnumerable of char's.
````C#
  var chars = new []{'a', 'b', 'f', 'o', 'o'};
  var foo = chars.Where(chr => chr > 0x65).ToRangeString();
  // foo == "f, o, o"
````

Parse an enumeration from a string.

````C#
public enum TestEnum
{
  Unknown = 0x0,
  FirstValue = 0x1,
  SecondValue = 0x2
}

public void Foo()
{
  var first = "FirstValue".ToEnum<TestEnum>();
  // first == TestEnum.FirstValue

  var invalid = "NotARealValue".ToEnum<TestEnum>();
  // invalid == TestEnum.Unknown
}
````

Retrieve the description from an enumeration.

````C#
public enum TestEnum
{
  [Description("Uknown Value")]
  Unknown = 0x0,

  [Description("The First Value")]
  FirstValue = 0x1,

  [Description("The Second Value")]
  SecondValue = 0x2
}

public void Foo()
{
  var description = "FirstValue".ToEnum<TestEnum>().GetDescription();
  // description == "The First Value"
}
````

Create a delimited list string, from any IEnumerable<string>.

````C#
public void Foo()
{
  const string fooDir = @"C:\foo";

  Directory.CreateDirectory(fooDir);
  File.Create(Path.Combine(fooDir, "bar.txt"));
  File.Create(Path.Combine(fooDir, "baz.txt"));

  var fooFiles = Directory.GetFiles(fooDir).ToListString("; ");
  // fooFiles = "bar.txt; baz.txt"
}
````

Retrieve the inner most exception.

````C#
  var foo = new NotImplementedException("Foo");
  var bar = new NotImplementedException("Bar", foo);

  var inner = bar.InnerMost();
  // inner == foo
````

Build a string of object properties.

````C#
	var testObject = new
	{
		Foo = "Baz",
		Identity = 42,
		NestedValues = new[]
		{
			"First",
			"Second",
			"Thrid"
		},
		NestedObject = new
		{
			Inner = "InnerValue",
			InnerIdentity = 47
		}
	};

	var details = testObject.ToDetailedString("testObject");
	/* details =
		(<>f__AnonymousType1`4) [<>f__AnonymousType1`4] : testObject
			(String) [Foo] : Baz
			(Int32) [Identity] : 42
			(String[]) [String[]] : NestedValues
				(String) : First
				(String) : Second
				(String) : Thrid
			(<>f__AnonymousType0`2) [<>f__AnonymousType0`2] : NestedObject
				(String) [Inner] : InnerValue
				(Int32) [InnerIdentity] : 47
	*/
````

Convert enumerations to collections.

````C#
  var foo = new [] { 1, 2, 3, 4 };
  var bar = foo
    .Where(val => val > 2)
    .ToCollection();

  // bar == Collection<int> { 3, 4 };
````

Perform an action on an enumeration.

````C#
  var foo = new [] { 1, 2, 3, 4 };

  foo.Select(val => val.ToString())
    .ForEach(Console.Write);

 // Console: 1234
````

Retrieve the default value of a given type.

````C#
  var foo = typeof(string).DefaultValue();
  var bar = typeof(int).DefaultValue();

  // foo == null
  // bar == 0

  foo = DefaultValue<string>();
  bar = DefaultValue<int>();

  // foo == null
  // bar == 0
````

## Build

You can build the project using Visual Studio or by running the grunt tasks for `msbuild`

## Contribute

This project uses [hylasoft/cs-boilerplate](https://github.com/hylasoft-usa/cs-boilerplate) to define tasks and stle guides. Please read the readme of the project to learn more about how to contribute.
