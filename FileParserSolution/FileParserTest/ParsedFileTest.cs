﻿using System.Collections.Generic;
using Xunit;

using FileParser;
using System.Linq;

namespace FileParserTest
{
    public class ParsedFileTest
    {
        private string _sampleFolderPath = "TestFiles" + System.IO.Path.DirectorySeparatorChar;

        [Fact]
        public void BasicTest()
        {
            List<int> numberList = new List<int>();
            List<string> stringList = new List<string>();

            ParsedFile file = new ParsedFile(_sampleFolderPath + "Sample_file.txt");

            ParsedLine firstLine = file.NextLine();

            int n = firstLine.NextElement<int>();
            string str = firstLine.NextElement<string>();

            while (!file.Empty)
            {
                ParsedLine line = file.NextLine();
                int counter = line.NextElement<int>();
                for (int j = 0; j < counter; ++j)
                    numberList.Add(line.NextElement<int>());

                while (!line.Empty)
                    stringList.Add(line.NextElement<string>());
            }

            Assert.Equal(23, n);
            Assert.Equal("food", str);
            Assert.Equal(new List<int>() { 100, 200, 300, 400, 500, 600, 700 }, numberList);
            Assert.Equal(new List<string>() { "apple", "peer", "banana", "meat", "fish" }, stringList);
        }

        [Fact]
        public void PeekTestNotModifiyngFile()
        {
            ParsedFile file = new ParsedFile(_sampleFolderPath + "Sample_file.txt");

            for (int i = 0; i < 10; ++i)
            {
                ParsedLine peekedFirstLine = file.PeekNextLine();       // Allows its modification without extracting it

                int peekedN = peekedFirstLine.PeekNextElement<int>();
                Assert.Equal(peekedN, peekedFirstLine.PeekNextElement<int>());

                ParsedLine peekedFirstLine2 = file.PeekNextLine();

                int peekedN2 = peekedFirstLine.PeekNextElement<int>();
                Assert.Equal(peekedN2, peekedFirstLine2.PeekNextElement<int>());

                Assert.Equal(peekedN, peekedN2);
            }
        }

        [Fact]
        public void PeekTestEmptyingFile()
        {
            ParsedFile file = new ParsedFile(_sampleFolderPath + "Sample_file.txt");

            ParsedLine peekedFirstLine = file.PeekNextLine();   // Allows its modification without extracting it

            int peekedN = peekedFirstLine.PeekNextElement<int>();
            Assert.Equal(peekedN, peekedFirstLine.NextElement<int>());          // Extracting the element
            string peekedStr = peekedFirstLine.PeekNextElement<string>();
            Assert.Equal(peekedStr, peekedFirstLine.NextElement<string>());

            Assert.True(file.NextLine().Empty);

            while (!file.Empty)
            {
                List<int> peekedNumberList = new List<int>();
                List<string> peekedStringList = new List<string>();
                List<int> numberList = new List<int>();
                List<string> stringList = new List<string>();

                ParsedLine peekedLine = file.PeekNextLine();
                int peekedCounter = peekedLine.PeekNextElement<int>();
                Assert.Equal(peekedCounter, peekedLine.NextElement<int>());
                for (int j = 0; j < peekedCounter; ++j)
                {
                    peekedNumberList.Add(peekedLine.PeekNextElement<int>());
                    Assert.Equal(peekedNumberList.Last(), peekedLine.NextElement<int>());   // Extracting the element
                }

                while (!peekedLine.Empty)
                {
                    peekedStringList.Add(peekedLine.PeekNextElement<string>());
                    Assert.Equal(peekedStringList.Last(), peekedLine.NextElement<string>());   // Extracting the element
                }

                ParsedLine line = file.NextLine(); ;   // Extracting the line, already emotied, to allow the test to finish
                Assert.True(line.Empty);
            }
        }

        [Fact]
        public void PeekTestChangesNotAffectingOriginalFile()
        {
            ParsedFile file = new ParsedFile(_sampleFolderPath + "Sample_file.txt");

            for (int i = 0; i < 10; ++i)
            {
                ParsedLine peekedFirstLine = file.PeekNextLine();       // Allows its modification without extracting it

                int peekedN = peekedFirstLine.PeekNextElement<int>();
                peekedN *= 2;
                Assert.Equal(peekedN, 2*peekedFirstLine.PeekNextElement<int>());

                ParsedLine peekedFirstLine2 = file.PeekNextLine();

                int peekedN2 = peekedFirstLine.PeekNextElement<int>();
                peekedN2 *= 2;
                Assert.Equal(peekedN2, 2*peekedFirstLine2.PeekNextElement<int>());

                Assert.Equal(peekedN, peekedN2);
            }
        }
    }
}
