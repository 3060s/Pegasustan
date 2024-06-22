﻿using Pegasustan.Utils;

namespace Pegasustan.Tests.Utils;

[TestFixture]
public sealed class YearMonthTest
{
    [Test]
    public void ParseText()
    {
        var yearMonth = YearMonth.Parse("2024-07");
        
        Assert.Multiple(() =>
        {
            Assert.That(yearMonth.Year, Is.EqualTo(2024));
            Assert.That(yearMonth.Month, Is.EqualTo(7));
        });
    }
    
    [Test]
    public void FailParsingInvalidText()
    {
        Assert.Multiple(() =>
        {
            Assert.Throws<ArgumentException>(() => YearMonth.Parse(""));
            Assert.Throws<ArgumentException>(() => YearMonth.Parse("-"));
            Assert.Throws<ArgumentException>(() => YearMonth.Parse("2024-13"));
            Assert.Throws<ArgumentException>(() => YearMonth.Parse("0000000"));
        });
    }
}
