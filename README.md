# FileParser

[![Build Status](https://travis-ci.org/eduherminio/FileParser.svg?branch=master)](https://travis-ci.org/eduherminio/FileParser)

[![Nuget Status](https://img.shields.io/nuget/v/FileParser.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/FileParser)

**FileParser** is a C# file parser designed to read text files line-by-line, saving each line's content into basic types vars (int, double, string, etc.).

**.NET Standard 2.0** library.

Versions for .NET Framework 4.7, 4.61 & 4.6 have been added (v1.0), since code is fully compatible now; but that support isn't guaranteed in future FileParser versions.

## Purpose
This project was born with a very specific purpose: providing a tool with whom easily parse files with a known structure, ideally being as flexible and easy to use as C++ standard IO approach.

For those who don't understand what I mean, here's a simple Use Case ([also reposited](https://github.com/eduherminio/FileParser/tree/master/Examples)):

Given the following `input.txt`, which contains an integer n (>=0) followed by n doubles and a final string,
```txt
5   1.1 3.14159265 2.2265       5.5 10              fish
```

A simple `.cpp` snippet like the following one could process `input.txt`, providing that file is selected as standard input source:

 `./myExecutable < input.txt > output.txt`

```cpp
#include <iostream>
#include <list>
#include <string>

int main()
{
    int _integer;
    std::string _str;
    std::list<double> _list;
    double _auxdouble;

    // Input start;
    std::cin>>_integer;
    for(int i=0; i<_integer; ++i)
    {
        std::cin>>_auxdouble;
        _list.push_back(_auxdouble);
    }
    std::cin>>_str;
    // Input end

    // Data processing

    // Output start
    std::cout<<_integer<<" ";
    for(const double& d : _list)
        std::cout<<d<<" ";
    std::cout<<_str;
    // Output end

    return 0;
}
```

Seems effortless to process these kind of simple `.txt` files using C++, right?

Well, using C# things are not so straight-forward, and that's why `FileParser` was created for:

```csharp
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using FileParser;

namespace FileParserSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

            List<double> listDouble = new List<double>();
            string str;

            // Input start
            IParsedFile file = new ParsedFile("SimpleInput.txt");
            IParsedLine firstLine = file.NextLine();

            int _integer = firstLine.NextElement<int>();

            for(int i=0; i<_integer; ++i)
                listDouble.Add(firstLine.NextElement<double>());

            str = firstLine.NextElement<string>();
            // Input end

            // Data Processing

            // Output start
            StreamWriter writer = new StreamWriter("..\\C#SimpleOutput.txt");
            using (writer)
            {
                writer.WriteLine(_integer + " " + string.Join(null, listDouble));
            }
            // Output end
        }
    }
}
```

# Documentation
*Coming soon!*

[These tests](https://github.com/eduherminio/FileParser/blob/master/FileParserSolution/FileParserTest/ParsedFileTest.cs) can serve as example meanwhile.

A real project using it can be found [here](https://github.com/eduherminio/Google_HashCode_2018/blob/master/GoogleHashCode2018/Project/Manager.cs#L63) too.