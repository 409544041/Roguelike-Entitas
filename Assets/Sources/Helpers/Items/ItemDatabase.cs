﻿using System.Collections.Generic;

public class ItemDatabase
{
	private readonly Dictionary<ItemName, IItem> items = new Dictionary<ItemName, IItem>();

	private static ItemDatabase _instance;
	public static ItemDatabase Instance
	{
		get { return _instance ?? (_instance = new ItemDatabase()); }
	}

	public IItem GetItem(ItemName name)
	{
		return items[name];
	}

	public void RegisterItem(ItemName name, IItem item)
	{
		items.Add(name, item);
	}
}