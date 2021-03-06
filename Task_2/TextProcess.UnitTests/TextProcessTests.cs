﻿using System.Linq;
using NUnit.Framework;
using TextModel;
using TextModel.TextElements.SentenceElements;
using TextTools.tools;

namespace TextProcess.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SortByWordCountTests()
        {
            // arrange
            var path = @"C:\Users\dilexet\Documents\epam\Task_2\TextProcess\file_1.txt";
            ITextStreamReader textStreamReader = new TextStream(path);
            IParser parser = new Parser(
                @"\w",
                @"[ ]+|[\t]+",
                @"[\r\n]+");
            Text text = parser.Parse(textStreamReader.TextReader());
            textStreamReader.Dispose();
            
            // act
            var actual = text.SortByWordCount().ToList();
           
            // assert
            Assert.IsNotEmpty(actual);
            for (int i = 0; i < actual.Count() - 1; i++)
            {
                int index = i;
                Assert.That(() => actual[index].WordsCount <= actual[index + 1].WordsCount);
            }
           
        }
        
        [Test]
        public void GetWordsGivenLengthTests()
        {
            // arrange
            var path = @"C:\Users\dilexet\Documents\epam\Task_2\TextProcess\file_1.txt";
            ITextStreamReader textStreamReader = new TextStream(path);
            IParser parser = new Parser(
                @"\w",
                @"[ ]+|[\t]+",
                @"[\r\n]+");
            Text text = parser.Parse(textStreamReader.TextReader());
            textStreamReader.Dispose();
            int lenght = 5;
            
            // act
            var actual = text.GetWordsGivenLength(lenght).ToList();
           
            // assert
            Assert.IsNotEmpty(actual);
            foreach (var word in actual)
            {
                Assert.That(() => word.SymbolCount == lenght);
            }
        }
        
        [Test]
        public void DeleteWordsBeginConsonantTests()
        {
            // arrange
            var path = @"C:\Users\dilexet\Documents\epam\Task_2\TextProcess\file_1.txt";
            ITextStreamReader textStreamReader = new TextStream(path);
            IParser parser = new Parser(
                @"\w",
                @"[ ]+|[\t]+",
                @"[\r\n]+");
            Text text = parser.Parse(textStreamReader.TextReader());
            textStreamReader.Dispose();
            int lenght = 5;
            
            // act
            var actual = text.DeleteWordsBeginConsonant(lenght).ToList();
           
            // assert
            Assert.IsNotEmpty(actual);
            foreach (var sentence in actual)
            {
                var words = sentence.GetSentenceItems().OfType<Word>().ToList();
                foreach (var word in words)
                {
                    Assert.That(() => !word.IsWordBeginWithConsonant || word.SymbolCount != lenght);
                }
            }
            
        }
        
        [Test]
        public void ReplaceStringWithSubstringTests()
        {
            // arrange
            var path = @"C:\Users\dilexet\Documents\epam\Task_2\TextProcess\file_1.txt";
            ITextStreamReader textStreamReader = new TextStream(path);
            IParser parser = new Parser(
                @"\w",
                @"[ ]+|[\t]+",
                @"[\r\n]+");
            Text text = parser.Parse(textStreamReader.TextReader());
            textStreamReader.Dispose();
            int lenght = 5;
            string substring = "Hello";
            var expected = text.GetSentence().ToList();
            
            // act
            var actual = text.ReplaceStringWithSubstring(lenght, substring).ToList();
           
            // assert
            Assert.IsNotEmpty(actual);
            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < actual.Count; i++)
            {
                var expectedWord = expected[i].GetSentenceItems().OfType<Word>().ToList();
                var actualWord = actual[i].GetSentenceItems().OfType<Word>().ToList();
                Assert.AreEqual(expectedWord.Count, actualWord.Count);
                for (int j = 0; j < actualWord.Count; j++)
                {
                    int index = j;
                    Assert.That(() => (expectedWord[index].SymbolCount == lenght && actualWord[index].Value == substring) ||
                                      (expectedWord[index].SymbolCount != lenght && actualWord[index].Value != substring));
                }
            }
        }
    }
}