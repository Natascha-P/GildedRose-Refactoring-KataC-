using System;
using System.Collections.Generic;
using System.Linq;

namespace GildedRoseKata;

public class GildedRose
{
    IList<Item> Items;

    public GildedRose(IList<Item> Items)
    {
        this.Items = Items;
    }

    public void UpdateQuality()
    {
        // first part : there was a bug  in the program => I have to change the condition Items[i].SellIn < 0 to Items[i].SellIn <= 0
        // all the test are ok now 
      /*  for (var i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name == "Aged Brie" && Items[i].Quality < 50 && Items[i].SellIn <= 0) Items[i].Quality = Items[i].Quality + 1;
            if(Items[i].Name == "Aged Brie" && Items[i].Quality < 50) Items[i].Quality = Items[i].Quality + 1;

            if( Items[i].Name == "Backstage passes to a TAFKAL80ETC concert" && Items[i].Quality < 50) Items[i].Quality = Items[i].Quality + 1;
            if( Items[i].Name == "Backstage passes to a TAFKAL80ETC concert" && Items[i].Quality < 50 && Items[i].SellIn < 11) Items[i].Quality = Items[i].Quality + 1;
            if( Items[i].Name == "Backstage passes to a TAFKAL80ETC concert" && Items[i].Quality < 50 && Items[i].SellIn < 6) Items[i].Quality = Items[i].Quality + 1;
            if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert" && Items[i].SellIn <= 0) Items[i].Quality = Items[i].Quality - Items[i].Quality;

            if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert" && Items[i].Quality > 0 && Items[i].Name != "Sulfuras, Hand of Ragnaros")
            {
                    Items[i].Quality = Items[i].Quality - 1;
            }
            if (Items[i].Name != "Sulfuras, Hand of Ragnaros") Items[i].SellIn = Items[i].SellIn - 1;

            if (Items[i].Name != "Aged Brie" && Items[i].SellIn < 0 && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert" && Items[i].Name != "Sulfuras, Hand of Ragnaros" && Items[i].Quality > 0)  Items[i].Quality = Items[i].Quality - 1;

        }*/

        // second part
        // make it better for the code
        /*      foreach(var item in Items)
        {
            
            if(item.Name == "Aged Brie") AgedBrieUpdateQuality(item);
            else if(item.Name == "Backstage passes to a TAFKAL80ETC concert") BackstageUpdateQuality(item);
            else if(item.Name == "Sulfuras, Hand of Ragnaros") SulfurasUpdateQuality(item);
            else DefaultsUpdateQuality(item);
            if(item.Name != "Sulfuras, Hand of Ragnaros") item.SellIn = item.SellIn - 1;
            
        }*/

        foreach(var item in Items)
        {
            // make it in switch
        /*  BaseItem baseItem = item.Name switch{
                "Aged Brie" =>  new AgedBrieItem(item),
                "Backstage passes to a TAFKAL80ETC concert" => new BackstageItem(item),
                "Sulfuras, Hand of Ragnaros" => new BaseItem(item),
                _ => new DefaultItem(item)
            };
*/
            CreateItem(item).Update();
        }
    }

    public Dictionary<string, Func<Item, BaseItem>> UpdateableBaseItemTable = new() {
        {"Aged Brie", (item) => new AgedBrieItem(item)},
        {"Backstage passes to a TAFKAL80ETC concert", (item) => new BackstageItem(item)},
        {"Sulfuras, Hand of Ragnaros", (item) => new BaseItem(item)},
        {"Conjured Mana Cake", (item) => new ConjuredItem(item)},
        {"Default", (item) => new DefaultItem(item)}

    };

    public BaseItem CreateItem(Item item)
    {
        // removed to Dictionary
        /* 
        return item.Name switch{
                "Aged Brie" =>  new AgedBrieItem(item),
                "Backstage passes to a TAFKAL80ETC concert" => new BackstageItem(item),
                "Sulfuras, Hand of Ragnaros" => new BaseItem(item),
                _ => new DefaultItem(item)
            };
            */

        return UpdateableBaseItemTable.First((keyValuePair) => (keyValuePair.Key.Equals(item.Name) || keyValuePair.Key.Equals("Default"))).Value(item);
    }

    public void AgedBrieUpdateQuality(Item item)
    {
         if( item.Quality < 50) item.Quality = item.Quality + 1;
        if ( item.Quality < 50 && item.SellIn <= 0) item.Quality = item.Quality + 1;
         
    }

    public void BackstageUpdateQuality(Item item)
    {
        if (item.Quality < 50) item.Quality = item.Quality + 1;
        if (item.Quality < 50 && item.SellIn < 11) item.Quality = item.Quality + 1;
        if (item.Quality < 50 && item.SellIn < 6) item.Quality = item.Quality + 1;
        if (item.SellIn <= 0) item.Quality = item.Quality - item.Quality;
    }

    public void SulfurasUpdateQuality(Item item)
    {
        return;
    }

    public void DefaultsUpdateQuality(Item item)
    {
        if(item.Quality > 0) item.Quality = item.Quality - 1;
        if(item.Quality > 0 && item.SellIn <= 0 ) item.Quality = item.Quality - 1;
    }
}

public class BaseItem
{
    public Item item;

    public BaseItem(Item _item)
    {
        this.item = _item;
    }

    public virtual void Update()
    {
        return;
    }
}

public class SulfuraItem : BaseItem
{

    public SulfuraItem(Item _item) : base(_item)
    {
    }

    public override void Update()
    {
        return;
    }
}

public class AgedBrieItem : BaseItem
{
    public AgedBrieItem(Item _item) : base(_item)
    {
    }
    

    public override void Update()
    {
        if ( item.Quality < 50 && item.SellIn <= 0) item.Quality = item.Quality + 1;
        if( item.Quality < 50) item.Quality = item.Quality + 1;

        item.SellIn = item.SellIn - 1;
    }
}

public class BackstageItem : BaseItem
{

    public BackstageItem(Item _item) : base(_item)
    {
    }
    

    public override void Update()
    {
        if (item.Quality < 50) item.Quality = item.Quality + 1;
        if (item.Quality < 50 && item.SellIn < 11) item.Quality = item.Quality + 1;
        if (item.Quality < 50 && item.SellIn < 6) item.Quality = item.Quality + 1;
        if (item.SellIn <= 0) item.Quality = item.Quality - item.Quality;
        item.SellIn = item.SellIn - 1;
    }
}

public class DefaultItem : BaseItem
{
    public DefaultItem(Item _item) : base(_item)
    {
    }

    public override void Update()
    {
        if(item.Quality > 0) item.Quality = item.Quality - 1;
        if(item.Quality > 0 && item.SellIn <=0  ) item.Quality = item.Quality - 1;
        item.SellIn = item.SellIn - 1;
    }
}

public class ConjuredItem : BaseItem
{
    public ConjuredItem(Item _item) : base(_item)
    {
    }

    public override void Update()
    {
        if(item.Quality > 0) item.Quality = item.Quality - 2;
        if(item.Quality > 0 && item.SellIn <= 0 ) item.Quality = item.Quality - 2;
        item.SellIn = item.SellIn - 1;

    }
}