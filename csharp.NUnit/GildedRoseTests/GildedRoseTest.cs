using System;
using System.Collections.Generic;
using GildedRoseKata;
using NUnit.Framework;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Test]
    public void Foo()
    {
        var items = new List<Item> { new Item { Name = "Default", SellIn = 0, Quality = 0 } };
        var app = new GildedRose(items);
        app.UpdateQuality();
        Assert.That(items[0].Name, Is.EqualTo("Default"));

      
    }

    [Test]
    //At the end of each day our system lowers SellIn value for every item
    public void GildedRose_ShouldLowersSellInValue()
    {
        var items = new List<Item> { new Item { Name = "Default", SellIn = 5, Quality = 5 } };
        var app = new GildedRose(items);

        app.UpdateQuality();
        Assert.That(items[0].SellIn, Is.EqualTo(4));
    }

    [Test]
     //At the end of each day our system lowers Quality value for every item
    public void GildedRose_ShouldLowersQualityValue()
    {
        var items = new List<Item> { CreateNewItem("Default", 5, 5) };
        var app = new GildedRose(items);

        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(4));
    }

    [Test]
    //  Once the sell by date has passed, `Quality` degrades twice as fast
    public void GildedRose_ShouldQuality_DegradesTwiceAs_fast_IfSellByDateHasPassed()
    {
        // Once the sell by date has passed => SellIn = 0
        var items = new List<Item> { CreateNewItem("Default", -1, 4) };
        var app = new GildedRose(items);

        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(2));
    }

    [Test] 
    //The `Quality` of an item is never negative
    public void GildedRose_ShouldQuality_StaysPositive()
    {
        // Once the sell by date has passed => SellIn = 0
        var items = new List<Item> { CreateNewItem("Default", -1, 1) };
        var app = new GildedRose(items);

        app.UpdateQuality();
        Assert.That(items[0].Quality, Is.EqualTo(0));
    }

    [Test]
    //  __"Aged Brie"__ actually increases in `Quality` the older it gets
    public void GildedRose_ShouldAgedBrie_ActuallyIncreasesInQuality_TheOlderItGets()
    {
        var agedBrieItem = CreateNewItem("Aged Brie", 5, 1);
        var app = new GildedRose(new List<Item> { agedBrieItem });

        app.UpdateQuality();
        // the quality increases
        Assert.That(agedBrieItem.Quality, Is.EqualTo(2));
    }

    [Test]
    //The `Quality` of an item is never more than `50`
    public void GildedRose_ShouldQuality_NeverBeMoreThan50()
    {
       // var fooItem = new Item { Name = "foo", SellIn = 5, Quality = 51 };
        var fooItem = CreateNewItem("Default", 5, 51);
        var app = new GildedRose(new List<Item> { fooItem });

        app.UpdateQuality();
        Assert.That(fooItem.Quality, Is.EqualTo(50));
    }

    [Test]
    //__"Sulfuras"__, being a legendary item, never has to be sold or decreases in `Quality`
    // wrong name in requirements or in the code
    public void GildedRose_ShouldSulfuras_NeverDecreasesInQuality()
    {
        var sulfurasItem = CreateNewItem("Sulfuras, Hand of Ragnaros", 5, 2);
        var app = new GildedRose(new List<Item> { sulfurasItem });

        app.UpdateQuality();
        // the quality doesn't decrease
        Assert.That(sulfurasItem.Quality, Is.EqualTo(2));
    }

    [Test]
    //__"Sulfuras"__, being a legendary item, never has to be sold or decreases in `Quality`
    // wrong name in requirements or in the code
    public void GildedRose_ShouldSulfuras_NeverHasToBeSold()
    {
        var sulfurasItem = CreateNewItem("Sulfuras, Hand of Ragnaros", 5, 2);
        var app = new GildedRose(new List<Item> { sulfurasItem });

        app.UpdateQuality();
        // never has to be sold ==> the sellIn is never changed
        Assert.That(sulfurasItem.SellIn, Is.EqualTo(5));
    }


    [Test]
    // _"Backstage passes"__, like aged brie, increases in `Quality` as its `SellIn` value approaches;
	//- `Quality` increases by `2` when there are `10` days or less and by `3` when there are `5` days or less but
	//- `Quality` drops to `0` after the concert

    public void GildedRose_ShouldBackstagePasses_IncreaseQualityIfSellInIsMoreThan10Days()
    {
        var backstagePassesItem = CreateNewItem("Backstage passes to a TAFKAL80ETC concert", 20, 20);
        var app = new GildedRose(new List<Item> { backstagePassesItem });

        app.UpdateQuality();
        // quality increases
        Assert.That(backstagePassesItem.Quality, Is.EqualTo(21));
    }

    [Test]
    // //- `Quality` increases by `2` when there are `10` days or less 
    public void GildedRose_ShouldBackstagePasses_IncreaseQualityIfSellInIsBetween6And10Days()
    {
        var backstagePassesItem = CreateNewItem("Backstage passes to a TAFKAL80ETC concert",10, 20);
        var app = new GildedRose(new List<Item> { backstagePassesItem });

        app.UpdateQuality();
        // quality increases by 2
        Assert.That(backstagePassesItem.Quality, Is.EqualTo(22));
    }

     [Test]
    // //- `Quality` increases  by `3` when there are `5` days or less
    public void GildedRose_ShouldBackstagePasses_IncreaseQualityIfSellInIsBetween0And5Days()
    {
        var backstagePassesItem = CreateNewItem("Backstage passes to a TAFKAL80ETC concert", 3, 20);
        var app = new GildedRose(new List<Item> { backstagePassesItem });

        app.UpdateQuality();
        // quality increases by 3
        Assert.That(backstagePassesItem.Quality, Is.EqualTo(23));
    }

    [Test]
     // Quality` drops to `0` after the concert
    public void GildedRose_ShouldBackstagePasses_QualityIs0AfterTheConcert()
    {
        //if the sellIn = 0
        var backstagePassesItem = CreateNewItem("Backstage passes to a TAFKAL80ETC concert", 0, 50);
        var app = new GildedRose(new List<Item> { backstagePassesItem });

        app.UpdateQuality();
        // quality drops to 0
        Assert.That(backstagePassesItem.Quality, Is.EqualTo(0));
    }

   [Test]
   //  __"Conjured"__ items degrade in `Quality` twice as fast as normal items
   public void GildedRose_ShouldConjured_QualityDegradesTwiceAsFastAsNormalItem()
   {
         var conjuredItem = CreateNewItem("Conjured Mana Cake", 5, 4);
         // tested for Conjured Mana Cake but I don't add it in the dictionary in the main file
     /*   var conjuredItemFromClass = new ConjuredItem(conjuredItem);
        conjuredItemFromClass.Update();*/
        // after added in the dictionary
        var app = new GildedRose(new List<Item> { conjuredItem });
        app.UpdateQuality();
         Assert.That(conjuredItem.Quality, Is.EqualTo(2));
   }

   [Test]
   // __"Sulfuras"__ is a legendary item and as such its `Quality` is `80` and it never alters.
   public void GildedRose_ShouldSulfuras_HaveAlwaysQuality80()
    {
        //if the sellIn = 0
        var backstagePassesItem = CreateNewItem("Sulfuras, Hand of Ragnaros", 0, 80);
        var app = new GildedRose(new List<Item> { backstagePassesItem });

        app.UpdateQuality();
       
        Assert.That(backstagePassesItem.Quality, Is.EqualTo(80));
    }

    public Item CreateNewItem(string name, int sellIn, int quality) => new Item{Name = name, SellIn = sellIn, Quality = quality}; 
}

