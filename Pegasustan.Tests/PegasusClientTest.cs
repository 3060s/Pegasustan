﻿using Pegasustan.Domain;

namespace Pegasustan.Tests;

[TestFixture]
public sealed class PegasusClientTest
{
    private PegasusClient _pegasusClient;
    
    [SetUp]
    public async Task Setup()
    {
        _pegasusClient = await PegasusClient.CreateAsync();
    }
    
    [Test]
    public void GetLanguagesAsync_WhenCalled_ReturnsNonEmptyArray()
    {
        var languages = Array.Empty<Language>();
        Assert.DoesNotThrowAsync(async () =>
        {
            languages = await _pegasusClient.GetLanguagesAsync();
        });
        Assert.That(languages, Is.Not.Empty);
    }

    [Test]
    public void DefaultLanguage_WhenUnchanged_IsEnglish()
    {
        Assert.That(_pegasusClient.DefaultLanguage.Code.ToLower(), Is.EqualTo("en"));
    }
    
    [TestCase("tr")]
    [TestCase("en")]
    [TestCase("de")]
    [TestCase("fr")]
    [TestCase("it")]
    [TestCase("es")]
    [TestCase("ru")]
    [TestCase("ar")]
    public void ChangeLanguage_WhenCodePassed_SetsDefaultLanguageToCode(string code)
    {
        _pegasusClient.ChangeLanguage(code);
        Assert.That(_pegasusClient.DefaultLanguage.Code.ToLower(), Is.EqualTo(code));
    }

    [Test]
    public void GetDepartureCountriesAsync_WhenCalled_ReturnsNonEmptyArray()
    {
        var countries = Array.Empty<Country>();
        Assert.DoesNotThrowAsync(async () =>
        {
            countries = await _pegasusClient.GetDepartureCountriesAsync();
        });
        Assert.That(countries, Is.Not.Empty);
    }    

    [Test]
    public async Task GetArrivalCountriesAsync_WhenCalled_DoesNotThrow()
    { 
        var countries = await _pegasusClient.GetDepartureCountriesAsync();
        Assert.DoesNotThrowAsync(async () => await _pegasusClient.GetArrivalCountriesAsync(countries.First().Ports.First()));
    }
        
    [Test]
    public void GetCitiesForBestDealsAsync_WhenCalled_ReturnsNonEmptyArray()
    {
        var cities = Array.Empty<BestDealsCity>();
        Assert.DoesNotThrowAsync(async () =>
        {
            cities = await _pegasusClient.GetCitiesForBestDealsAsync();
        });
        Assert.That(cities, Is.Not.Empty);
    }
    
    [Test]
    public async Task GetBestDealsAsync_WhenCalled_ReturnsNonEmptyArray()
    {
        var cities = await _pegasusClient.GetCitiesForBestDealsAsync();
        var deals = Array.Empty<BestDeal>();
        Assert.DoesNotThrowAsync(async () =>
        {
            deals = await _pegasusClient.GetBestDealsAsync(cities.First(), Currency.Lira);
        });
        Assert.That(deals, Is.Not.Empty);
    }
    
    // TODO: More tests
}
