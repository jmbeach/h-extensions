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
## Build

You can build the project using Visual Studio or by running the grunt tasks for `msbuild`

## Contribute

This project uses [hylasoft/cs-boilerplate](https://github.com/hylasoft-usa/cs-boilerplate) to define tasks and stle guides. Please read the readme of the project to learn more about how to contribute.
